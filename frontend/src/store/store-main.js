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
                currencyCode: 'EUR',
                balance: 0.00
            }
        },
        accountValues: {
            '0': {
                accountValueId: 0,
                date: "2000-01-01",
                value: 1000.00,
                bankAccountId: 0
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
    initialiseAccountValues(state, accountValues) {
        commit('deleteAccountValue', 0)
        Object.assign(state.accountValues, accountValues)
    },
    addAccountValue(state, accountValue) {
        Object.assign(state.accountValues[accountValue.accountValueId], accountValue)
    },
    deleteAccountValue(state, accountValueId) {
        // Vue.delete(state.bankAccounts, { where: bankAccountId = accountId })
        Vue.delete(state.accountValues, accountValueId)
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
    accountValues: (state) => {
        return state.accountValues
    },
    accountValueById: (state) => (id) => {
        return state.accountValues.find(x => x.accountValueId === id)
    },
    accountValuesByAccountId: (state) => (id) => {
        return state.accountValues.where(x => x.bankAccountId === id)
    },
}

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters
}