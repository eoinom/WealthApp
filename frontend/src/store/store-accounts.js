import Vue from 'vue'

const getDefaultState = () => {
  return {
    accountTypes: [
      'Current', 
      'Savings', 
      'Investment', 
      'Other'
    ],
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
        label: 'Value (EUR €)', 
        field: row => row.value,
        // format: val => Number(val).toFixed(2),
        sortable: true 
      }
    ]
  }
}

const state = getDefaultState()

const mutations = {    
  resetState (state) {
    Object.assign(state, getDefaultState())
  },
  updateSelectedAccountId(state, accountId) {
    state.selectedAccountId = accountId
  },
  updateSelectedAccountCurrencySymbol(state, symbol) {
    state.selectedAccountCurrencySymbol = symbol
  },
  updateTableColumn(state, payload) {
    Object.assign(state.tableColumns[payload.columnNo], payload.columnObj)
  },
}

const actions = {
  updateSelectedAccountId({ commit }, accountId) {
    commit('updateSelectedAccountId', accountId)
  },
  updateSelectedAccountCurrencySymbol({ commit }, symbol) {
    commit('updateSelectedAccountCurrencySymbol', symbol)
  },
  updateTableColumn({ commit }, payload) {
    commit('updateTableColumn', payload)
  },
}

const getters = {
  accountTypes: (state) => {
    return state.accountTypes
  },
  selectedAccountId: (state) => {
    return state.selectedAccountId
  },
  selectedAccountCurrencySymbol: (state) => {
    return state.selectedAccountCurrencySymbol
  },  
  tableColumns: (state) => {
    return state.tableColumns
  },
}


export default {
  namespaced: true,
  state,
  mutations,
  actions,
  getters
}