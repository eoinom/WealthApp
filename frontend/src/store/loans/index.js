import * as getters from './getters'
import * as mutations from './mutations'
import * as actions from './actions'

const getDefaultState = () => {
  return { 
    loanTypes: [
      'Mortgage',
      'Personal Loan', 
      'Student Loan',
      'Car Loan',         
      'Other Loan'
    ],
    loans: {
      '0': {
        loanId: 0,
        loanName: '',
        description: '',
        institution: '',
        type: '',
        startPrincipal: 0.00,
        startDate: '',
        totalTerm: 0,
        fixedTerm: 0,
        rateType: '',
        aprRate: 0.00,
        repaymentFrequency: '',
        repaymentAmount: 0.00,
        isActive: false,
        quotedCurrency: {
          code: '',
          nameLong: '',
          nameShort: ''
        },
        loanValues: [
          {
            loanValueId: 0,
            date: '',
            value: 0.00,
            rateToUserCurrency: 0.0000,
            valueUserCurrency: 0.00,
            loan: {
              loanId: 0
            }
          }
        ],
        balance: 0.00,
        balanceUserCurrency: 0.00
      }
    },    
    loanIds: [],
    initialFirstLoanId: 0,
    rateTypes: ['Fixed', 'Variable'],
    repaymentPeriods: ['Weekly', 'Fortnightly', 'Monthly', 'Quarterly', 'Bi-Annually', 'Annually'],
    selectedLoanId: 0,
    selectedLoanCurrencySymbol: '€',    
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
        label: 'Value (USD $)', 
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
    totalLoansBalances: []
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
