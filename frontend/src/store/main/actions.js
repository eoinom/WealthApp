
function logout ({ commit }) {
  commit('resetState')
};

// login ({ commit }, payload) {
//     commit('updateUser', payload.user)
//     commit('initialiseBankAccounts', payload.bankAccounts)
//     commit('initialiseAccountValues', payload.accountValues)
//     commit('updateAccountBalances')
// }

function updateUser ({ commit }, user) {
  commit('updateUser', user)
  commit('updateAuth', true)
};

function initialiseBankAccounts ({ commit }, bankAccounts) {
  for (var i = 0; i < Object.keys(bankAccounts).length; i++) {
    var id = bankAccounts[i].bankAccountId
    if (i === 0) {
      commit('setInitialFirstBankAccountId', id)
    }
    commit('addBankAccount', bankAccounts[i])
  } 
};

async function addBankAccount ({ commit, state }, account) {
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
        mutation ($account: BankAccountInputType!){
          bankAccount_mutations {
            addBankAccount(bankAccount: $account) {
              bankAccountId
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
          account: {
            accountName: account.accountName,
            description: account.description,
            type: account.type,
            institution: account.institution,
            isActive: account.isActive,                
            quotedCurrency: account.currencyCode,
            userId: state.user.userId
          }
        },
      }
    });            
    
    // get back details of new account from database and add to local store
    if (response.data.data.bankAccount_mutations.addBankAccount != null) {          
      commit('addBankAccount', response.data.data.bankAccount_mutations.addBankAccount)
    }   
  } catch (error) {
    console.error(error); 
  }
};

async function updateBankAccount ({ commit, state }, account) {
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
        mutation ($account: BankAccountInputType!){
          bankAccount_mutations {
            updateBankAccount(bankAccount: $account) {
              bankAccountId
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
          account: {
            bankAccountId: account.bankAccountId,
            accountName: account.accountName,
            description: account.description,
            type: account.type,
            institution: account.institution,
            isActive: account.isActive,                
            quotedCurrency: account.currencyCode,
            userId: state.user.userId
          }
        },
      }
    });            
    
    // get back details of amended account from database and update in local store
    if (response.data.data.bankAccount_mutations.updateBankAccount != null) {          
      commit('updateBankAccount', response.data.data.bankAccount_mutations.updateBankAccount)
    }   
  } catch (error) {
    console.error(error); 
  }
}

async function deleteBankAccount ({ commit, state, rootState, dispatch }, bankAccountId) {
  console.log('bankAccountId for deletion: ' + bankAccountId)
  
  //send mutation to graphql with bankAccountId to delete from db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($bankAccountId: ID!){
          bankAccount_mutations {
            deleteBankAccount(bankAccountId: $bankAccountId)
          }
        }
        `,
        variables: {
          bankAccountId: bankAccountId
        },
      }
    });            
    
    // if delete from db was successful then delete also from local store
    if (response.data.data.bankAccount_mutations.deleteBankAccount != null) {          
      commit('deleteBankAccount', bankAccountId)
    }   
  } catch (error) {
    console.error(error); 
  }
  
  // update the selectedAccountId
  var newSelectedAccId = 0
  if (state.bankAccountIds.length > 0) {
    newSelectedAccId = state.bankAccountIds[0]
  }
  var selectedId = rootState.accounts.selectedAccountId
  if (selectedId == bankAccountId) {
    dispatch('accounts/updateSelectedAccountId', newSelectedAccId, {root:true})
  }
}

async function addBankAccountValue ({ commit }, accountValue) {
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
              bankAccount {
                bankAccountId
              }
            }
          }
        }
        `,
        variables: {
          accountValue: {
            date: accountValue.date,
            value: accountValue.value,
            bankAccountId: accountValue.accountId
          }
        },
      }
    });            
    
    // get back details of new account from database and add to local store
    if (response.data.data.accountValue_mutations.addAccountValue != null) {       
      commit('addBankAccountValue', response.data.data.accountValue_mutations.addAccountValue)
    }   
  } catch (error) {
    console.error(error); 
  }
}

async function updateBankAccountValue ({ commit }, accountValue) {
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
              bankAccount {
                bankAccountId
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
            bankAccountId: accountValue.bankAccount.bankAccountId
          }
        },
      }
    });            
    
    // get back details of new account from database and add to local store
    if (response.data.data.accountValue_mutations.updateAccountValue != null) {       
      commit('updateBankAccountValue', response.data.data.accountValue_mutations.updateAccountValue)
    }   
  } catch (error) {
    console.error(error); 
  }
}

// function updateBankAccountValues ({ commit }, payload) {
//   // Not implemented
//   // commit('updateBankAccountValues', payload)
// }

function sortBankAccountValues ({ commit }, accountId) {
  commit('sortBankAccountValues', accountId)
};

async function deleteBankAccountValues ({ commit }, payload) {
  console.log('deleteBankAccountValues actions payload:')
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
          accountValueIds: payload.bankAccountValueIds
        },
      }
    });            
    
    // if delete from db was successful then delete also from local store
    if (response.data.data.accountValue_mutations.deleteAccountValuesByIds != null) {       
      commit('deleteBankAccountValues', payload)
    }   
  } catch (error) {
    console.error(error); 
  }
};

export {
  logout,
  // login,
  updateUser,
  initialiseBankAccounts,
  addBankAccount,
  updateBankAccount,
  deleteBankAccount,
  addBankAccountValue,
  updateBankAccountValue,
  // updateBankAccountValues,
  sortBankAccountValues,
  deleteBankAccountValues,
}