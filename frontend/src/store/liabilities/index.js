import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return { 
    liabilityTypes: [
      'Credit Card', 
      'Mortgage',
      'Personal Loan', 
      'Student Loan',
      'Car Loan',         
      'Other'
    ],
    liabilities: {
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
    liabilityIds: [],
    initialFirstLiabilityId: 0,    
    selectedLiabilityId: 0,
    selectedLiabilityCurrencySymbol: '€',    
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
        label: 'Value (EUR €)', 
        field: row => row.value,
        // format: val => Number(val).toFixed(2),
        sortable: true 
      }
    ]
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
