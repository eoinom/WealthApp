
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
  try {
    var numOfValues = state.accounts[accountId].accountValues.length;
    return state.accounts[accountId].accountValues[ numOfValues - 1 ].value;
  }
  catch (error) {
    console.error(error); 
    return -1;
  }   
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

const tableColumns = state => {
  return state.tableColumns
}

export {
  getInitialFirstAccountId,
  accounts,
  accountById,
  accountName,
  accountValues,
  accountValueById,
  accountValuesByAccountId,
  accountBalance,
  accountTypes,
  selectedAccountId,
  selectedAccountCurrencySymbol,
  tableColumns
}