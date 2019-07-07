import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return {
    authenticated: false,    
    bankAccounts: {
      '0': {
        bankAccountId: 0,
        accountName: '',
        description: '',
        institution: '',
        type: '',
        isActive: false,
        quotedCurrency: {
          code: '',
          nameLong: '',
          nameShort: ''
        },
        accountValues: [
          {
            accountValueId: 0,
            date: '',
            value: 0.00,
            bankAccount: {
              bankAccountId: 0
            }
          }
        ],
        // balance: 0.00  // Not implemented yet
      }
    },    
    bankAccountIds: [],
    currencyCodes: [
      'AUD',
      'CAD',
      'EUR', 
      'GBP', 
      'USD',
      'NZD'
    ],
    dateFormat: 'YYYY-MM-DD',
    initialFirstBankAccountId: 0,
    user: {
      userId: 0,
      email: '',
      firstName: '',
      lastName: '',
      newsletterSub: false,
      country: {},
      displayCurrency: {},
      bankAccounts: {}
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
