import Vue from 'vue'
import index from './index'

function resetState (state) {  
  // https://stackoverflow.com/questions/42295340/how-to-clear-state-in-vuex-store
  Object.assign(state, index.getDefaultState())
  // Vuex.store.replaceState({})
}

function updateUser (state, user) {
  Object.assign(state.user, user)
}

function updateAuth (state, authenticated) {
  state.authenticated = authenticated
}

function updateTotalNetWorthBalances (state, totalNetWorthBals) {
  state.totalNetWorthBalances = totalNetWorthBals
}


export {
  resetState,
  updateUser,
  updateAuth,
  updateTotalNetWorthBalances
}