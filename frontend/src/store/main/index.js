import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return {
    authenticated: false, 
    countries: [ 'Australia', 'Canada', 'France', 'Germany', 'Ireland', 'Italy', 'Netherlands', 'Poland', 'Portugal', 'Spain', 'United Kingdomn', 'United States', 'New Zealand' ],   
    countryCodes: [ 'AU', 'CA', 'FR', 'DE', 'IE', 'IT', 'NL', 'PL', 'PT', 'ES', 'GB', 'US', 'NZ' ],   
    currencyCodes: [ 'AUD', 'CAD', 'EUR', 'GBP', 'PLN', 'USD', 'NZD' ],   
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
    },
    totalNetWorthBalances: [],
    getChart_XY_DataFromObj: function(obj) {
      let keys = Object.keys(obj)
      let values = Object.values(obj)
      let chartData = new Array(keys.length) // initialising the array size for increased performance
      for (var i = 0; i < keys.length; i++) {
        chartData[i] = {x: keys[i], y: values[i] }
      }
      return chartData
    },
    toLocaleFixed: function(num, decimals) {      
      return num.toLocaleString(undefined, {
        minimumFractionDigits: decimals,
        maximumFractionDigits: decimals
      })
    },
    toUserCurrency: function(num) {      
      return parseFloat(num).toLocaleString(undefined, {
        style: 'currency',
        currency: this.user.displayCurrency.code,
        currencyDisplay: 'symbol'
      })
    },
    toCurrency: function(num, currencyCode) {      
      return parseFloat(num).toLocaleString(undefined, {
        style: 'currency',
        currency: currencyCode,
        currencyDisplay: 'symbol'
      })
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
