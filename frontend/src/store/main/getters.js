
const authenticated = state => {
  return state.authenticated
}

const currencyCodes = state => {
  return state.currencyCodes
}

const getDateFormat = state => {
  return state.dateFormat
}

const user = state => {
  console.log('state');
  console.log(state);  
  return state.user
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
  getDateFormat,
  user,
  userFullName,
  userEmail  
}