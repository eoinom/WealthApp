
const authenticated = state => {
  return state.state.authenticated
}

const user = state => {
  console.log('state.state.user');
  console.log(state.state.user);
  
  return state.state.user
}

const userFullName = state => {
  return state.state.user.firstName + ' ' + state.state.user.lastName
}

const userEmail = state => {
  return state.state.user.email
}

const getInitialFirstBankAccountId = state => {
  return Object.keys(state.state.bankAccounts)[1]
}

const bankAccounts = state => {
  var filtered = {}
  Object.assign(filtered, state.state.bankAccounts)
  for (var key in filtered) {
    if (key == 0) {
      delete filtered[key];
    }
  }
  return filtered
}

const bankAccountById = state => (id) => {
  return state.state.bankAccounts[id]
}

const bankAccountName = state => (id) => {
  var account = state.state.bankAccounts[id]
  return account.accountName
}

const bankAccountValues = state => {
  return state.state.bankAccountValues
}

const bankAccountValueById = state => (id) => {
  return state.state.bankAccountValues[id]
}

const bankAccountValuesByAccountId = state => (id) => {
  if (state.state.bankAccounts[id].hasOwnProperty('accountValues')) {
    return state.state.bankAccounts[id].accountValues
  } 
  else {
    return []
  }
}

const getBankAccountBalance = state => (accountId) => {
  try {
    var numOfValues = state.state.bankAccounts[accountId].accountValues.length;
    return state.state.bankAccounts[accountId].accountValues[ numOfValues - 1 ].value;
  }
  catch (error) {
    console.error(error); 
    return -1;
  }   
}

const getDateFormat = state => {
  return state.state.dateFormat
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