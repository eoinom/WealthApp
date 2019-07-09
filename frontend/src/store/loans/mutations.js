import Vue from 'vue'
import index from './index'

function resetState (state) {  
  // https://stackoverflow.com/questions/42295340/how-to-clear-state-in-vuex-store
  Object.assign(state, index.getDefaultState())
  // Vuex.store.replaceState({})
}

function addLoan (state, loan) {
  // first convert the date format of any Loan Values to the user preferred format (state.dateFormat)
  if (loan.hasOwnProperty('loanValues') && loan.loanValues.length > 0) {
    
    // sort loanValues
    if (loan.loanValues.length > 1) {
      var loanValsSorted = loan.loanValues.sort(function(a, b) {
        var date1 = new Date(a.date);
        var date2 = new Date(b.date);
        return date1 - date2;
      });
    }
    
    //Change dates into user preferred format (this really should be done only in UI input/output)
    loan.loanValues.forEach(function(a) {     // ref: https://stackoverflow.com/questions/12482961/is-it-possible-to-change-values-of-the-array-when-doing-foreach-in-javascript
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
  
  // Now add the loan and store the loanId in a sorted array
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

// function updateLoanBalance = (state, loanId) {
//   // To do
// }

// function updateLoanBalances = (state) {
//   // To do
// }

function addLoanValue (state, loanValue) {
  var date = new Date(loanValue.date);
  var dd = date.getDate();
  var mm = date.getMonth()+1;  // As January is 0
  var yyyy = date.getFullYear();
  
  if (dd < 10) 
  dd = '0' + dd;
  if (mm < 10) 
  mm = '0' + mm;
  
  switch(state.dateFormat) {
    case "DD-MM-YYYY":
    loanValue.date = dd + '-' + mm + '-' + yyyy;
    break;
    case "DD/MM/YYYY":
    loanValue.date = dd + '/' + mm + '/' + yyyy;
    break;
    case "MM-DD-YYYY":
    loanValue.date = mm + '-' + dd + '-' + yyyy;
    break;
    case "MM/DD/YYYY":
    loanValue.date = mm + '/' + dd + '/' + yyyy;
    break;
    case "YYYY-MM-DD":
    loanValue.date = yyyy + '-' + mm + '-' + dd;
    break;
    case "YYYY/MM/DD":
    loanValue.date = yyyy + '/' + mm + '/' + dd;
    break;
    default:
    // leave as is
  }
  var loanVals = state.loans[loanValue.loan.loanId].loanValues
  Vue.set(loanVals, loanVals.length, loanValue)
  
  //sort the Loan Values
  loanVals.sort(function(a, b) {
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

function updateLoanValue (state, loanValue) {    
  var date = new Date(loanValue.date);
  var dd = date.getDate();
  var mm = date.getMonth()+1;  // As January is 0
  var yyyy = date.getFullYear();
  
  if (dd < 10) 
  dd = '0' + dd;
  if (mm < 10) 
  mm = '0' + mm;
  
  switch(state.dateFormat) {
    case "DD-MM-YYYY":
    loanValue.date = dd + '-' + mm + '-' + yyyy;
    break;
    case "DD/MM/YYYY":
    loanValue.date = dd + '/' + mm + '/' + yyyy;
    break;
    case "MM-DD-YYYY":
    loanValue.date = mm + '-' + dd + '-' + yyyy;
    break;
    case "MM/DD/YYYY":
    loanValue.date = mm + '/' + dd + '/' + yyyy;
    break;
    case "YYYY-MM-DD":
    loanValue.date = yyyy + '-' + mm + '-' + dd;
    break;
    case "YYYY/MM/DD":
    loanValue.date = yyyy + '/' + mm + '/' + dd;
    break;
    default:
    // leave as is
  }
  
  var loanVals = state.loans[loanValue.loan.loanId].loanValues
  var valueId = loanValue.loanValueId;
  for (var i = 0; i < loanVals.length; i++) {
    if (loanVals[i].loanValueId === valueId) {
      Vue.set(loanVals, i, loanValue)
      
      //sort the Loan Values
      loanVals.sort(function(a, b) {
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
    console.log('valueId:' + valueId);
    var accValuesArr = state.loans[payload.loanId].loanValues;
    console.log('accValuesArr length:' + accValuesArr.length);
    for (var j = 0; j < accValuesArr.length; j++) {
      if (accValuesArr[j].loanValueId === valueId) {
        Vue.delete(accValuesArr, j)
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

function setInitialFirstloanId (state, loanId) {
  state.initialFirstloanId = loanId
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

export {
  resetState,
  addLoan,
  updateLoan,
  deleteLoan,
  // updateLoanBalance,
  // updateLoanBalances,
  addLoanValue,
  updateLoanValue,
  // updateLoanValues,
  deleteValuesForLoan,
  deleteLoanValue,
  deleteLoanValues,
  sortLoanValues,
  setInitialFirstloanId,
  updateSelectedLoanId,
  updateSelectedLoanCurrencySymbol,
  updateTableColumn
}