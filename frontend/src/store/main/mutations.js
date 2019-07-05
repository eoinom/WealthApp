import Vue from 'vue'

function resetState (state) {
  Object.assign(state, getDefaultState())
}

function updateUser (state, user) {
  console.log('user: ')
  console.log(user)
  console.log('state: ')
  console.log(state)
  console.log('state.user: ')
  console.log(state.user)

  Object.assign(state.user, user)
}

function updateAuth (state, authenticated) {
  state.authenticated = authenticated
}

function addBankAccount (state, bankAccount) {
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
}

function updateBankAccount (state, bankAccount) {    
  var id = bankAccount.bankAccountId
  Vue.set(state.bankAccounts[id], "accountName", bankAccount.accountName)
  Vue.set(state.bankAccounts[id], "description", bankAccount.description)
  Vue.set(state.bankAccounts[id], "institution", bankAccount.institution)
  Vue.set(state.bankAccounts[id], "type", bankAccount.type)
  Vue.set(state.bankAccounts[id], "isActive", bankAccount.isActive)
  Vue.set(state.bankAccounts[id], "quotedCurrency", bankAccount.quotedCurrency)
}

function deleteBankAccount (state, bankAccountId) {     
  Vue.delete(state.bankAccounts, bankAccountId)
  
  // remove id from state.bankAccountIds array
  for (var i=0; i < state.bankAccountIds.length; i++) {
    if (state.bankAccountIds[i] === bankAccountId) {
      Vue.delete(state.bankAccountIds, i)
      break
    }
  }
}

// function updateBankAccountBalance = (state, bankAccountId) {
//   // To do
// }

// function updateBankAccountBalances = (state) {
//   // To do
// }

function addBankAccountValue (state, accountValue) {
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
  var accountVals = state.bankAccounts[accountValue.bankAccount.bankAccountId].accountValues
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

function updateBankAccountValue (state, accountValue) {    
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
  
  var accountVals = state.bankAccounts[accountValue.bankAccount.bankAccountId].accountValues
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

// function updateBankAccountValues = (state, payload) {
//   // To Do
// }

function deleteValuesForBankAccount (state, accountId) {
  Vue.delete(state.bankAccountValues, accountId)
}

function deleteBankAccountValue (state, accountId, accountValueId) {
  Vue.delete(state.bankAccountValues[accountId], accountValueId)
}

function deleteBankAccountValues (state, payload) {
  console.log('deleteBankAccountValues mutation payload:')
  console.log(payload)
  for (var i = 0; i < Object.keys(payload.bankAccountValueIds).length; i++) {
    var valueId = payload.bankAccountValueIds[i];
    console.log('valueId:' + valueId);
    var accValuesArr = state.bankAccounts[payload.bankAccountId].accountValues;
    console.log('accValuesArr length:' + accValuesArr.length);
    for (var j = 0; j < accValuesArr.length; j++) {
      if (accValuesArr[j].accountValueId === valueId) {
        Vue.delete(accValuesArr, j)
        break;
      } 
    }
  }
}

function sortBankAccountValues (state, accountId) {
  state.bankAccounts[accountId].accountValues.sort(function(a, b) {
    var dateA = new Date(a.date);
    var dateB = new Date(b.date);
    return dateA - dateB;
  });    
}

function setInitialFirstBankAccountId (state, accountId) {
  state.initialFirstBankAccountId = accountId
}

export {
  resetState,
  updateUser,
  updateAuth,
  addBankAccount,
  updateBankAccount,
  deleteBankAccount,
  // updateBankAccountBalance,
  // updateBankAccountBalances,
  addBankAccountValue,
  updateBankAccountValue,
  // updateBankAccountValues,
  deleteValuesForBankAccount,
  deleteBankAccountValue,
  deleteBankAccountValues,
  sortBankAccountValues,
  setInitialFirstBankAccountId
}