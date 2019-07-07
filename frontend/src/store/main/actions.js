
function logout ({ commit }) {
  commit('resetState')
};

// login ({ commit }, payload) {
//     commit('updateUser', payload.user)
//     commit('initialiseAccounts', payload.accounts)
//     commit('initialiseAccountValues', payload.accountValues)
//     commit('updateAccountBalances')
// }

function updateUser ({ commit }, user) {
  commit('updateUser', user)
  commit('updateAuth', true)
};


export {
  logout,
  // login,
  updateUser
}