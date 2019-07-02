import Vue from 'vue'
import { isContext } from 'vm';

const getDefaultState = () => {
  return {
    authenticated: false,
    user: {
      userId: 0,
      email: '',
      firstName: '',
      lastName: '',
      newsletterSub: false,
      country: {},
      displayCurrency: {},
      bankAccounts: {}
    },
    bankAccounts: {
      '0': {
        bankAccountId: 0,
        accountName: '',
        description: '',
        institution: '',
        type: '',
        isActive: false,
        quotedCurrency: {
          code: '',
          nameLong: '',
          nameShort: ''
        },
        accountValues: [
          {
            accountValueId: 0,
            date: '',
            value: 0.00
          }
        ],
        balance: 0.00
      }
    },
    initialFirstBankAccountId: 0,
    bankAccountIds: [],
    bankAccountValues: {
      '0': {
        '0': {
          accountValueId: 0,
          date: '',
          value: 0.00
        }
      }
    },
    dateFormat: 'MM/DD/YYYY'
  }
}

// initial state (ref: https://tahazsh.com/vuebyte-reset-module-state)
const state = getDefaultState()

const mutations = {    
  resetState (state) {
    Object.assign(state, getDefaultState())
  },
  updateUser(state, user) {
    Object.assign(state.user, user)
  },
  updateAuth(state, authenticated) {
    state.authenticated = authenticated
  },
  // initialiseBankAccounts(state, bankAccounts) {    
  // },
  addBankAccount(state, bankAccount) {
    // first convert the date format of any Account Values to the user preferred format (state.dateFormat)
    if (bankAccount.hasOwnProperty('accountValues') && bankAccount.accountValues.length > 0) {
      
      // sort bankAccountValues
      if (bankAccount.accountValues.length > 1) {
        var accountValsSorted = bankAccount.accountValues.sort(function(a, b) {
          var date1 = new Date(a.date);
          var date2 = new Date(b.date);
          return date1 - date2;
        });
      }

      //Change dates into user preferred format (this really should be done only in UI input/output)
      bankAccount.accountValues.forEach(function(a) {     // ref: https://stackoverflow.com/questions/12482961/is-it-possible-to-change-values-of-the-array-when-doing-foreach-in-javascript
        var date = new Date(a.date);
        var dd = date.getDate();
        var mm = date.getMonth()+1;  // As January is 0
        var yyyy = date.getFullYear();
    
        if (dd < 10) 
          dd = '0' + dd;
        if (mm < 10) 
          mm = '0' + mm;
    
        switch(state.dateFormat) {
          case "DD-MM-YYYY":
            a.date = dd + '-' + mm + '-' + yyyy;
            break;
          case "DD/MM/YYYY":
            a.date = dd + '/' + mm + '/' + yyyy;
            break;
          case "MM-DD-YYYY":
            a.date = mm + '-' + dd + '-' + yyyy;
            break;
          case "MM/DD/YYYY":
            a.date = mm + '/' + dd + '/' + yyyy;
            break;
          case "YYYY-MM-DD":
            a.date = yyyy + '-' + mm + '-' + dd;
            break;
          case "YYYY/MM/DD":
            a.date = yyyy + '/' + mm + '/' + dd;
            break;
          default:
            // do nothing
        }
      });    
    }

    // Now add the account and store the accountId in a sorted array
    Vue.set(state.bankAccounts, bankAccount.bankAccountId, bankAccount)
    Vue.set(state.bankAccountIds, state.bankAccountIds.length, bankAccount.bankAccountId)
    state.bankAccountIds.sort(function(a, b){return a - b});
  },
  updateBankAccount(state, bankAccount) {    
    var id = bankAccount.bankAccountId
    Vue.set(state.bankAccounts[id], "accountName", bankAccount.accountName)
    Vue.set(state.bankAccounts[id], "description", bankAccount.description)
    Vue.set(state.bankAccounts[id], "institution", bankAccount.institution)
    Vue.set(state.bankAccounts[id], "type", bankAccount.type)
    Vue.set(state.bankAccounts[id], "isActive", bankAccount.isActive)
    Vue.set(state.bankAccounts[id], "quotedCurrency", bankAccount.quotedCurrency)
  },
  deleteBankAccount(state, bankAccountId) {     
    Vue.delete(state.bankAccounts, bankAccountId)

    // remove id from state.bankAccountIds array
    for (var i=0; i < state.bankAccountIds.length; i++) {
      if (state.bankAccountIds[i] === bankAccountId) {
        Vue.delete(state.bankAccountIds, i)
        break
      }
    }
  },
  updateBankAccountBalances(state) {
    // To do
  },
  updateBankAccountBalance(state, bankAccountId) {
    // To do
  },
  updateBankAccountValues(state, payload) {
    state.bankAccountValues[payload.bankAccountId] = payload.bankAccountValues
  },
  addBankAccountValue(state, payload) {
    var date = new Date(payload.accountValue.date);
    var dd = date.getDate();
    var mm = date.getMonth()+1;  // As January is 0
    var yyyy = date.getFullYear();

    if (dd < 10) 
      dd = '0' + dd;
    if (mm < 10) 
      mm = '0' + mm;

    switch(state.dateFormat) {
      case "DD-MM-YYYY":
        payload.accountValue.date = dd + '-' + mm + '-' + yyyy;
        break;
      case "DD/MM/YYYY":
        payload.accountValue.date = dd + '/' + mm + '/' + yyyy;
        break;
      case "MM-DD-YYYY":
        payload.accountValue.date = mm + '-' + dd + '-' + yyyy;
        break;
      case "MM/DD/YYYY":
        payload.accountValue.date = mm + '/' + dd + '/' + yyyy;
        break;
      case "YYYY-MM-DD":
        payload.accountValue.date = yyyy + '-' + mm + '-' + dd;
        break;
      case "YYYY/MM/DD":
        payload.accountValue.date = yyyy + '/' + mm + '/' + dd;
        break;
      default:
        // leave as is
    }
    var accountVals = state.bankAccounts[payload.bankAccountId].accountValues
    Vue.set(accountVals, accountVals.length, payload.accountValue)

    //sort the Account Values
    accountVals.sort(function(a, b) {
        var date1 = new Date();
        var date2 = new Date();
        var dd1, mm1, yyyy1, dd2, mm2, yyyy2;
        dd1 = mm1 = yyyy1 = dd2 = mm2 = yyyy2 = '';
        
        switch(state.dateFormat) {
          case "YYYY-MM-DD":
          case "MM/DD/YYYY":
            date1 = new Date(a.date);
            date2 = new Date(b.date);
            console.log('date1: ' + date1)
            console.log('date2: ' + date2)
            return date1 - date2;
            break;
          case "DD-MM-YYYY":
          case "DD/MM/YYYY":
            dd1 = a.date.slice(0,2)
            mm1 = a.date.slice(3,5)
            yyyy1 = a.date.slice(6,10)
            // dateA = new Date(yyyy + '-' + mm + '-' + dd);
            dd2 = b.date.slice(0,2)
            mm2 = b.date.slice(3,5)
            yyyy2 = b.date.slice(6,10)
            // dateB = new Date(yyyy + '-' + mm + '-' + dd);
            break;
          case "MM-DD-YYYY":
            mm1 = a.date.slice(0,2)
            dd1 = a.date.slice(3,5)
            yyyy1 = a.date.slice(6,10)
            // dateA = new Date(yyyy + '-' + mm + '-' + dd);
            mm2 = b.date.slice(0,2)
            dd2 = b.date.slice(3,5)
            yyyy2 = b.date.slice(6,10)
            // dateB = new Date(yyyy + '-' + mm + '-' + dd);
            break;
          case "YYYY/MM/DD":
            yyyy1 = a.date.slice(0,4)
            mm1 = a.date.slice(5,7)
            dd1 = a.date.slice(8,10)
            // dateA = new Date(yyyy + '-' + mm + '-' + dd);
            yyyy2 = b.date.slice(0,4)
            mm2 = b.date.slice(5,7)
            dd2 = b.date.slice(8,10)
            // dateB = new Date(yyyy + '-' + mm + '-' + dd);
            break;
          default:
            // do nothing
        }
        date1 = new Date(yyyy1 + '-' + mm1 + '-' + dd1);
        date2 = new Date(yyyy2 + '-' + mm2 + '-' + dd2);
        return date1 - date2;
      });
  },
  deleteValuesForBankAccount(state, accountId) {
    Vue.delete(state.bankAccountValues, accountId)
  },
  deleteBankAccountValue(state, accountId, accountValueId) {
    Vue.delete(state.bankAccountValues[accountId], accountValueId)
  },
  deleteBankAccountValues(state, payload) {
    console.log('deleteBankAccountValues mutation payload:')
    console.log(payload)
    for (var i = 0; i < Object.keys(payload.valueIds).length; i++) {
      var valueId = payload.valueIds[i].accountValueId;
      console.log('valueId:' + valueId);
      var accValuesArr = state.bankAccounts[payload.accountId].accountValues;
      console.log('accValuesArr length:' + accValuesArr.length);
      for (var j = 0; j < accValuesArr.length; j++) {
        if (accValuesArr[j].accountValueId == valueId) {
          Vue.delete(accValuesArr, j)
          break;
        } 
      }
    }
  },
  sortBankAccountValues(state, accountId) {
    state.bankAccounts[accountId].accountValues.sort(function(a, b) {
      var dateA = new Date(a.date);
      var dateB = new Date(b.date);
      return dateA - dateB;
    });    
  },
  setInitialFirstBankAccountId(state, accountId) {
    state.initialFirstBankAccountId = accountId
  }
}

const actions = {
    logout ({ commit }) {
        commit('resetState')
    },
    // login ({ commit }, payload) {
    //     commit('updateUser', payload.user)
    //     commit('initialiseBankAccounts', payload.bankAccounts)
    //     commit('initialiseAccountValues', payload.accountValues)
    //     commit('updateAccountBalances')
    // },
    updateUser({ commit }, user) {
        commit('updateUser', user)
        commit('updateAuth', true)
    },
    initialiseBankAccounts({ commit }, bankAccounts) {
        for (var i = 0; i < Object.keys(bankAccounts).length; i++) {
          var id = bankAccounts[i].bankAccountId
          if (i === 0) {
            commit('setInitialFirstBankAccountId', id)
          }
          commit('addBankAccount', bankAccounts[i])
        } 
    },

    async addBankAccount({ commit }, account) {
      console.log('account to add:')
      console.log(account)

      //sent mutation to graphql with account to add to db
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
    },

    async updateBankAccount({ commit }, account) {
      console.log('account to update:')
      console.log(account)

      //sent mutation to graphql with account to update in db
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
    },

    async deleteBankAccount({ commit, state, rootState, dispatch }, bankAccountId) {
      console.log('bankAccountId for deletion= ' + bankAccountId)

      //sent mutation to graphql with bankAccountId to delete from db
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
    },

    async addBankAccountValue({ commit }, accountValue) {
      console.log('account value to add:')
      console.log(accountValue)

      //sent mutation to graphql with accountValue to add to db
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
          commit('addBankAccountValue', 
            {bankAccountId: accountValue.accountId, accountValue: response.data.data.accountValue_mutations.addAccountValue})
        }   
      } catch (error) {
          console.error(error); 
      }
    },

    updateBankAccountValues({ commit }, payload) {
        commit('updateBankAccountValues', payload)
    },
    sortBankAccountValues({ commit }, accountId) {
        commit('sortBankAccountValues', accountId)
    },
    deleteBankAccountValues({ commit }, payload) {
        console.log('deleteBankAccountValues actions payload:')
        console.log(payload)
        commit('deleteBankAccountValues', payload)
    },
}

const getters = {
  authenticated: (state) => {
    return state.authenticated
  },
  user: (state) => {
    return state.user
  },
  userFullName: (state) => {
    return state.user.firstName + ' ' + state.user.lastName
  },
  userEmail: (state) => {
    return state.user.email
  },
  getInitialFirstBankAccountId: (state) => {
    return Object.keys(state.bankAccounts)[1]
  },
  bankAccounts: (state) => {
    var filtered = {}
    Object.assign(filtered, state.bankAccounts)
    for (var key in filtered) {
      if (key == 0) {
        delete filtered[key];
      }
    }
    return filtered
  },
  bankAccountById: (state) => (id) => {
    return state.bankAccounts[id]
  },
  bankAccountName: (state) => (id) => {
    var account = state.bankAccounts[id]
    return account.accountName
  },
  bankAccountValues: (state) => {
    return state.bankAccountValues
  },
  bankAccountValueById: (state) => (id) => {
    return state.bankAccountValues[id]
  },
  bankAccountValuesByAccountId: (state) => (id) => {
    console.log('id: ' + id)
    if (state.bankAccounts[id].hasOwnProperty('accountValues')) {
      return state.bankAccounts[id].accountValues
    } 
    else {
      return []
    }
  },
  getBankAccountBalance: (state) => (accountId) => {
    try {
      var numOfValues = state.bankAccounts[accountId].accountValues.length;
      // const accountValsArray = [...state.bankAccounts[accountId].accountValues];
      // var accountValsSorted = accountValsArray.sort(function(a, b) {
      //   var date1 = new Date();
      //   var date2 = new Date();
      //   var dd1, mm1, yyyy1, dd2, mm2, yyyy2;
      //   dd1 = mm1 = yyyy1 = dd2 = mm2 = yyyy2 = '';
        
      //   switch(state.dateFormat) {
      //     case "YYYY-MM-DD":
      //     case "MM/DD/YYYY":
      //       date1 = new Date(a.date);
      //       date2 = new Date(b.date);
      //       console.log('date1: ' + date1)
      //       console.log('date2: ' + date2)
      //       return date1 - date2;
      //       break;
      //     case "DD-MM-YYYY":
      //     case "DD/MM/YYYY":
      //       dd1 = a.date.slice(0,2)
      //       mm1 = a.date.slice(3,5)
      //       yyyy1 = a.date.slice(6,10)
      //       // dateA = new Date(yyyy + '-' + mm + '-' + dd);
      //       dd2 = b.date.slice(0,2)
      //       mm2 = b.date.slice(3,5)
      //       yyyy2 = b.date.slice(6,10)
      //       // dateB = new Date(yyyy + '-' + mm + '-' + dd);
      //       break;
      //     case "MM-DD-YYYY":
      //       mm1 = a.date.slice(0,2)
      //       dd1 = a.date.slice(3,5)
      //       yyyy1 = a.date.slice(6,10)
      //       // dateA = new Date(yyyy + '-' + mm + '-' + dd);
      //       mm2 = b.date.slice(0,2)
      //       dd2 = b.date.slice(3,5)
      //       yyyy2 = b.date.slice(6,10)
      //       // dateB = new Date(yyyy + '-' + mm + '-' + dd);
      //       break;
      //     case "YYYY/MM/DD":
      //       yyyy1 = a.date.slice(0,4)
      //       mm1 = a.date.slice(5,7)
      //       dd1 = a.date.slice(8,10)
      //       // dateA = new Date(yyyy + '-' + mm + '-' + dd);
      //       yyyy2 = b.date.slice(0,4)
      //       mm2 = b.date.slice(5,7)
      //       dd2 = b.date.slice(8,10)
      //       // dateB = new Date(yyyy + '-' + mm + '-' + dd);
      //       break;
      //     default:
      //       // do nothing
      //   }
      //   date1 = new Date(yyyy1 + '-' + mm1 + '-' + dd1);
      //   date2 = new Date(yyyy2 + '-' + mm2 + '-' + dd2);
      //   return date1 - date2;
      // });
      // return accountValsSorted[ numOfValues - 1 ].value;
      return state.bankAccounts[accountId].accountValues[ numOfValues - 1 ].value;
    }
    catch (error) {
      console.error(error); 
      return -1;
    }   
  },
  getDateFormat: (state) => {
    return state.dateFormat
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}