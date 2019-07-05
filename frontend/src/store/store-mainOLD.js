// import Vue from 'vue'
// import { isContext } from 'vm';

// const getDefaultState = () => {
//   return {
//     authenticated: false,
//     user: {
//       userId: 0,
//       email: '',
//       firstName: '',
//       lastName: '',
//       newsletterSub: false,
//       country: {},
//       displayCurrency: {},
//       bankAccounts: {}
//     },
//     bankAccounts: {
//       '0': {
//         bankAccountId: 0,
//         accountName: '',
//         description: '',
//         institution: '',
//         type: '',
//         isActive: false,
//         quotedCurrency: {
//           code: '',
//           nameLong: '',
//           nameShort: ''
//         },
//         accountValues: [
//           {
//             accountValueId: 0,
//             date: '',
//             value: 0.00,
//             bankAccount: {
//               bankAccountId: 0
//             }
//           }
//         ],
//         // balance: 0.00  // Not implemented yet
//       }
//     },
//     initialFirstBankAccountId: 0,
//     bankAccountIds: [],
//     dateFormat: 'YYYY-MM-DD'
//   }
// }

// // initial state (ref: https://tahazsh.com/vuebyte-reset-module-state)
// const state = getDefaultState()

// const mutations = {    
//   resetState (state) {
//     Object.assign(state, getDefaultState())
//   },
//   updateUser(state, user) {
//     Object.assign(state.user, user)
//   },
//   updateAuth(state, authenticated) {
//     state.authenticated = authenticated
//   },
//   // initialiseBankAccounts(state, bankAccounts) {    
//   // },
//   addBankAccount(state, bankAccount) {
//     // first convert the date format of any Account Values to the user preferred format (state.dateFormat)
//     if (bankAccount.hasOwnProperty('accountValues') && bankAccount.accountValues.length > 0) {
      
//       // sort bankAccountValues
//       if (bankAccount.accountValues.length > 1) {
//         var accountValsSorted = bankAccount.accountValues.sort(function(a, b) {
//           var date1 = new Date(a.date);
//           var date2 = new Date(b.date);
//           return date1 - date2;
//         });
//       }

//       //Change dates into user preferred format (this really should be done only in UI input/output)
//       bankAccount.accountValues.forEach(function(a) {     // ref: https://stackoverflow.com/questions/12482961/is-it-possible-to-change-values-of-the-array-when-doing-foreach-in-javascript
//         var date = new Date(a.date);
//         var dd = date.getDate();
//         var mm = date.getMonth()+1;  // As January is 0
//         var yyyy = date.getFullYear();
    
//         if (dd < 10) 
//           dd = '0' + dd;
//         if (mm < 10) 
//           mm = '0' + mm;
    
//         switch(state.dateFormat) {
//           case "DD-MM-YYYY":
//             a.date = dd + '-' + mm + '-' + yyyy;
//             break;
//           case "DD/MM/YYYY":
//             a.date = dd + '/' + mm + '/' + yyyy;
//             break;
//           case "MM-DD-YYYY":
//             a.date = mm + '-' + dd + '-' + yyyy;
//             break;
//           case "MM/DD/YYYY":
//             a.date = mm + '/' + dd + '/' + yyyy;
//             break;
//           case "YYYY-MM-DD":
//             a.date = yyyy + '-' + mm + '-' + dd;
//             break;
//           case "YYYY/MM/DD":
//             a.date = yyyy + '/' + mm + '/' + dd;
//             break;
//           default:
//             // do nothing
//         }
//       });    
//     }

//     // Now add the account and store the accountId in a sorted array
//     Vue.set(state.bankAccounts, bankAccount.bankAccountId, bankAccount)
//     Vue.set(state.bankAccountIds, state.bankAccountIds.length, bankAccount.bankAccountId)
//     state.bankAccountIds.sort(function(a, b){return a - b});
//   },
//   updateBankAccount(state, bankAccount) {    
//     var id = bankAccount.bankAccountId
//     Vue.set(state.bankAccounts[id], "accountName", bankAccount.accountName)
//     Vue.set(state.bankAccounts[id], "description", bankAccount.description)
//     Vue.set(state.bankAccounts[id], "institution", bankAccount.institution)
//     Vue.set(state.bankAccounts[id], "type", bankAccount.type)
//     Vue.set(state.bankAccounts[id], "isActive", bankAccount.isActive)
//     Vue.set(state.bankAccounts[id], "quotedCurrency", bankAccount.quotedCurrency)
//   },
//   deleteBankAccount(state, bankAccountId) {     
//     Vue.delete(state.bankAccounts, bankAccountId)

//     // remove id from state.bankAccountIds array
//     for (var i=0; i < state.bankAccountIds.length; i++) {
//       if (state.bankAccountIds[i] === bankAccountId) {
//         Vue.delete(state.bankAccountIds, i)
//         break
//       }
//     }
//   },
//   updateBankAccountBalances(state) {
//     // To do
//   },
//   updateBankAccountBalance(state, bankAccountId) {
//     // To do
//   },  
//   addBankAccountValue(state, accountValue) {
//     var date = new Date(accountValue.date);
//     var dd = date.getDate();
//     var mm = date.getMonth()+1;  // As January is 0
//     var yyyy = date.getFullYear();

//     if (dd < 10) 
//       dd = '0' + dd;
//     if (mm < 10) 
//       mm = '0' + mm;

//     switch(state.dateFormat) {
//       case "DD-MM-YYYY":
//         accountValue.date = dd + '-' + mm + '-' + yyyy;
//         break;
//       case "DD/MM/YYYY":
//         accountValue.date = dd + '/' + mm + '/' + yyyy;
//         break;
//       case "MM-DD-YYYY":
//         accountValue.date = mm + '-' + dd + '-' + yyyy;
//         break;
//       case "MM/DD/YYYY":
//         accountValue.date = mm + '/' + dd + '/' + yyyy;
//         break;
//       case "YYYY-MM-DD":
//         accountValue.date = yyyy + '-' + mm + '-' + dd;
//         break;
//       case "YYYY/MM/DD":
//         accountValue.date = yyyy + '/' + mm + '/' + dd;
//         break;
//       default:
//         // leave as is
//     }
//     var accountVals = state.bankAccounts[accountValue.bankAccount.bankAccountId].accountValues
//     Vue.set(accountVals, accountVals.length, accountValue)

//     //sort the Account Values
//     accountVals.sort(function(a, b) {
//         var date1 = new Date();
//         var date2 = new Date();
//         var dd1, mm1, yyyy1, dd2, mm2, yyyy2;
//         dd1 = mm1 = yyyy1 = dd2 = mm2 = yyyy2 = '';
        
//         switch(state.dateFormat) {
//           case "YYYY-MM-DD":
//           case "MM/DD/YYYY":
//             date1 = new Date(a.date);
//             date2 = new Date(b.date);
//             return date1 - date2;
//             break;
//           case "DD-MM-YYYY":
//           case "DD/MM/YYYY":
//             dd1 = a.date.slice(0,2)
//             mm1 = a.date.slice(3,5)
//             yyyy1 = a.date.slice(6,10)
//             dd2 = b.date.slice(0,2)
//             mm2 = b.date.slice(3,5)
//             yyyy2 = b.date.slice(6,10)
//             break;
//           case "MM-DD-YYYY":
//             mm1 = a.date.slice(0,2)
//             dd1 = a.date.slice(3,5)
//             yyyy1 = a.date.slice(6,10)
//             mm2 = b.date.slice(0,2)
//             dd2 = b.date.slice(3,5)
//             yyyy2 = b.date.slice(6,10)
//             break;
//           case "YYYY/MM/DD":
//             yyyy1 = a.date.slice(0,4)
//             mm1 = a.date.slice(5,7)
//             dd1 = a.date.slice(8,10)
//             yyyy2 = b.date.slice(0,4)
//             mm2 = b.date.slice(5,7)
//             dd2 = b.date.slice(8,10)
//             break;
//           default:
//             // do nothing
//         }
//         date1 = new Date(yyyy1 + '-' + mm1 + '-' + dd1);
//         date2 = new Date(yyyy2 + '-' + mm2 + '-' + dd2);
//         return date1 - date2;
//       });
//   },
//   updateBankAccountValue(state, accountValue) {    
//     var date = new Date(accountValue.date);
//     var dd = date.getDate();
//     var mm = date.getMonth()+1;  // As January is 0
//     var yyyy = date.getFullYear();

//     if (dd < 10) 
//       dd = '0' + dd;
//     if (mm < 10) 
//       mm = '0' + mm;

//     switch(state.dateFormat) {
//       case "DD-MM-YYYY":
//         accountValue.date = dd + '-' + mm + '-' + yyyy;
//         break;
//       case "DD/MM/YYYY":
//         accountValue.date = dd + '/' + mm + '/' + yyyy;
//         break;
//       case "MM-DD-YYYY":
//         accountValue.date = mm + '-' + dd + '-' + yyyy;
//         break;
//       case "MM/DD/YYYY":
//         accountValue.date = mm + '/' + dd + '/' + yyyy;
//         break;
//       case "YYYY-MM-DD":
//         accountValue.date = yyyy + '-' + mm + '-' + dd;
//         break;
//       case "YYYY/MM/DD":
//         accountValue.date = yyyy + '/' + mm + '/' + dd;
//         break;
//       default:
//         // leave as is
//     }

//     var accountVals = state.bankAccounts[accountValue.bankAccount.bankAccountId].accountValues
//     var valueId = accountValue.accountValueId;
//     for (var i = 0; i < accountVals.length; i++) {
//       if (accountVals[i].accountValueId === valueId) {
//         Vue.set(accountVals, i, accountValue)

//         //sort the Account Values
//         accountVals.sort(function(a, b) {
//           var date1 = new Date();
//           var date2 = new Date();
//           var dd1, mm1, yyyy1, dd2, mm2, yyyy2;
//           dd1 = mm1 = yyyy1 = dd2 = mm2 = yyyy2 = '';
          
//           switch(state.dateFormat) {
//             case "YYYY-MM-DD":
//             case "MM/DD/YYYY":
//               date1 = new Date(a.date);
//               date2 = new Date(b.date);
//               return date1 - date2;
//               break;
//             case "DD-MM-YYYY":
//             case "DD/MM/YYYY":
//               dd1 = a.date.slice(0,2)
//               mm1 = a.date.slice(3,5)
//               yyyy1 = a.date.slice(6,10)
//               dd2 = b.date.slice(0,2)
//               mm2 = b.date.slice(3,5)
//               yyyy2 = b.date.slice(6,10)
//               break;
//             case "MM-DD-YYYY":
//               mm1 = a.date.slice(0,2)
//               dd1 = a.date.slice(3,5)
//               yyyy1 = a.date.slice(6,10)
//               mm2 = b.date.slice(0,2)
//               dd2 = b.date.slice(3,5)
//               yyyy2 = b.date.slice(6,10)
//               break;
//             case "YYYY/MM/DD":
//               yyyy1 = a.date.slice(0,4)
//               mm1 = a.date.slice(5,7)
//               dd1 = a.date.slice(8,10)
//               yyyy2 = b.date.slice(0,4)
//               mm2 = b.date.slice(5,7)
//               dd2 = b.date.slice(8,10)
//               break;
//             default:
//               // do nothing
//           }
//           date1 = new Date(yyyy1 + '-' + mm1 + '-' + dd1);
//           date2 = new Date(yyyy2 + '-' + mm2 + '-' + dd2);
//           return date1 - date2;
//         });
//         break;
//       }
//     }
//   },
//   updateBankAccountValues(state, payload) {
//     // To Do
//   },
//   deleteValuesForBankAccount(state, accountId) {
//     Vue.delete(state.bankAccountValues, accountId)
//   },
//   deleteBankAccountValue(state, accountId, accountValueId) {
//     Vue.delete(state.bankAccountValues[accountId], accountValueId)
//   },
//   deleteBankAccountValues(state, payload) {
//     console.log('deleteBankAccountValues mutation payload:')
//     console.log(payload)
//     for (var i = 0; i < Object.keys(payload.bankAccountValueIds).length; i++) {
//       var valueId = payload.bankAccountValueIds[i];
//       console.log('valueId:' + valueId);
//       var accValuesArr = state.bankAccounts[payload.bankAccountId].accountValues;
//       console.log('accValuesArr length:' + accValuesArr.length);
//       for (var j = 0; j < accValuesArr.length; j++) {
//         if (accValuesArr[j].accountValueId === valueId) {
//           Vue.delete(accValuesArr, j)
//           break;
//         } 
//       }
//     }
//   },
//   sortBankAccountValues(state, accountId) {
//     state.bankAccounts[accountId].accountValues.sort(function(a, b) {
//       var dateA = new Date(a.date);
//       var dateB = new Date(b.date);
//       return dateA - dateB;
//     });    
//   },
//   setInitialFirstBankAccountId(state, accountId) {
//     state.initialFirstBankAccountId = accountId
//   }
// }

// const actions = {
//   logout ({ commit }) {
//       commit('resetState')
//   },
//   // login ({ commit }, payload) {
//   //     commit('updateUser', payload.user)
//   //     commit('initialiseBankAccounts', payload.bankAccounts)
//   //     commit('initialiseAccountValues', payload.accountValues)
//   //     commit('updateAccountBalances')
//   // },
//   updateUser({ commit }, user) {
//       commit('updateUser', user)
//       commit('updateAuth', true)
//   },
//   initialiseBankAccounts({ commit }, bankAccounts) {
//       for (var i = 0; i < Object.keys(bankAccounts).length; i++) {
//         var id = bankAccounts[i].bankAccountId
//         if (i === 0) {
//           commit('setInitialFirstBankAccountId', id)
//         }
//         commit('addBankAccount', bankAccounts[i])
//       } 
//   },

//   async addBankAccount({ commit }, account) {
//     console.log('account to add:')
//     console.log(account)

//     //send mutation to graphql with account to add to db
//     const axios = require("axios");
//     try {
//       var response = await axios({
//         method: "POST",
//         url: "/",
//         data: {
//           query: `                    
//             mutation ($account: BankAccountInputType!){
//               bankAccount_mutations {
//                 addBankAccount(bankAccount: $account) {
//                   bankAccountId
//                   accountName
//                   description
//                   type
//                   institution
//                   isActive
//                   quotedCurrency {
//                     code
//                     nameLong
//                     nameShort
//                   }
//                 }
//               }
//             }
//           `,
//           variables: {
//             account: {
//               accountName: account.accountName,
//               description: account.description,
//               type: account.type,
//               institution: account.institution,
//               isActive: account.isActive,                
//               quotedCurrency: account.currencyCode,
//               userId: state.user.userId
//             }
//           },
//         }
//       });            
      
//       // get back details of new account from database and add to local store
//       if (response.data.data.bankAccount_mutations.addBankAccount != null) {          
//         commit('addBankAccount', response.data.data.bankAccount_mutations.addBankAccount)
//       }   
//     } catch (error) {
//         console.error(error); 
//     }
//   },

//   async updateBankAccount({ commit }, account) {
//     console.log('account to update:')
//     console.log(account)

//     //send mutation to graphql with account to update in db
//     const axios = require("axios");
//     try {
//       var response = await axios({
//         method: "POST",
//         url: "/",
//         data: {
//           query: `                    
//             mutation ($account: BankAccountInputType!){
//               bankAccount_mutations {
//                 updateBankAccount(bankAccount: $account) {
//                   bankAccountId
//                   accountName
//                   description
//                   type
//                   institution
//                   isActive
//                   quotedCurrency {
//                     code
//                     nameLong
//                     nameShort
//                   }
//                 }
//               }
//             }
//           `,
//           variables: {
//             account: {
//               bankAccountId: account.bankAccountId,
//               accountName: account.accountName,
//               description: account.description,
//               type: account.type,
//               institution: account.institution,
//               isActive: account.isActive,                
//               quotedCurrency: account.currencyCode,
//               userId: state.user.userId
//             }
//           },
//         }
//       });            
      
//       // get back details of amended account from database and update in local store
//       if (response.data.data.bankAccount_mutations.updateBankAccount != null) {          
//         commit('updateBankAccount', response.data.data.bankAccount_mutations.updateBankAccount)
//       }   
//     } catch (error) {
//         console.error(error); 
//     }
//   },

//   async deleteBankAccount({ commit, state, rootState, dispatch }, bankAccountId) {
//     console.log('bankAccountId for deletion: ' + bankAccountId)

//     //send mutation to graphql with bankAccountId to delete from db
//     const axios = require("axios");
//     try {
//       var response = await axios({
//         method: "POST",
//         url: "/",
//         data: {
//           query: `                    
//             mutation ($bankAccountId: ID!){
//               bankAccount_mutations {
//                 deleteBankAccount(bankAccountId: $bankAccountId)
//               }
//             }
//           `,
//           variables: {
//             bankAccountId: bankAccountId
//           },
//         }
//       });            
      
//       // if delete from db was successful then delete also from local store
//       if (response.data.data.bankAccount_mutations.deleteBankAccount != null) {          
//         commit('deleteBankAccount', bankAccountId)
//       }   
//     } catch (error) {
//         console.error(error); 
//     }
    
//     // update the selectedAccountId
//     var newSelectedAccId = 0
//     if (state.bankAccountIds.length > 0) {
//       newSelectedAccId = state.bankAccountIds[0]
//     }
//     var selectedId = rootState.accounts.selectedAccountId
//     if (selectedId == bankAccountId) {
//       dispatch('accounts/updateSelectedAccountId', newSelectedAccId, {root:true})
//     }
//   },

//   async addBankAccountValue({ commit }, accountValue) {
//     console.log('account value to add:')
//     console.log(accountValue)

//     //send mutation to graphql with accountValue to add to db
//     const axios = require("axios");
//     try {
//       var response = await axios({
//         method: "POST",
//         url: "/",
//         data: {
//           query: `                    
//             mutation ($accountValue: AccountValueInputType!){
//               accountValue_mutations {
//                 addAccountValue(accountValue: $accountValue) {                    
//                   accountValueId  
//                   date
//                   value
//                   bankAccount {
//                     bankAccountId
//                   }
//                 }
//               }
//             }
//           `,
//           variables: {
//             accountValue: {
//               date: accountValue.date,
//               value: accountValue.value,
//               bankAccountId: accountValue.bankAccountId
//             }
//           },
//         }
//       });            
      
//       // get back details of new account from database and add to local store
//       if (response.data.data.accountValue_mutations.addAccountValue != null) {       
//         commit('addBankAccountValue', response.data.data.accountValue_mutations.addAccountValue)
//       }   
//     } catch (error) {
//         console.error(error); 
//     }
//   },

//   async updateBankAccountValue({ commit }, accountValue) {
//     console.log('account value to update:')
//     console.log(accountValue)

//     //send mutation to graphql with accountValue to add to db
//     const axios = require("axios");
//     try {
//       var response = await axios({
//         method: "POST",
//         url: "/",
//         data: {
//           query: `                    
//             mutation ($accountValue: AccountValueInputType!){
//               accountValue_mutations {
//                 updateAccountValue(accountValue: $accountValue) {                    
//                   accountValueId  
//                   date
//                   value
//                   bankAccount {
//                     bankAccountId
//                   }
//                 }
//               }
//             }
//           `,
//           variables: {
//             accountValue: {
//               accountValueId: accountValue.accountValueId,
//               date: accountValue.date,
//               value: accountValue.value,
//               bankAccountId: accountValue.bankAccount.bankAccountId
//             }
//           },
//         }
//       });            
      
//       // get back details of new account from database and add to local store
//       if (response.data.data.accountValue_mutations.updateAccountValue != null) {       
//         commit('updateBankAccountValue', response.data.data.accountValue_mutations.updateAccountValue)
//       }   
//     } catch (error) {
//         console.error(error); 
//     }
//   },

//   updateBankAccountValues({ commit }, payload) {
//       // Not implemented
//       // commit('updateBankAccountValues', payload)
//   },
//   sortBankAccountValues({ commit }, accountId) {
//       commit('sortBankAccountValues', accountId)
//   },
//   async deleteBankAccountValues({ commit }, payload) {
//     console.log('deleteBankAccountValues actions payload:')
//     console.log(payload)

//     // send mutation to graphql with array of AccountValueIds to delete from db
//     const axios = require("axios");
//     try {
//       var response = await axios({
//         method: "POST",
//         url: "/",
//         data: {
//           query: `                    
//             mutation ($accountValueIds: [Int]!) {
//               accountValue_mutations {
//                 deleteAccountValuesByIds(accountValueIds: $accountValueIds)
//               }
//             }
//           `,
//           variables: {
//             accountValueIds: payload.bankAccountValueIds
//           },
//         }
//       });            
      
//       // if delete from db was successful then delete also from local store
//       if (response.data.data.accountValue_mutations.deleteAccountValuesByIds != null) {       
//         commit('deleteBankAccountValues', payload)
//       }   
//     } catch (error) {
//         console.error(error); 
//     }
//   },
// }

// const getters = {
//   authenticated: (state) => {
//     return state.authenticated
//   },
//   user: (state) => {
//     return state.user
//   },
//   userFullName: (state) => {
//     return state.user.firstName + ' ' + state.user.lastName
//   },
//   userEmail: (state) => {
//     return state.user.email
//   },
//   getInitialFirstBankAccountId: (state) => {
//     return Object.keys(state.bankAccounts)[1]
//   },
//   bankAccounts: (state) => {
//     var filtered = {}
//     Object.assign(filtered, state.bankAccounts)
//     for (var key in filtered) {
//       if (key == 0) {
//         delete filtered[key];
//       }
//     }
//     return filtered
//   },
//   bankAccountById: (state) => (id) => {
//     return state.bankAccounts[id]
//   },
//   bankAccountName: (state) => (id) => {
//     var account = state.bankAccounts[id]
//     return account.accountName
//   },
//   bankAccountValues: (state) => {
//     return state.bankAccountValues
//   },
//   bankAccountValueById: (state) => (id) => {
//     return state.bankAccountValues[id]
//   },
//   bankAccountValuesByAccountId: (state) => (id) => {
//     console.log('id: ' + id)
//     if (state.bankAccounts[id].hasOwnProperty('accountValues')) {
//       return state.bankAccounts[id].accountValues
//     } 
//     else {
//       return []
//     }
//   },
//   getBankAccountBalance: (state) => (accountId) => {
//     try {
//       var numOfValues = state.bankAccounts[accountId].accountValues.length;
//       return state.bankAccounts[accountId].accountValues[ numOfValues - 1 ].value;
//     }
//     catch (error) {
//       console.error(error); 
//       return -1;
//     }   
//   },
//   getDateFormat: (state) => {
//     return state.dateFormat
//   }
// }

// export default {
//   namespaced: true,
//   state,
//   mutations,
//   actions,
//   getters
// }