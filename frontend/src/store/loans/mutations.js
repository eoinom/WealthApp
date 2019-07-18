import Vue from 'vue'
import index from './index'

function resetState (state) {  
  // https://stackoverflow.com/questions/42295340/how-to-clear-state-in-vuex-store
  Object.assign(state, index.getDefaultState())
  // Vuex.store.replaceState({})
}

function addLoan (state, loan) {   
  // Add the loan and store the loanId in a sorted array
  Vue.set(state.loans, loan.loanId, loan)
  Vue.set(state.loanIds, state.loanIds.length, loan.loanId)
  state.loanIds.sort(function(a, b){return a - b});
}

function updateLoan (state, loan) {    
  var id = loan.loanId
  Vue.set(state.loans[id], "loanName", loan.loanName)
  Vue.set(state.loans[id], "description", loan.description)
  Vue.set(state.loans[id], "institution", loan.institution)
  Vue.set(state.loans[id], "type", loan.type)
  Vue.set(state.loans[id], "isActive", loan.isActive)
  Vue.set(state.loans[id], "quotedCurrency", loan.quotedCurrency)
}

function deleteLoan (state, loanId) {     
  Vue.delete(state.loans, loanId)  
  // remove id from state.loanIds array
  for (var i=0; i < state.loanIds.length; i++) {
    if (state.loanIds[i] === loanId) {
      Vue.delete(state.loanIds, i)
      break
    }
  }
}

function updateLoanBalance (state, payload) {
  Vue.set(state.loans[payload.loanId], "balance", payload.balance)
  Vue.set(state.loans[payload.loanId], "balanceUserCurrency", payload.balanceUserCurrency)
}

function addLoanValue (state, loanValue) {
  var loanVals = state.loans[loanValue.loan.loanId].loanValues
  Vue.set(loanVals, loanVals.length, loanValue)
}

function updateLoanValue (state, loanValue) {      
  var loanVals = state.loans[loanValue.loan.loanId].loanValues
  var valueId = loanValue.loanValueId;
  for (var i = 0; i < loanVals.length; i++) {
    if (loanVals[i].loanValueId === valueId) {
      Vue.set(loanVals, i, loanValue)
      break;
    }
  }
}

// function updateLoanValues = (state, payload) {
//   // To Do
// }

function deleteValuesForLoan (state, loanId) {
  Vue.delete(state.loanValues, loanId)
}

function deleteLoanValue (state, loanId, loanValueId) {
  Vue.delete(state.loanValues[loanId], loanValueId)
}

function deleteLoanValues (state, payload) {
  console.log('deleteLoanValues mutation payload:')
  console.log(payload)
  for (var i = 0; i < Object.keys(payload.loanValueIds).length; i++) {
    var valueId = payload.loanValueIds[i];
    var valuesArr = state.loans[payload.loanId].loanValues;
    for (var j = 0; j < valuesArr.length; j++) {
      if (valuesArr[j].loanValueId === valueId) {
        Vue.delete(valuesArr, j)
        break;
      } 
    }
  }
}

function sortLoanValues (state, loanId) {
  state.loans[loanId].loanValues.sort(function(a, b) {
    var dateA = new Date(a.date);
    var dateB = new Date(b.date);
    return dateA - dateB;
  });    
}

function setInitialFirstLoanId (state, loanId) {
  state.initialFirstLoanId = loanId
}

function updateSelectedLoanId(state, loanId) {
  state.selectedLoanId = loanId
}

function updateSelectedLoanCurrencySymbol(state, symbol) {
  state.selectedLoanCurrencySymbol = symbol
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
  addLoan,
  updateLoan,
  deleteLoan,
  updateLoanBalance,
  addLoanValue,
  updateLoanValue,
  // updateLoanValues,
  deleteValuesForLoan,
  deleteLoanValue,
  deleteLoanValues,
  sortLoanValues,
  setInitialFirstLoanId,
  updateSelectedLoanId,
  updateSelectedLoanCurrencySymbol,
  updateTableColumn,
  addToVisibleColumns,
  removeFromVisibleColumns
}