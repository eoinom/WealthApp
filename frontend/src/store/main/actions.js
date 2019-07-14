var moment = require('moment');

function formatDate_Iso2User({ state }, date) {         
  return moment(date, "YYYY-MM-DD").format(state.dateFormat)
}

function formatDate_User2Iso({ state }, date) {        
  return moment(date, state.dateFormat).format("YYYY-MM-DD")
}

function logout ({ commit }) {
  commit('resetState')
}

// login ({ commit }, payload) {
//     commit('updateUser', payload.user)
//     commit('initialiseAccounts', payload.accounts)
//     commit('initialiseAccountValues', payload.accountValues)
//     commit('updateAccountBalances')
// }

function updateUser ({ commit }, user) {
  commit('updateUser', user)
  commit('updateAuth', true)
}


export {
  formatDate_Iso2User,
  formatDate_User2Iso,
  logout,
  // login,
  updateUser
}