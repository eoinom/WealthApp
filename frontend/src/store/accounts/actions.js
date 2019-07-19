var moment = require('moment');


function resetState ({ commit }) {
  commit('resetState')
};

function initialiseAccounts ({ commit, state }, accounts) {
  var accountIdsWithVals = []   // for getting the total accounts balances below
  for (let i = 0; i < Object.keys(accounts).length; i++) {
    let id = accounts[i].accountId
    if (i === 0) {
      commit('setInitialFirstAccountId', id)
    }
    commit('addAccount', accounts[i])
    commit('sortAccountValues', id)

    // get the balance and update in the store
    var numOfValues = accounts[i].accountValues.length;
    if (numOfValues > 0) {
      accountIdsWithVals.push(id)
      let balance = state.accounts[id].accountValues[ numOfValues - 1 ].value;
      let balanceUser = state.accounts[id].accountValues[ numOfValues - 1 ].valueUserCurrency;
      commit('updateAccountBalance', {accountId: id, balance: balance, balanceUserCurrency: balanceUser})
    }
  } 

  // Calculate the total accounts balances 
  // first copy accountValues arrays from each account (with values in it) into another container array
  let allAccountVals = []
  for (let i = 0; i < accountIdsWithVals.length; i++) {
    allAccountVals.push( state.accounts[accountIdsWithVals[i]].accountValues.slice(0) )   // use slice to push a clone of the array 
  }  
  // get all dates
  let allDatesArr = []
  for (let i = 0; i < accountIdsWithVals.length; i++) {
    let accountDates = allAccountVals[i].map(a => a.date);
    for(let j = 0; j < accountDates.length; j++){
      allDatesArr[allDatesArr.length + j] = accountDates[j]
    }
  }
  //remove the empty elements:
  let allDates = allDatesArr.filter(function () { return true });
  // sort the dates
  allDates.sort(function(a, b) {
    let dateA = new Date(a);
    let dateB = new Date(b);
    return dateA - dateB;
  });  
  // create object to store total balances and initialise with a key for each date and a corresponding 0.0 value
  let allAccountBals = {}
  for (let i = 0; i < allDates.length; i++) {
    allAccountBals[ allDates[i] ] = 0.0
  }
  // then for each key value pair, for each account, get accountVal with date closest (but lower) then the current key (date of pair)
  // add the valueUserCurrency to the value part of the pair (the total balance for that date)
  for (let i = 0; i < allDates.length; i++) {
    let date = allDates[i]    
    let balance = 0.0
    for (let j = 0; j < accountIdsWithVals.length; j++) {
      var nearestBal = 0.0
      for (let k = 0; k < allAccountVals[j].length; k++) {      
        if (allAccountVals[j][k].date <= date) {
            nearestBal = allAccountVals[j][k].valueUserCurrency
        }
        else {          
          break;
        }
      }        
      balance = balance + nearestBal
      // round to two decimals (https://stackoverflow.com/questions/11832914/round-to-at-most-2-decimal-places-only-if-necessary)
      balance = Math.round((balance + 0.00001) * 100) / 100
    }
    allAccountBals[date] = balance
  }
  console.log('allAccountBals (after) =');
  console.log(allAccountBals); 
  commit('updateTotalAccountsBalances', allAccountBals)
};

async function addAccount ({ commit, rootState }, account) {
  account.userId = rootState.main.user.userId
  account.quotedCurrency = account.currencyCode
  delete account.currencyCode
  console.log('account to add:')
  console.log(account)  
  
  //send mutation to graphql with account to add to db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($account: AccountInputType!){
          account_mutations {
            addAccount(account: $account) {
              accountId
              accountName
              description
              type
              institution
              isActive
              quotedCurrency {
                code
                nameLong
                nameShort
              }
            }
          }
        }
        `,
        variables: {
          account: account
        },
      }
    });            
    
    // get back details of new account from database and add to local store
    if (response.data.data.account_mutations.addAccount != null) {     
      commit('addAccount', response.data.data.account_mutations.addAccount)
    }   
  } catch (error) {
    console.error(error); 
  }
};

async function updateAccount ({ commit, rootState }, account) {
  account.userId = rootState.main.user.userId
  account.quotedCurrency = account.currencyCode
  delete account.currencyCode
  console.log('account to update:')
  console.log(account)

  //send mutation to graphql with account to update in db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($account: AccountInputType!){
          account_mutations {
            updateAccount(account: $account) {
              accountId
              accountName
              description
              type
              institution
              isActive
              quotedCurrency {
                code
                nameLong
                nameShort
              }
            }
          }
        }
        `,
        variables: {
          account: account
        },
      }
    });            
    
    // get back details of amended account from database and update in local store
    if (response.data.data.account_mutations.updateAccount != null) {          
      commit('updateAccount', response.data.data.account_mutations.updateAccount)
    }   
  } catch (error) {
    console.error(error); 
  }
}

async function deleteAccount ({ commit, state, rootState }, accountId) {
  console.log('accountId for deletion: ' + accountId)
  
  //send mutation to graphql with accountId to delete from db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($accountId: ID!){
          account_mutations {
            deleteAccount(accountId: $accountId)
          }
        }
        `,
        variables: {
          accountId: accountId
        },
      }
    });            
    
    // if delete from db was successful then delete also from local store
    if (response.data.data.account_mutations.deleteAccount != null) {          
      commit('deleteAccount', accountId)
    }   
  } catch (error) {
    console.error(error); 
  }

  if (state.selectedAccountId == accountId) {
    // update the selectedAccountId
    var newSelectedId = 0
    if (state.accountIds.length > 0) {
      newSelectedId = state.accountIds[0]
    }
    commit('updateSelectedAccountId', newSelectedId)
  }
}

function updateAccountBalance ({ commit }, payload) {
  commit('updateAccountBalance', payload)
}

function updateAccountBalances ({ commit, state }, payload) {
  for (var i = 0; i < Object.keys(state.accounts).length; i++) {
    var id = Object.keys(state.accounts)[i].accountId

    commit('sortAccountValues', id)

    // get the balance and update in the store
    var numOfValues = state.accounts[id].accountValues.length;
    if (numOfValues > 0) {
      var balance = state.accounts[id].accountValues[ numOfValues - 1 ].value;
      var balanceUser = state.accounts[id].accountValues[ numOfValues - 1 ].valueUserCurrency;
      commit('updateAccountBalance', {accountId: id, balance: balance, balanceUserCurrency: balanceUser})
    }
  } 
}

async function addAccountValue ({ commit }, accountValue) {
  console.log('account value to add:')
  console.log(accountValue)
  
  //send mutation to graphql with accountValue to add to db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($accountValue: AccountValueInputType!){
          accountValue_mutations {
            addAccountValue(accountValue: $accountValue) {                    
              accountValueId  
              date
              value
              rateToUserCurrency
              valueUserCurrency
              account {
                accountId
              }
            }
          }
        }
        `,
        variables: {
          accountValue: accountValue
        },
      }
    });            
    
    // get back details of new account from database and add to local store
    if (response.data.data.accountValue_mutations.addAccountValue != null) {       
      commit('addAccountValue', response.data.data.accountValue_mutations.addAccountValue)
      commit('sortAccountValues', accountValue.accountId)
    }   
  } catch (error) {
    console.error(error); 
  }
}

async function updateAccountValue ({ commit, rootState }, accountValue) {
  console.log('account value to update:')
  console.log(accountValue)
  
  //send mutation to graphql with accountValue to add to db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($accountValue: AccountValueInputType!){
          accountValue_mutations {
            updateAccountValue(accountValue: $accountValue) {                    
              accountValueId  
              date
              value
              rateToUserCurrency
              valueUserCurrency
              account {
                accountId
              }
            }
          }
        }
        `,
        variables: {
          accountValue: {
            accountValueId: accountValue.accountValueId,
            date: accountValue.date,
            value: accountValue.value,
            accountId: accountValue.account.accountId
          }
        },
      }
    });            
    
    // get back details of new account from database and add to local store
    if (response.data.data.accountValue_mutations.updateAccountValue != null) { 
      commit('updateAccountValue', response.data.data.accountValue_mutations.updateAccountValue)
      commit('sortAccountValues', accountValue.account.accountId)

      // get the balance and update in the store
      var id = accountValue.account.accountId
      var numOfValues = state.accounts[id].accountValues.length;
      var balance = state.accounts[id].accountValues[ numOfValues - 1 ].value;
      commit('updateAccountBalance', {accountId: id, balance: balance})
    }   
  } catch (error) {
    console.error(error); 
  }
}

// function updateAccountValues ({ commit }, payload) {
//   // Not implemented
//   // commit('updateAccountValues', payload)
// }

function sortAccountValues ({ commit }, accountId) {
  commit('sortAccountValues', accountId)
};

async function deleteAccountValues ({ commit }, payload) {
  console.log('deleteAccountValues actions payload:')
  console.log(payload)
  
  // send mutation to graphql with array of AccountValueIds to delete from db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($accountValueIds: [Int]!) {
          accountValue_mutations {
            deleteAccountValuesByIds(accountValueIds: $accountValueIds)
          }
        }
        `,
        variables: {
          accountValueIds: payload.accountValueIds
        },
      }
    });            
    
    // if delete from db was successful then delete also from local store
    if (response.data.data.accountValue_mutations.deleteAccountValuesByIds != null) {       
      commit('deleteAccountValues', payload)
    }   
  } catch (error) {
    console.error(error); 
  }
};

function updateSelectedAccountId({ commit }, accountId) {
  commit('updateSelectedAccountId', accountId)
}

function updateSelectedAccountCurrencySymbol({ commit }, symbol) {
  commit('updateSelectedAccountCurrencySymbol', symbol)
}

function updateTableColumn({ commit }, payload) {
  commit('updateTableColumn', payload)
}

function addToVisibleColumns({ commit }, columnName) {
  commit('addToVisibleColumns', columnName)
}

function removeFromVisibleColumns({ commit }, columnName) {
  commit('removeFromVisibleColumns', columnName)
}

export {
  resetState,
  initialiseAccounts,
  addAccount,
  updateAccount,
  deleteAccount,
  updateAccountBalance,
  updateAccountBalances,
  addAccountValue,
  updateAccountValue,
  // updateAccountValues,
  sortAccountValues,
  deleteAccountValues,
  updateSelectedAccountId,
  updateSelectedAccountCurrencySymbol,
  updateTableColumn,
  addToVisibleColumns,
  removeFromVisibleColumns
}