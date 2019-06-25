import Vue from 'vue'

const getDefaultState = () => {
  return {
    selectedAccountId: 0,
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
        // field: 'value',
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
  updateTableColumn(state, payload) {
    Object.assign(state.tableColumns[payload.columnNo], payload.columnObj)
  },
}

const actions = {
  updateSelectedAccountId({ commit }, accountId) {
    commit('updateSelectedAccountId', accountId)
  },
  updateTableColumn({ commit }, payload) {
    commit('updateTableColumn', payload)
  },
}

const getters = {
  selectedAccountId: (state) => {
    return state.selectedAccountId
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