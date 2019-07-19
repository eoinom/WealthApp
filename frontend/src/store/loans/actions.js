var moment = require('moment');


function resetState ({ commit }) {
  commit('resetState')
};

function initialiseLoans ({ commit, state }, loans) {
  var loanIdsWithVals = []   // for getting the total accounts balances below
  for (var i = 0; i < Object.keys(loans).length; i++) {
    var id = loans[i].loanId
    if (i === 0) {
      commit('setInitialFirstLoanId', id)
    }
    commit('addLoan', loans[i])
    commit('sortLoanValues', loans[i].loanId)

    // get the balance and update in the store
    var numOfValues = loans[i].loanValues.length;
    if (numOfValues > 0) {
      loanIdsWithVals.push(id)
      var balance = state.loans[id].loanValues[ numOfValues - 1 ].value;
      var balanceUser = state.loans[id].loanValues[ numOfValues - 1 ].valueUserCurrency;
      commit('updateLoanBalance', {loanId: id, balance: balance, balanceUserCurrency: balanceUser})
    }
  } 

  // Calculate the total loans balances 
  // first copy loanValues arrays from each loan (with values in it) into another container array
  let allLoanVals = []
  for (let i = 0; i < loanIdsWithVals.length; i++) {
    allLoanVals.push( state.loans[loanIdsWithVals[i]].loanValues.slice(0) )   // use slice to push a clone of the array 
  }  
  // get all dates
  let allDatesArr = []
  for (let i = 0; i < loanIdsWithVals.length; i++) {
    let loanDates = allLoanVals[i].map(a => a.date);
    for(let j = 0; j < loanDates.length; j++){
      allDatesArr[allDatesArr.length + j] = loanDates[j]
    }
  }
  //remove the empty elements:
  let allDates = allDatesArr.filter(function () { return true });
  // sort the dates
  allDates.sort(function(a, b) {
    let dateA = new Date(a);
    let dateB = new Date(b);
    return dateA - dateB;
  });  
  // create object to store total balances and initialise with a key for each date and a corresponding 0.0 value
  let allLoanBals = {}
  for (let i = 0; i < allDates.length; i++) {
    allLoanBals[ allDates[i] ] = 0.0
  }
  // then for each key value pair, for each loan, get loanVal with date closest (but lower) then the current key (date of pair)
  // add the valueUserCurrency to the value part of the pair (the total balance for that date)
  for (let i = 0; i < allDates.length; i++) {
    let date = allDates[i]    
    let balance = 0.0
    for (let j = 0; j < loanIdsWithVals.length; j++) {
      var nearestBal = 0.0
      for (let k = 0; k < allLoanVals[j].length; k++) {      
        if (allLoanVals[j][k].date <= date) {
            nearestBal = allLoanVals[j][k].valueUserCurrency
        }
        else {          
          break;
        }
      }        
      balance = balance + nearestBal
      // round to two decimals (https://stackoverflow.com/questions/11832914/round-to-at-most-2-decimal-places-only-if-necessary)
      balance = Math.round((balance + 0.00001) * 100) / 100
    }
    allLoanBals[date] = balance
  }
  console.log('allLoanBals (after) =');
  console.log(allLoanBals); 
  commit('updateTotalLoansBalances', allLoanBals)
};

async function addLoan ({ commit, rootState }, loan) {  
  loan.userId = rootState.main.user.userId
  console.log('loan to add:')
  console.log(loan)
  
  //send mutation to graphql with loan to add to db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($loan: LoanInputType!){
          loan_mutations {
            addLoan(loan: $loan) {
              loanId
              loanName
              description
              type
              institution
              startPrincipal
              startDate
              totalTerm
              fixedTerm
              rateType
              aprRate
              repaymentFrequency
              repaymentAmount
              isActive
              quotedCurrency {
                code
                nameLong
                nameShort
              }              
            }
          }
        }
        `,
        variables: {
          loan: loan
        },
      }
    });            
    
    // get back details of new loan from database and add to local store
    if (response.data.data.loan_mutations.addLoan != null) {          
      commit('addLoan', response.data.data.loan_mutations.addLoan)
    }   
  } catch (error) {
    console.error(error); 
  }
};

async function updateLoan ({ commit, rootState }, loan) {
  loan.userId = rootState.main.user.userId
  console.log('loan to update:')
  console.log(loan)

  //send mutation to graphql with loan to update in db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($loan: LoanInputType!){
          loan_mutations {
            updateLoan(loan: $loan) {
              loanId
              loanName
              description
              type
              institution
              isActive
              quotedCurrency {
                code
                nameLong
                nameShort
              }
            }
          }
        }
        `,
        variables: {
          loan: loan
        },
      }
    });            
    
    // get back details of amended loan from database and update in local store
    if (response.data.data.loan_mutations.updateLoan != null) {          
      commit('updateLoan', response.data.data.loan_mutations.updateLoan)
    }   
  } catch (error) {
    console.error(error); 
  }
}

 async function deleteLoan ({ commit, state, /*dispatch*/ }, loanId) {
  console.log('loanId for deletion: ' + loanId)
  
  //send mutation to graphql with loanId to delete from db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($loanId: ID!){
          loan_mutations {
            deleteLoan(loanId: $loanId)
          }
        }
        `,
        variables: {
          loanId: loanId
        },
      }
    });            
    
    // if delete from db was successful then delete also from local store
    if (response.data.data.loan_mutations.deleteLoan != null) {          
      commit('deleteLoan', loanId)
    }   
  } catch (error) {
    console.error(error); 
  }  
  
  if (state.selectedLoanId == loanId) {
    // update the selectedLoanId
    var newSelectedId = 0
    if (state.loanIds.length > 0) {
      newSelectedId = state.loanIds[0]
    }
    commit('updateSelectedLoanId', newSelectedId)
  }
}

function updateLoanBalance ({ commit }, payload) {
  commit('updateLoanBalance', payload)
}

function updateLoanBalances ({ commit, state }, payload) {
  for (var i = 0; i < Object.keys(state.loans).length; i++) {
    var id = Object.keys(state.loans)[i].loanId

    commit('sortLoanValues', id)

    // get the balance and update in the store
    var numOfValues = state.loans[id].loanValues.length;
    if (numOfValues > 0) {
      var balance = state.loans[id].loanValues[ numOfValues - 1 ].value;
      var balanceUser = state.loans[id].loanValues[ numOfValues - 1 ].valueUserCurrency;
      commit('updateLoanBalance', {loanId: id, balance: balance, balanceUserCurrency: balanceUser})
    }
  } 
}

async function addLoanValue ({ commit }, loanValue) {
  console.log('loan value to add:')
  console.log(loanValue)
  
  //send mutation to graphql with loanValue to add to db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($loanValue: LoanValueInputType!){
          loanValue_mutations {
            addLoanValue(loanValue: $loanValue) {                    
              loanValueId  
              date
              value
              rateToUserCurrency
              valueUserCurrency
              loan {
                loanId
              }
            }
          }
        }
        `,
        variables: {
          loanValue: loanValue
        },
      }
    });            
    
    // get back details of new loan from database and add to local store
    if (response.data.data.loanValue_mutations.addLoanValue != null) {       
      commit('addLoanValue', response.data.data.loanValue_mutations.addLoanValue)
      commit('sortLoanValues', loanValue.loanId)
    }   
  } catch (error) {
    console.error(error); 
  }
}

async function updateLoanValue ({ commit }, loanValue) {
  console.log('loan value to update:')
  console.log(loanValue)
  
  //send mutation to graphql with loanValue to add to db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($loanValue: LoanValueInputType!){
          loanValue_mutations {
            updateLoanValue(loanValue: $loanValue) {                    
              loanValueId  
              date
              value
              rateToUserCurrency
              valueUserCurrency
              loan {
                loanId
              }
            }
          }
        }
        `,
        variables: {
          loanValue: {
            loanValueId: loanValue.loanValueId,
            date: loanValue.date,
            value: loanValue.value,
            loanId: loanValue.loan.loanId
          }
        },
      }
    });            
    
    // get back details of new loan from database and add to local store
    if (response.data.data.loanValue_mutations.updateLoanValue != null) {    
      commit('updateLoanValue', response.data.data.loanValue_mutations.updateLoanValue)
      commit('sortLoanValues', loanValue.loan.loanId)

      // get the balance and update in the store
      var id = loanValue.loan.loanId
      var numOfValues = state.loans[id].loanValues.length;
      var balance = state.loans[id].loanValues[ numOfValues - 1 ].value;
      commit('updateLoanBalance', {loanId: id, balance: balance})
    }   
  } catch (error) {
    console.error(error); 
  }
}

// function updateLoanValues ({ commit }, payload) {
//   // Not implemented
//   // commit('updateLoanValues', payload)
// }

function sortLoanValues ({ commit }, loanId) {
  commit('sortLoanValues', loanId)
};

async function deleteLoanValues ({ commit }, payload) {
  console.log('deleteLoanValues actions payload:')
  console.log(payload)
  
  // send mutation to graphql with array of LoanValueIds to delete from db
  const axios = require("axios");
  try {
    var response = await axios({
      method: "POST",
      url: "/",
      data: {
        query: `                    
        mutation ($loanValueIds: [Int]!) {
          loanValue_mutations {
            deleteLoanValuesByIds(loanValueIds: $loanValueIds)
          }
        }
        `,
        variables: {
          loanValueIds: payload.loanValueIds
        },
      }
    });            
    
    // if delete from db was successful then delete also from local store
    if (response.data.data.loanValue_mutations.deleteLoanValuesByIds != null) {       
      commit('deleteLoanValues', payload)
    }   
  } catch (error) {
    console.error(error); 
  }
};

function updateSelectedLoanId({ commit }, loanId) {
  commit('updateSelectedLoanId', loanId)
}

function updateSelectedLoanCurrencySymbol({ commit }, symbol) {
  commit('updateSelectedLoanCurrencySymbol', symbol)
}

function updateTableColumn({ commit }, payload) {
  commit('updateTableColumn', payload)
}

function addToVisibleColumns({ commit }, columnName) {
  commit('addToVisibleColumns', columnName)
}

function removeFromVisibleColumns({ commit }, columnName) {
  commit('removeFromVisibleColumns', columnName)
}

export {
  resetState,
  initialiseLoans,
  addLoan,
  updateLoan,
  deleteLoan,
  updateLoanBalance,
  updateLoanBalances,
  addLoanValue,
  updateLoanValue,
  // updateLoanValues,
  sortLoanValues,
  deleteLoanValues,
  updateSelectedLoanId,
  updateSelectedLoanCurrencySymbol,
  updateTableColumn,  
  addToVisibleColumns,
  removeFromVisibleColumns
}