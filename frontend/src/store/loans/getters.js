
const getInitialFirstLoanId = state => {
  return Object.keys(state.loans)[1]
}

const loans = state => {
  var filtered = {}
  Object.assign(filtered, state.loans)
  for (var key in filtered) {
    if (key == 0) {
      delete filtered[key];
    }
  }
  return filtered
}

const loanById = state => (id) => {
  return state.loans[id]
}

const loanName = state => (id) => {
  var loan = state.loans[id]
  return loan.loanName
}

const loanValues = state => {
  return state.loanValues
}

const loanValueById = state => (id) => {
  return state.loanValues[id]
}

const loanValuesByLoanId = state => (id) => {  
  if (state.loans[id].hasOwnProperty('loanValues')) {
    return state.loans[id].loanValues
  } 
  else {
    return []
  }
}

const loanBalance = state => (loanId) => {
  try {
    var numOfValues = state.loans[loanId].loanValues.length;
    return state.loans[loanId].loanValues[ numOfValues - 1 ].value;
  }
  catch (error) {
    console.error(error); 
    return -1;
  }   
}

const loanTypes = state => {
  return state.loanTypes
}

const selectedLoanId = state => {
  return state.selectedLoanId
}

const selectedLoanCurrencySymbol = state => {
  return state.selectedLoanCurrencySymbol
}

const tableColumns = state => {
  return state.tableColumns
}

export {
  getInitialFirstLoanId,
  loans,
  loanById,
  loanName,
  loanValues,
  loanValueById,
  loanValuesByLoanId,
  loanBalance,
  loanTypes,
  selectedLoanId,
  selectedLoanCurrencySymbol,
  tableColumns
}