
const getInitialFirstAccountId = state => {
  return Object.keys(state.accounts)[1]
}

const accounts = state => {
  var filtered = {}
  Object.assign(filtered, state.accounts)
  for (var key in filtered) {
    if (key == 0) {
      delete filtered[key];
    }
  }
  return filtered
}

const accountById = state => (id) => {
  return state.accounts[id]
}

const accountIds = state => {
  return state.accountIds
}

const accountIdsWithVals = state => {
  let accountIdsWithVals = []
  for (var i = 0; i < state.accountIds.length; i++) {
    let id = state.accountIds[i]
    if (state.accounts[id].hasOwnProperty('accountValues') && state.accounts[id].accountValues.length > 0) {
      accountIdsWithVals.push(id)
    }
  }
  return accountIdsWithVals
}

const accountName = state => (id) => {
  var account = state.accounts[id]
  return account.accountName
}

const accountValues = state => {
  return state.accountValues
}

const accountValueById = state => (id) => {
  return state.accountValues[id]
}

const accountValuesByAccountId = state => (id) => {
  if (state.accounts[id].hasOwnProperty('accountValues')) {
    return state.accounts[id].accountValues
  } 
  else {
    return []
  }
}

const accountBalance = state => (accountId) => {
  if (state.accounts[accountId].accountValues.length === 0) {
    return -1
  }
  return state.accounts[accountId].balance
}

const accountBalanceUserCurrency = state => (accountId) => { 
  console.log('state.accounts[accountId].accountValues.length:');
  console.log(state.accounts[accountId].accountValues.length);
  
  if (state.accounts[accountId].accountValues.length === 0) {
    return -1
  }
  return state.accounts[accountId].balanceUserCurrency
}

const accountTypes = state => {
  return state.accountTypes
}

const selectedAccountId = state => {
  return state.selectedAccountId
}

const selectedAccountCurrencySymbol = state => {
  return state.selectedAccountCurrencySymbol
}

const totalAccountsBalances = state => {
  return state.totalAccountsBalances
}

const tableColumns = state => {
  return state.tableColumns
}

const visibleColumns = state => {
  return state.visibleColumns
}

export {
  getInitialFirstAccountId,
  accounts,
  accountById,
  accountIds,
  accountIdsWithVals,
  accountName,
  accountValues,
  accountValueById,
  accountValuesByAccountId,
  accountBalance,
  accountBalanceUserCurrency,
  accountTypes,
  selectedAccountId,
  selectedAccountCurrencySymbol,
  totalAccountsBalances,
  tableColumns,
  visibleColumns
}