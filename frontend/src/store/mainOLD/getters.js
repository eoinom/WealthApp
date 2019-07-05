
const authenticated = state => {
  return state.authenticated
}

const user = state => {
  return state.user
}

const userFullName = state => {
  return state.user.firstName + ' ' + state.user.lastName
}

const userEmail = state => {
  return state.user.email
}

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
  console.log('id: ' + id)
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

const getDateFormat = state => {
  return state.dateFormat
}

export {
  authenticated,
  user,
  userFullName,
  userEmail,
  getInitialFirstBankAccountId,
  bankAccounts,
  bankAccountById,
  bankAccountName,
  bankAccountValues,
  bankAccountValueById,
  bankAccountValuesByAccountId,
  getBankAccountBalance,
  getDateFormat
}