import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return { 
    accountTypes: [
      'Current', 
      'Savings', 
      'Investment', 
      'Other'
    ],
    accounts: {
      '0': {
        accountId: 0,
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
            rateToUserCurrency: 0.0000,
            valueUserCurrency: 0.00,
            account: {
              accountId: 0
            }
          }
        ],
        balance: 0.00,
        balanceUserCurrency: 0.00
      }
    },    
    accountIds: [],
    currencyCodes: [ 'AUD', 'CAD', 'EUR', 'GBP', 'USD', 'NZD' ],
    initialFirstAccountId: 0,    
    selectedAccountId: 0,
    selectedAccountCurrencySymbol: '€',    
    tableColumns: [
      {
        name: 'date',
        required: true,
        label: 'Date',
        align: 'left',
        field: row => row.date,
        format: val => `${val}`,
        sortable: true
      },
      { 
        name: 'value', 
        align: 'center', 
        label: 'Balance (USD $)', 
        field: row => row.value,
        // format: val => Number(val).toFixed(2),
        sortable: true 
      },
      { 
        name: 'rateToUserCurrency', 
        align: 'center', 
        label: 'Rate', 
        field: row => row.rateToUserCurrency,
        // format: val => Number(val).toFixed(2),
        sortable: true 
      },
      { 
        name: 'valueUserCurrency', 
        align: 'center', 
        label: 'Value (EUR €)', 
        field: row => row.valueUserCurrency,
        // format: val => Number(val).toFixed(2),
        sortable: true 
      }
    ],
    visibleColumns: ['date', 'value'],
    totalAccountsBalances: []
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
