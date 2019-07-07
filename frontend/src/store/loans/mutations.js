import Vue from 'vue'
import index from './index'

function resetState (state) {  
  // https://stackoverflow.com/questions/42295340/how-to-clear-state-in-vuex-store
  Object.assign(state, index.getDefaultState())
  // Vuex.store.replaceState({})
}

function addAccount (state, account) {
  // first convert the date format of any Account Values to the user preferred format (state.dateFormat)
  if (account.hasOwnProperty('accountValues') && account.accountValues.length > 0) {
    
    // sort accountValues
    if (account.accountValues.length > 1) {
      var accountValsSorted = account.accountValues.sort(function(a, b) {
        var date1 = new Date(a.date);
        var date2 = new Date(b.date);
        return date1 - date2;
      });
    }
    
    //Change dates into user preferred format (this really should be done only in UI input/output)
    account.accountValues.forEach(function(a) {     // ref: https://stackoverflow.com/questions/12482961/is-it-possible-to-change-values-of-the-array-when-doing-foreach-in-javascript
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
  Vue.set(state.accounts, account.accountId, account)
  Vue.set(state.accountIds, state.accountIds.length, account.accountId)
  state.accountIds.sort(function(a, b){return a - b});
}

function updateAccount (state, account) {    
  var id = account.accountId
  Vue.set(state.accounts[id], "accountName", account.accountName)
  Vue.set(state.accounts[id], "description", account.description)
  Vue.set(state.accounts[id], "institution", account.institution)
  Vue.set(state.accounts[id], "type", account.type)
  Vue.set(state.accounts[id], "isActive", account.isActive)
  Vue.set(state.accounts[id], "quotedCurrency", account.quotedCurrency)
}

function deleteAccount (state, accountId) {     
  Vue.delete(state.accounts, accountId)
  
  // remove id from state.accountIds array
  for (var i=0; i < state.accountIds.length; i++) {
    if (state.accountIds[i] === accountId) {
      Vue.delete(state.accountIds, i)
      break
    }
  }
}

// function updateAccountBalance = (state, accountId) {
//   // To do
// }

// function updateAccountBalances = (state) {
//   // To do
// }

function addAccountValue (state, accountValue) {
  var date = new Date(accountValue.date);
  var dd = date.getDate();
  var mm = date.getMonth()+1;  // As January is 0
  var yyyy = date.getFullYear();
  
  if (dd < 10) 
  dd = '0' + dd;
  if (mm < 10) 
  mm = '0' + mm;
  
  switch(state.dateFormat) {
    case "DD-MM-YYYY":
    accountValue.date = dd + '-' + mm + '-' + yyyy;
    break;
    case "DD/MM/YYYY":
    accountValue.date = dd + '/' + mm + '/' + yyyy;
    break;
    case "MM-DD-YYYY":
    accountValue.date = mm + '-' + dd + '-' + yyyy;
    break;
    case "MM/DD/YYYY":
    accountValue.date = mm + '/' + dd + '/' + yyyy;
    break;
    case "YYYY-MM-DD":
    accountValue.date = yyyy + '-' + mm + '-' + dd;
    break;
    case "YYYY/MM/DD":
    accountValue.date = yyyy + '/' + mm + '/' + dd;
    break;
    default:
    // leave as is
  }
  var accountVals = state.accounts[accountValue.account.accountId].accountValues
  Vue.set(accountVals, accountVals.length, accountValue)
  
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
      return date1 - date2;
      break;
      case "DD-MM-YYYY":
      case "DD/MM/YYYY":
      dd1 = a.date.slice(0,2)
      mm1 = a.date.slice(3,5)
      yyyy1 = a.date.slice(6,10)
      dd2 = b.date.slice(0,2)
      mm2 = b.date.slice(3,5)
      yyyy2 = b.date.slice(6,10)
      break;
      case "MM-DD-YYYY":
      mm1 = a.date.slice(0,2)
      dd1 = a.date.slice(3,5)
      yyyy1 = a.date.slice(6,10)
      mm2 = b.date.slice(0,2)
      dd2 = b.date.slice(3,5)
      yyyy2 = b.date.slice(6,10)
      break;
      case "YYYY/MM/DD":
      yyyy1 = a.date.slice(0,4)
      mm1 = a.date.slice(5,7)
      dd1 = a.date.slice(8,10)
      yyyy2 = b.date.slice(0,4)
      mm2 = b.date.slice(5,7)
      dd2 = b.date.slice(8,10)
      break;
      default:
      // do nothing
    }
    date1 = new Date(yyyy1 + '-' + mm1 + '-' + dd1);
    date2 = new Date(yyyy2 + '-' + mm2 + '-' + dd2);
    return date1 - date2;
  });
}

function updateAccountValue (state, accountValue) {    
  var date = new Date(accountValue.date);
  var dd = date.getDate();
  var mm = date.getMonth()+1;  // As January is 0
  var yyyy = date.getFullYear();
  
  if (dd < 10) 
  dd = '0' + dd;
  if (mm < 10) 
  mm = '0' + mm;
  
  switch(state.dateFormat) {
    case "DD-MM-YYYY":
    accountValue.date = dd + '-' + mm + '-' + yyyy;
    break;
    case "DD/MM/YYYY":
    accountValue.date = dd + '/' + mm + '/' + yyyy;
    break;
    case "MM-DD-YYYY":
    accountValue.date = mm + '-' + dd + '-' + yyyy;
    break;
    case "MM/DD/YYYY":
    accountValue.date = mm + '/' + dd + '/' + yyyy;
    break;
    case "YYYY-MM-DD":
    accountValue.date = yyyy + '-' + mm + '-' + dd;
    break;
    case "YYYY/MM/DD":
    accountValue.date = yyyy + '/' + mm + '/' + dd;
    break;
    default:
    // leave as is
  }
  
  var accountVals = state.accounts[accountValue.account.accountId].accountValues
  var valueId = accountValue.accountValueId;
  for (var i = 0; i < accountVals.length; i++) {
    if (accountVals[i].accountValueId === valueId) {
      Vue.set(accountVals, i, accountValue)
      
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
          return date1 - date2;
          break;
          case "DD-MM-YYYY":
          case "DD/MM/YYYY":
          dd1 = a.date.slice(0,2)
          mm1 = a.date.slice(3,5)
          yyyy1 = a.date.slice(6,10)
          dd2 = b.date.slice(0,2)
          mm2 = b.date.slice(3,5)
          yyyy2 = b.date.slice(6,10)
          break;
          case "MM-DD-YYYY":
          mm1 = a.date.slice(0,2)
          dd1 = a.date.slice(3,5)
          yyyy1 = a.date.slice(6,10)
          mm2 = b.date.slice(0,2)
          dd2 = b.date.slice(3,5)
          yyyy2 = b.date.slice(6,10)
          break;
          case "YYYY/MM/DD":
          yyyy1 = a.date.slice(0,4)
          mm1 = a.date.slice(5,7)
          dd1 = a.date.slice(8,10)
          yyyy2 = b.date.slice(0,4)
          mm2 = b.date.slice(5,7)
          dd2 = b.date.slice(8,10)
          break;
          default:
          // do nothing
        }
        date1 = new Date(yyyy1 + '-' + mm1 + '-' + dd1);
        date2 = new Date(yyyy2 + '-' + mm2 + '-' + dd2);
        return date1 - date2;
      });
      break;
    }
  }
}

// function updateAccountValues = (state, payload) {
//   // To Do
// }

function deleteValuesForAccount (state, accountId) {
  Vue.delete(state.accountValues, accountId)
}

function deleteAccountValue (state, accountId, accountValueId) {
  Vue.delete(state.accountValues[accountId], accountValueId)
}

function deleteAccountValues (state, payload) {
  console.log('deleteAccountValues mutation payload:')
  console.log(payload)
  for (var i = 0; i < Object.keys(payload.accountValueIds).length; i++) {
    var valueId = payload.accountValueIds[i];
    console.log('valueId:' + valueId);
    var accValuesArr = state.accounts[payload.accountId].accountValues;
    console.log('accValuesArr length:' + accValuesArr.length);
    for (var j = 0; j < accValuesArr.length; j++) {
      if (accValuesArr[j].accountValueId === valueId) {
        Vue.delete(accValuesArr, j)
        break;
      } 
    }
  }
}

function sortAccountValues (state, accountId) {
  state.accounts[accountId].accountValues.sort(function(a, b) {
    var dateA = new Date(a.date);
    var dateB = new Date(b.date);
    return dateA - dateB;
  });    
}

function setInitialFirstaccountId (state, accountId) {
  state.initialFirstaccountId = accountId
}

function updateSelectedAccountId(state, accountId) {
  state.selectedAccountId = accountId
}

function updateSelectedAccountCurrencySymbol(state, symbol) {
  state.selectedAccountCurrencySymbol = symbol
}

function updateTableColumn(state, payload) {
  Object.assign(state.tableColumns[payload.columnNo], payload.columnObj)
}

export {
  resetState,
  addAccount,
  updateAccount,
  deleteAccount,
  // updateAccountBalance,
  // updateAccountBalances,
  addAccountValue,
  updateAccountValue,
  // updateAccountValues,
  deleteValuesForAccount,
  deleteAccountValue,
  deleteAccountValues,
  sortAccountValues,
  setInitialFirstaccountId,
  updateSelectedAccountId,
  updateSelectedAccountCurrencySymbol,
  updateTableColumn
}