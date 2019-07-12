var moment = require('moment');


function resetState ({ commit }) {
  commit('resetState')
};

function initialiseAccounts ({ commit }, accounts) {
  for (var i = 0; i < Object.keys(accounts).length; i++) {
    var id = accounts[i].accountId
    if (i === 0) {
      commit('setInitialFirstAccountId', id)
    }
    commit('addAccount', accounts[i])
    commit('sortAccountValues', accounts[i].accountId)
  } 
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
      commit('sortLoanValues', accountValue.account.accountId)
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

export {
  resetState,
  initialiseAccounts,
  addAccount,
  updateAccount,
  deleteAccount,
  addAccountValue,
  updateAccountValue,
  // updateAccountValues,
  sortAccountValues,
  deleteAccountValues,
  updateSelectedAccountId,
  updateSelectedAccountCurrencySymbol,
  updateTableColumn
}