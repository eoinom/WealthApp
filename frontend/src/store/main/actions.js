
function logout ({ commit }) {
  commit('resetState')
};

// login ({ commit }, payload) {
//     commit('updateUser', payload.user)
//     commit('initialiseBankAccounts', payload.bankAccounts)
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