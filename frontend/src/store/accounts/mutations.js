import Vue from 'vue'
import index from './index'

function resetState (state) {  
  // https://stackoverflow.com/questions/42295340/how-to-clear-state-in-vuex-store
  Object.assign(state, index.getDefaultState())
  // Vuex.store.replaceState({})
}

function addAccount (state, account) {
  // Add the account and store the accountId in a sorted array
  Vue.set(state.accounts, account.accountId, account)

  // Check if array already contains this accountId
  var hasId = false
  state.accountIds.forEach(id => {
    if (id === account.accountId) {
      hasId = true
    }
  });
  if (!hasId) {
    Vue.set(state.accountIds, state.accountIds.length, account.accountId)
    state.accountIds.sort(function(a, b){return a - b});
  }
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

function updateAccountBalance (state, payload) {
  Vue.set(state.accounts[payload.accountId], "balance", payload.balance)
  Vue.set(state.accounts[payload.accountId], "balanceUserCurrency", payload.balanceUserCurrency)
}

function updateTotalAccountsBalances (state, payload) {
  state.totalAccountsBalances = payload
}

function addAccountValue (state, accountValue) {
  var accountVals = state.accounts[accountValue.account.accountId].accountValues
  Vue.set(accountVals, accountVals.length, accountValue)  
}

function updateAccountValue (state, accountValue) {      
  var accountVals = state.accounts[accountValue.account.accountId].accountValues
  var valueId = accountValue.accountValueId;
  for (var i = 0; i < accountVals.length; i++) {
    if (accountVals[i].accountValueId === valueId) {
      Vue.set(accountVals, i, accountValue)      
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
    var valuesArr = state.accounts[payload.accountId].accountValues;
    for (var j = 0; j < valuesArr.length; j++) {
      if (valuesArr[j].accountValueId === valueId) {
        Vue.delete(valuesArr, j)
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

function setInitialFirstAccountId (state, accountId) {
  state.initialFirstAccountId = accountId
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

function addToVisibleColumns(state, columnName) {
  if (!state.visibleColumns.includes(columnName)) {
    state.visibleColumns.push(columnName)
  }
}

function removeFromVisibleColumns(state, columnName) {
  if (state.visibleColumns.includes(columnName)) {
    Vue.delete(state.visibleColumns, state.visibleColumns.indexOf(columnName))
  }
}

export {
  resetState,
  addAccount,
  updateAccount,
  deleteAccount,
  updateAccountBalance,
  updateTotalAccountsBalances,
  addAccountValue,
  updateAccountValue,
  // updateAccountValues,
  deleteValuesForAccount,
  deleteAccountValue,
  deleteAccountValues,
  sortAccountValues,
  setInitialFirstAccountId,
  updateSelectedAccountId,
  updateSelectedAccountCurrencySymbol,
  updateTableColumn,
  addToVisibleColumns,
  removeFromVisibleColumns
}