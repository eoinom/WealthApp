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
            value: 0.00
          }
        ],
        balance: 0.00
      }
    },
    initialFirstBankAccountId: 0,
    bankAccountValues: {
      '0': {
        '0': {
          accountValueId: 0,
          date: '',
          value: 0.00
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
    console.log(bankAccounts)
    for (var i = 0; i < Object.keys(bankAccounts).length; i++) {
      console.log('i = ' + i)
      console.log(bankAccounts[i])
      var id = bankAccounts[i].bankAccountId
      if (i == 0) {
        state.initialFirstBankAccountId = id
      }
      // Object.assign(state.bankAccounts[bankAccounts[i].bankAccountId], bankAccounts[i])
      state.bankAccounts[id] = bankAccounts[i]
    }
    if (Object.keys(bankAccounts).length > 0) {
      Vue.delete(state.bankAccounts, 0)
    }
  },
  addBankAccount(state, bankAccount) {
    Object.assign(state.bankAccounts[bankAccount.bankAccountId], bankAccount)
  },
  deleteBankAccount(state, bankAccountId) {        
    Vue.delete(state.bankAccounts, bankAccountId)
  },
  updateBankAccountBalances(state) {
    // To do
  },
  updateBankAccountBalance(state, bankAccountId) {
    // To do
  },
  updateBankAccountValues(state, payload) {
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
  deleteBankAccountValues(state, payload) {
    console.log('deleteBankAccountValues mutation payload:')
    console.log(payload)
    for (var i = 0; i < Object.keys(payload.valueIds).length; i++) {
      var valueId = payload.valueIds[i].accountValueId;
      console.log('valueId:' + valueId);
      var accValuesArr = state.bankAccounts[payload.accountId].accountValues;
      console.log('accValuesArr length:' + accValuesArr.length);
      for (var j = 0; j < accValuesArr.length; j++) {
        if (accValuesArr[j].accountValueId == valueId) {
          Vue.delete(accValuesArr, j)
          break;
        } 
      }
    }
  },
  sortBankAccountValues(state, accountId) {
    state.bankAccounts[accountId].accountValues.sort(function(a, b) {
      var dateA = new Date(a.date);
      var dateB = new Date(b.date);
      return dateA - dateB;
    });
  }
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
    deleteBankAccount({ commit }, accountId) {
        console.log('accountId for deletion= ' + accountId)
        commit('deleteBankAccount', accountId)
    },
    updateBankAccountValues({ commit }, payload) {
        commit('updateBankAccountValues', payload)
    },
    sortBankAccountValues({ commit }, accountId) {
        commit('sortBankAccountValues', accountId)
    },
    deleteBankAccountValues({ commit }, payload) {
        console.log('deleteBankAccountValues actions payload:')
        console.log(payload)
        commit('deleteBankAccountValues', payload)
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
    getInitialFirstBankAccountId: (state) => {
        return Object.keys(state.bankAccounts)[0]
    },
    firstBankAccountId: (state) => {
        // console.log('in firstBankAccountId')
        // var firstaccID = 14
        // var accounts = state.bankAccounts
        // return firstaccID
        // return Object.keys(state.bankAccounts)[0]
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
        return state.bankAccounts[id].accountValues
    },
    getBankAccountBalance: (state) => (accountId) => {
        try {
            var numOfValues = state.bankAccounts[accountId].accountValues.length;
            console.log('numOfValues: ' + numOfValues )
            var accountValsArray = state.bankAccounts[accountId].accountValues;
            console.log('accountValsArray: ')
            console.log(accountValsArray)
            var accountValsSorted = accountValsArray.sort(function(a, b) {
                var dateA = new Date(a.date);
                var dateB = new Date(b.date);
                return dateA - dateB;
            });
            console.log('accountValsSorted: ')
            console.log(accountValsSorted)
            return accountValsSorted[ numOfValues - 1 ].value;
        }
        catch (error) {
            console.error(error); 
            return -1;
        }   

    }
}

export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters
}