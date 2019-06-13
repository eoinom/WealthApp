import Vue from 'vue'

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
        },
        bankAccounts: {
            '0': {
                bankAccountId: 0,
                accountName: 'accountName',
                description: 'description',
                institution: 'institution',
                type: 'type',
                isActive: false,
                quotedCurrency: {},
                balance: 0.00
            }
        },
        bankAccountValues: {
            '0': {
                '0': {
                    accountValueId: 0,
                    date: "2000-01-01",
                    value: 1000.00
                }
            }
        }
    }
}

// initial state (ref: https://tahazsh.com/vuebyte-reset-module-state)
// **CHECK IF THIS IS REACTIVE, IF NOT LOOK AT: https://github.com/vuejs/vuex/issues/1118
const state = getDefaultState()

const mutations = {    
    resetState (state) {
        Object.assign(state, getDefaultState())
    },
    updateUser(state, user) {
        Object.assign(state.user, user)
    },
    updateAuth(state, authenticated) {
        state.authenticated = authenticated
    },
    initialiseBankAccounts(state, bankAccounts) {
        // Vue.delete(state.bankAccounts, 0)
        Object.assign(state.bankAccounts, bankAccounts)
        // for (var i = 0; i < bankAccounts.size(); i++) {
        //     Object.assign(state.bankAccounts[bankAccounts[i].bankAccountId], bankAccount[i])
        // }
        // for (var i = 0; i < bankAccounts.size(); i++) {
        //     commit('addBankAccount', bankAccounts[i])
        // }
    },
    addBankAccount(state, bankAccount) {
        Object.assign(state.bankAccounts[bankAccount.bankAccountId], bankAccount)
    },
    deleteBankAccount(state, bankAccountId) {
        // Vue.delete(state.bankAccounts, { where: bankAccountId = accountId })
        Vue.delete(state.bankAccounts, bankAccountId)
    },
    updateBankAccountBalances(state) {
        // To do
    },
    updateBankAccountBalance(state, bankAccountId) {
        // To do
    },
    updateBankAccountValues(state, payload) {
        // commit('deleteValuesForBankAccount', 0)
        console.log('In updateBankAccountValues')
        console.log('bankAccountId: ' + payload.bankAccountId)
        console.log('bankAccountValues: ')
        console.log(payload.bankAccountValues)
        state.bankAccountValues[payload.bankAccountId] = payload.bankAccountValues
    },
    addBankAccountValue(state, accountValue) {
        Object.assign(state.bankAccountValues[accountValue.accountValueId], accountValue)
    },
    deleteValuesForBankAccount(state, accountId) {
        // Vue.delete(state.bankAccounts, { where: bankAccountId = accountId })
        Vue.delete(state.bankAccountValues, accountId)
    },
    deleteBankAccountValue(state, accountId, accountValueId) {
        Vue.delete(state.bankAccountValues[accountId], accountValueId)
    },
}

const actions = {
    logout ({ commit }) {
        commit('resetState')
    },
    login ({ commit }, payload) {
        commit('updateUser', payload.user)
        commit('initialiseBankAccounts', payload.bankAccounts)
        commit('initialiseAccountValues', payload.accountValues)
        commit('updateAccountBalances')
    },
    updateUser({ commit }, user) {
        commit('updateUser', user)
        commit('updateAuth', true)
    },
    initialiseBankAccounts({ commit }, bankAccounts) {
        commit('initialiseBankAccounts', bankAccounts)
    },
    updateBankAccountValues({ commit }, payload) {
        commit('updateBankAccountValues', payload)
    },
}

const getters = {
    authenticated: (state) => {
        return state.authenticated
    },
    user: (state) => {
        return state.user
    },
    userFullName: (state) => {
        return state.user.firstName + ' ' + state.user.lastName
    },
    userEmail: (state) => {
        return state.user.email
    },
    bankAccounts: (state) => {
        return state.bankAccounts
    },
    bankAccountById: (state) => (id) => {
        return state.bankAccounts.find(x => x.bankAccountId === id)
    },
    bankAccountValues: (state) => {
        return state.bankAccountValues
    },
    bankAccountValueById: (state) => (id) => {
        return state.bankAccountValues.find(x => x.accountValueId === id)
    },
    bankAccountValuesByAccountId: (state) => (id) => {
        return state.bankAccountValues[id]
    },
}

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters
}