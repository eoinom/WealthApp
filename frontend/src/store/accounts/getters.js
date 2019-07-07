
const getInitialFirstBankAccountId = state => {
  return Object.keys(state.bankAccounts)[1]
}

const bankAccounts = state => {
  var filtered = {}
  Object.assign(filtered, state.bankAccounts)
  for (var key in filtered) {
    if (key == 0) {
      delete filtered[key];
    }
  }
  return filtered
}

const bankAccountById = state => (id) => {
  return state.bankAccounts[id]
}

const bankAccountName = state => (id) => {
  var account = state.bankAccounts[id]
  return account.accountName
}

const bankAccountValues = state => {
  return state.bankAccountValues
}

const bankAccountValueById = state => (id) => {
  return state.bankAccountValues[id]
}

const bankAccountValuesByAccountId = state => (id) => {
  if (state.bankAccounts[id].hasOwnProperty('accountValues')) {
    return state.bankAccounts[id].accountValues
  } 
  else {
    return []
  }
}

const getBankAccountBalance = state => (accountId) => {
  try {
    var numOfValues = state.bankAccounts[accountId].accountValues.length;
    return state.bankAccounts[accountId].accountValues[ numOfValues - 1 ].value;
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
  getInitialFirstBankAccountId,
  bankAccounts,
  bankAccountById,
  bankAccountName,
  bankAccountValues,
  bankAccountValueById,
  bankAccountValuesByAccountId,
  getBankAccountBalance,
  accountTypes,
  selectedAccountId,
  selectedAccountCurrencySymbol,
  tableColumns
}