import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'

import main from './main'
import accounts from './accounts'
import loans from './loans'

Vue.use(Vuex)

/*
 * If not building with SSR mode, you can
 * directly export the Store instantiation
 */

 
export default function (/* { ssrContext } */) {
  const Store = new Vuex.Store({
    modules: {
      main,
      accounts,
      loans 
    },
    // plugins: [createPersistedState()],
    plugins: [createPersistedState({
      key: 'vuex',
      reducer (val) {                           // https://github.com/robinvdvleuten/vuex-persistedstate/issues/88
        if(val.main.authenticated === false) {
          return {}
        }
        return val
      }
    })],

    // enable strict mode (adds overhead!)
    // for dev mode only
    strict: process.env.DEV
  })

  return Store
}
