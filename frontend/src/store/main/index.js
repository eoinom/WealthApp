import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return {
    authenticated: false,
    user: {
      userId: 0,
      email: '',
      firstName: '',
      lastName: '',
      newsletterSub: false,
      country: {},
      displayCurrency: {},
      bankAccounts: {}
    },
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
    initialFirstBankAccountId: 0,
    bankAccountIds: [],
    dateFormat: 'YYYY-MM-DD'
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
