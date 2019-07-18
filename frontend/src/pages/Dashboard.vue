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

    <!-- <div class="row justify-center q-ma-md">
      <div class="col-12 q-ml-sm">
        <apexchart width="100%" height="500" type="area" :options="chartOptions" :series="series"></apexchart>
      </div>
    </div> -->
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

      updateChart() {
        const max = 90;
        const min = 20;
        const newData = this.series[0].data.map(() => {
          return Math.floor(Math.random() * (max - min + 1)) + min
        })

        const colors = ['#008FFB', '#00E396', '#FEB019', '#FF4560', '#775DD0']

        // Make sure to update the whole options config and not just a single property to allow the Vue watch catch the change.
        this.chartOptions = {
          colors: [colors[Math.floor(Math.random()*colors.length)]]
        };
        // In the same way, update the series option
        this.series = [{
          data: newData
        }]
      }
    },

    computed: {
      ...mapGetters('accounts', ['accounts', 'accountBalanceUserCurrency', 'accountById', 'accountValuesByAccountId', 'accountName']),
      ...mapGetters('loans', ['loans', 'loanBalanceUserCurrency', 'loanById', 'loanValuesByLoanId', 'loanName']),
      ...mapGetters('main', ['userDisplayCurrencyCode']),

      userCurrencySymbol() {
        return this.getCurrencySymbol(this.userDisplayCurrencyCode)
      },

      accountsTotal() {
        var total = 0.00
        var accounts = this.accounts        
        for (var i = 0; i < Object.keys(accounts).length; i++) {
          var id = Object.keys(accounts)[i]
          console.log('in accountsTotal');          
          console.log('id: ');
          console.log(id);
          
          var balance = this.accountBalanceUserCurrency(id)
          console.log('balance = ' + this.accountBalanceUserCurrency(id));
          if (balance > 0) {
            total = total + balance
            console.log('this.accountBalanceUserCurrency(id): ');
            console.log(this.accountBalanceUserCurrency(id));
          }
        }
        console.log('accountsTotal =' + total);
        return total
      },

      assetsBreakdown() {
        var breakdown = []
        var obj = {}
        obj.name = "Accounts"
        obj.symbol = this.userCurrencySymbol
        obj.value = this.accountsTotal
        // Vue.set(breakdown, 0, obj)
        breakdown[0] = obj
        return breakdown
      },

      assetsTotal() {
        console.log('assetsTotal =' + this.accountsTotal);
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
        console.log('loansTotal =' + total);
        return total
      },

      liabilitiesBreakdown() {
        var breakdown = []
        var obj = {}
        obj.name = "Loans"
        obj.symbol = this.userCurrencySymbol
        obj.value = this.loansTotal
        // Vue.set(breakdown, 0, obj)
        breakdown[0] = obj
        return breakdown
      },

      liabilitiesTotal() {
        console.log('liabilitiesTotal =' + this.loansTotal);
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
            id: 'vuechart-example',
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
            name: 'Account Values',
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
      series() {
        var storeAccountVals = this.accountValuesByAccountId(this.selectedAccountId());   // get array from store
        var accountVals = storeAccountVals.map((b, idx) => Object.assign({ index: idx }, b));   // clone the array, ref:https://stackoverflow.com/questions/44837957/how-to-clone-a-vuex-array

        accountVals.forEach(obj => {
          if (obj.date !== undefined) {
            Object.defineProperty(obj, "x",
              Object.getOwnPropertyDescriptor(obj, "date"));
            delete obj["date"];
          }
          if (obj.value !== undefined) {
            Object.defineProperty(obj, "y",
              Object.getOwnPropertyDescriptor(obj, "value"));
            delete obj["value"];
          }
          if (obj.accountValueId !== undefined) {
            delete obj["accountValueId"];
          }
        });

        return [{
            name: this.selectedAccountName,
            data: accountVals
        }]
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