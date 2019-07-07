import Vue from 'vue'
import Vuex from 'vuex'

import main from './main'
import accounts from './store-accounts'
import liabilities from './store-liabilities'

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
      liabilities 
    },

    // enable strict mode (adds overhead!)
    // for dev mode only
    strict: process.env.DEV
  })

  return Store
}
