
const authenticated = state => {
  return state.authenticated
}

const currencyCodes = state => {
  return state.currencyCodes
}

const getChart_XY_DataFromObj = state => {
  return state.getChart_XY_DataFromObj()
}

const getDateFormat = state => {
  return state.dateFormat
}

const user = state => {
  console.log('state');
  console.log(state);  
  return state.user
}

const userDisplayCurrency = state => {
  return state.user.displayCurrency
}

const userDisplayCurrencyCode = state => {
  return state.user.displayCurrency.code
}

const userFullName = state => {
  return state.user.firstName + ' ' + state.user.lastName
}

const userEmail = state => {
  return state.user.email
}


export {
  authenticated,
  currencyCodes,
  getChart_XY_DataFromObj,
  getDateFormat,
  user,
  userDisplayCurrency,
  userDisplayCurrencyCode,
  userFullName,
  userEmail  
}