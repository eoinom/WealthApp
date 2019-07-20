<template>
  <q-page padding>

    <div class="row">
      <div class="col" />
      <div class="col-3">
        <card-total 
          title = "Total Assets"
          :currencySymbol = userCurrencySymbol
          :total = assetsTotal
          decimalPlaces = 2
          cardStyle = 'background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)'
          iconName = 'account_balance_wallet'
          :breakdown = assetsBreakdown
        />
      </div>
      <div class="col" />
      <div class="col" />
      <div class="col-3">
        <card-total 
          :title = "'Total Liabilities'"
          :currencySymbol = userCurrencySymbol
          :total = liabilitiesTotal
          decimalPlaces = 2
          cardStyle = "background: radial-gradient(circle, #BB5601 0%, #883F01 100%)"
          iconName = 'fas fa-hand-holding-usd'
          :breakdown = liabilitiesBreakdown
        />
      </div>
      <div class="col" />
      <div class="col" />
      <div class="col-3">
        <card-total 
          :title = "'Total Net Worth'"
          :currencySymbol = "userCurrencySymbol"
          :total = netWorthTotal
          decimalPlaces = 2
          cardStyle = "background: radial-gradient(circle, #01BB56 0%, #01883F 100%)"
          iconName = 'fas fa-balance-scale-left'
        />
      </div>
      <div class="col" />
    </div>

    <div class="row justify-center q-ma-md">
      <div class="col-12 q-ml-sm">
        <apexchart width="100%" height="500" type="area" :options="chartOptions" :series="netWorthSeries"></apexchart>
      </div>
    </div>

    <div class="row justify-center q-ma-md">
      <div class="col-12 q-ml-sm">
        <apexchart width="100%" height="500" type="area" :options="chartOptions" :series="assetsLiabilitiesSeries"></apexchart>
      </div>
    </div>
  </q-page>
</template>


<script>
  import { mapGetters } from 'vuex'
  import { mapActions } from 'vuex'

  export default {
    // el: 'Dashboard',

    data: function() {
      return {             
      }
    },

    methods: {
      ...mapActions('accounts', ['updateTotalAccountsBalances']),
      ...mapActions('loans', ['updateTotalLoansBalances']),
      ...mapActions('main', ['updateTotalNetWorthBalances']),

      calculateLineGraphSeries() {  
        // Calculate the total accounts, loans and net worth balances (for each date where a value exists in any account or loan account)

        // copy accountValues arrays from each account (with values in it) into another container array
        let allAccountVals = []
        for (let i = 0; i < this.accountIdsWithVals.length; i++) {
          allAccountVals.push( this.accounts[this.accountIdsWithVals[i]].accountValues.slice(0) )   // use slice to push a clone of the array 
        }  
        // copy loanValues arrays from each loan (with values in it) into another container array
        let allLoanVals = []
        for (let i = 0; i < this.loanIdsWithVals.length; i++) {
          allLoanVals.push( this.loans[this.loanIdsWithVals[i]].loanValues.slice(0) )   // use slice to push a clone of the array 
        }   

        // get all dates
        let allDatesArr = []
        for (let i = 0; i < this.accountIdsWithVals.length; i++) {
          let accountDates = allAccountVals[i].map(a => a.date);
          for(let j = 0; j < accountDates.length; j++){
            allDatesArr[allDatesArr.length + j] = accountDates[j]
          }
        }
        for (let i = 0; i < this.loanIdsWithVals.length; i++) {
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

        // create objects to store total balances and initialise with a key for each date and a corresponding 0.0 value
        let allAccountBals = {}
        let allLoanBals = {}
        let allNetWorthBals = {}
        for (let i = 0; i < allDates.length; i++) {
          allAccountBals[ allDates[i] ] = 0.0
          allLoanBals[ allDates[i] ] = 0.0
          allNetWorthBals[ allDates[i] ] = 0.0
        }

        // for each key value pair, for each account, get accountVal with date closest (but lower) then the current key (date of pair)
        // add the valueUserCurrency to the value part of the pair (the total balance for that date)
        for (let i = 0; i < allDates.length; i++) {
          let date = allDates[i]    
          let balance = 0.0
          for (let j = 0; j < allAccountVals.length; j++) {
            var nearestBal = 0.0
            for (let k = 0; k < allAccountVals[j].length; k++) {      
              if (allAccountVals[j][k].date <= date) {
                  nearestBal = allAccountVals[j][k].valueUserCurrency
              }
              else {          
                break;
              }
            }        
            balance = balance + nearestBal
            // round to two decimals (https://stackoverflow.com/questions/11832914/round-to-at-most-2-decimal-places-only-if-necessary)
            balance = Math.round((balance + 0.00001) * 100) / 100
          }
          allAccountBals[date] = balance
        }
        this.updateTotalAccountsBalances(allAccountBals)

        // now do for loans
        for (let i = 0; i < allDates.length; i++) {
          let date = allDates[i]    
          let balance = 0.0
          for (let j = 0; j < allLoanVals.length; j++) {
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
            balance = Math.round((balance + 0.00001) * 100) / 100
          }
          allLoanBals[date] = balance
        }
        this.updateTotalLoansBalances(allLoanBals)

        // now get net worth balances
        for (let i = 0; i < allDates.length; i++) {
          let date = allDates[i]    
          let balance = allAccountBals[date] - allLoanBals[date]
          balance = Math.round((balance + 0.00001) * 100) / 100
          allNetWorthBals[date] = balance
        }
        this.updateTotalNetWorthBalances(allNetWorthBals)
      }
    },

    computed: {
      ...mapGetters('accounts', ['accounts', 'accountBalanceUserCurrency', 'accountIdsWithVals', 'totalAccountsBalances', 'accountById', 'accountValuesByAccountId', 'accountName']),
      ...mapGetters('loans', ['loans', 'loanBalanceUserCurrency', 'loanIdsWithVals', 'totalLoansBalances', 'loanById', 'loanValuesByLoanId', 'loanName']),
      ...mapGetters('main', ['totalNetWorthBalances', 'userDisplayCurrencyCode']),

      userCurrencySymbol() {
        return this.getCurrencySymbol(this.userDisplayCurrencyCode)
      },

      accountsTotal() {
        var total = 0.00
        var accounts = this.accounts        
        for (var i = 0; i < Object.keys(accounts).length; i++) {
          var id = Object.keys(accounts)[i]
          
          var balance = this.accountBalanceUserCurrency(id)
          if (balance > 0) {
            total = total + balance
          }
        }
        return total
      },

      assetsBreakdown() {
        var breakdown = []
        var obj = {}
        obj.name = "Accounts"
        obj.symbol = this.userCurrencySymbol
        obj.value = this.accountsTotal
        breakdown[0] = obj
        return breakdown
      },

      assetsTotal() {
        return this.accountsTotal
      },

      loansTotal() {
        var total = 0.00
        var loans = this.loans
        for (var i = 0; i < Object.keys(loans).length; i++) {
          var id = Object.keys(loans)[i]
          var balance = this.loanBalanceUserCurrency(id)
          if (balance > 0 ) {
            total = total + this.loanBalanceUserCurrency(id)
          }
        }
        return total
      },

      liabilitiesBreakdown() {
        var breakdown = []
        var obj = {}
        obj.name = "Loans"
        obj.symbol = this.userCurrencySymbol
        obj.value = this.loansTotal
        breakdown[0] = obj
        return breakdown
      },

      liabilitiesTotal() {
        return this.loansTotal
      },

      netWorthTotal() {
        return this.assetsTotal - this.liabilitiesTotal
      },

// CHART PROPERTIES
      chartOptions() {
        // Make sure to update the whole options config and not just a single property to allow the Vue watch catch the change.
        return {
          chart: {
            id: 'assets and liabilities line chart',
            zoom: {
              enabled: true
            }
          },
          dataLabels: {
            enabled: false
          },
          stroke: {
            curve: 'straight'   // straight or smooth
          },
          series: [{
            name: 'Assets',
            data: [1000.00, 2000.50, 1500.54, 1856.42, 2254.24, 2354.11]
          }],
           title: {
            text: this.selectedAccountName,
            align: 'center',
            style: {
              fontSize:  '20px',
              color:  '#027BE3'
            },
          },
          labels: ['2001-01-28', '2001-03-28', '2001-05-28', '2001-07-28', '2001-09-28', '2001-11-28'],
          xaxis: {
            type: 'datetime',
          }
        }
      },
      netWorthSeries() {
        var netWorthData = this.$store.state.main.getChart_XY_DataFromObj(this.totalNetWorthBalances)
        var series = [
          { name: "Net Worth", data: netWorthData }
        ]
        return series
      },
      assetsLiabilitiesSeries() {
        var accountsData = this.$store.state.main.getChart_XY_DataFromObj(this.totalAccountsBalances)
        var loansData = this.$store.state.main.getChart_XY_DataFromObj(this.totalLoansBalances)
        var series = [
          { name: "Accounts", data: accountsData },
          { name: "Loans", data: loansData }
        ]
        return series
      }
      // END OF CHART PROPERTIES 
    },

    mounted () {    
      this.calculateLineGraphSeries()
    },

    components : {
      'card-total' : require('components/Dashboard/cardTotal.vue').default
    },
  }
</script>


<style>
  
</style>