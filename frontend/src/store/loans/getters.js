
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

const loanIds = state => {
  return state.loanIds
}

const loanIdsWithVals = state => {
  let loanIdsWithVals = []
  for (var i = 0; i < state.loanIds.length; i++) {
    let id = state.loanIds[i]
    if (state.loans[id].hasOwnProperty('loanValues') && state.loans[id].loanValues.length > 0) {
      loanIdsWithVals.push(id)
    }
  }
  return loanIdsWithVals
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
  if (state.loans[loanId].loanValues.length === 0) {
    return -1
  }
  return state.loans[loanId].balance
}

const loanBalanceUserCurrency = state => (loanId) => {
  if (state.loans[loanId].loanValues.length === 0) {
    return -1
  }
  return state.loans[loanId].balanceUserCurrency
}

const loanTypes = state => {
  return state.loanTypes
}

const rateTypes = state => {
  return state.rateTypes
}

const repaymentPeriods = state => {
  return state.repaymentPeriods
}

const selectedLoanId = state => {
  return state.selectedLoanId
}

const selectedLoanCurrencySymbol = state => {
  return state.selectedLoanCurrencySymbol
}

const totalLoansBalances = state => {
  return state.totalLoansBalances
}

const tableColumns = state => {
  return state.tableColumns
}

const visibleColumns = state => {
  return state.visibleColumns
}

export {
  getInitialFirstLoanId,
  loans,
  loanById,
  loanIds,
  loanIdsWithVals,
  loanName,
  loanValues,
  loanValueById,
  loanValuesByLoanId,
  loanBalance,
  loanBalanceUserCurrency,
  loanTypes,
  rateTypes,
  repaymentPeriods,
  selectedLoanId,
  selectedLoanCurrencySymbol,
  totalLoansBalances,
  tableColumns,
  visibleColumns
}