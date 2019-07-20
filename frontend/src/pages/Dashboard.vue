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
      ...mapGetters('accounts', ['selectedAccountId']),
      ...mapGetters('loans', []),
    },

    computed: {
      ...mapGetters('accounts', ['accounts', 'accountBalanceUserCurrency', 'totalAccountsBalances', 'accountById', 'accountValuesByAccountId', 'accountName']),
      ...mapGetters('loans', ['loans', 'loanBalanceUserCurrency', 'totalLoansBalances', 'loanById', 'loanValuesByLoanId', 'loanName']),
      ...mapGetters('main', ['userDisplayCurrencyCode']),

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
      assetsLiabilitiesSeries() {

        var accountsData = this.$store.state.main.getChart_XY_DataFromObj(this.totalAccountsBalances)
        var loansData = this.$store.state.main.getChart_XY_DataFromObj(this.totalLoansBalances)

        var series = [
          {
            name: "Accounts",
            data: accountsData
          },
          {
            name: "Loans",
            data: loansData
          }
        ]
        return series
      }
      // END OF CHART PROPERTIES 
    },

    mounted () {    
    },

    components : {
      'card-total' : require('components/Dashboard/cardTotal.vue').default
    },
  }
</script>


<style>
  
</style>