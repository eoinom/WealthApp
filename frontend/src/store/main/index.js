import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return {
    authenticated: false, 
    currencyCodes: [ 'AUD', 'CAD', 'EUR', 'GBP', 'USD', 'NZD' ],   
    dateFormat: 'DD/MM/YYYY',
    user: {
      userId: 0,
      email: '',
      firstName: '',
      lastName: '',
      newsletterSub: false,
      country: {},
      displayCurrency: {
        code: 'EUR',
        nameShort: 'Euro',
        nameLong: 'Euro'
      },
      accounts: {},
      loans: {}
    }
  }
}

// initial state (https://stackoverflow.com/questions/42295340/how-to-clear-state-in-vuex-store?answertab=votes#tab-top)
const state = getDefaultState()

export default {
  namespaced: true,
  state,
  getters,
  mutations,
  actions,
  getDefaultState
}
