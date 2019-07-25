<template>
  <q-page padding>

<!-- CARDS -->
    <div class="row q-col-gutter-md">
      <div id="cardDiv-networth" :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col-4'})">
        <card-total 
          :title = "'Total Net Worth'"
          :currencySymbol = "userCurrencySymbol"
          :total = netWorthTotal
          :decimalPlaces = cardDecimalPlaces
          cardStyle = "background: radial-gradient(circle, #01BB56 0%, #01883F 100%)"
          iconName = 'fas fa-balance-scale-left'
        />
      </div>
      <div id="cardDiv-assets" :class="$mq | mq({ mobile_sm: 'col-12', mobile_lg: 'col-6', tablet_lg: 'col-4'})">
        <card-total 
          title = "Total Assets"
          :currencySymbol = userCurrencySymbol
          :total = assetsTotal
          :decimalPlaces = cardDecimalPlaces
          cardStyle = 'background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)'
          iconName = 'account_balance_wallet'
          :breakdown = assetsBreakdown
        />
      </div>
      <div id="cardDiv-liabilities" :class="$mq | mq({ mobile_sm: 'col-12', mobile_lg: 'col-6', tablet_lg: 'col-4'})">
        <card-total 
          :title = "'Total Liabilities'"
          :currencySymbol = userCurrencySymbol
          :total = liabilitiesTotal
          :decimalPlaces = cardDecimalPlaces
          cardStyle = "background: radial-gradient(circle, #BB5601 0%, #883F01 100%)"
          iconName = 'fas fa-hand-holding-usd'
          :breakdown = liabilitiesBreakdown
        />
      </div>
    </div>


<!-- LINE/AREA CHARTS -->
    <div class="row justify-center q-my-md q-col-gutter-md">
      <!-- <div class="col-md-12 col-lg-6 q-ma-md"> -->
      <div class="q-ma-md" :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col'})">
        <apexchart 
          width="100%" 
          height="300" 
          type="area" 
          :options="assetsLiabilitiesAreaChartOptions" 
          :series="assetsLiabilitiesSeries" 
        />
      </div>      

      <!-- <div class="col-md-12 col-lg-6 q-ma-md"> -->
      <div class="q-ma-md" :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col'})">
        <apexchart 
          width="100%" 
          height="300" 
          type="area" 
          :options="netWorthAreaChartOptions" 
          :series="netWorthSeries" 
        />
      </div>
    </div>

<!-- PIE/DONUT CHARTS -->
    <div class="row justify-center q-col-gutter-md">
      <!-- <div class="col-md-12 col-lg-6"> -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col'})">
        <apexchart 
          width="100%" 
          height="300" 
          type="donut" 
          :options="accountsPieChartOptions" 
          :series="accountsPieChartOptions.series" 
        />
      </div>
      <!-- <div class="col-md-12 col-lg-6"> -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col'})">
        <apexchart 
          width="100%" 
          height="300" 
          type="donut" 
          :options="loansPieChartOptions" 
          :series="loansPieChartOptions.series" 
        />
      </div>
    </div>

    <div class="row justify-center">
      <br /><br /><br />
    </div>

<!-- BAR CHARTS -->
    <div class="row justify-center">
      <!-- <div class="col-sm-12 col-md-6"> -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col'})">
        <apexchart 
          width="100%" 
          height="600" 
          type="bar" 
          :options="accountsBarChartOptions" 
          :series="accountsBarChartOptions.series" 
        />
      </div>
      <!-- <div class="col-sm-12 col-md-6"> -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col'})">
        <apexchart 
          width="100%" 
          height="600" 
          type="bar"
          :options="loansBarChartOptions" 
          :series="loansBarChartOptions.series" 
        />
      </div>
    </div>
  </q-page>
</template>


<script>  
  import { mapGetters } from 'vuex'
  import { mapActions } from 'vuex'
  import Vue from 'vue'
  import VueMq from 'vue-mq'

  Vue.use(VueMq, {
    breakpoints: {
      mobile_sm: 450,
      mobile_md: 767,
      mobile_lg: 1023,
      mobile: 1023,
      tablet_md: 1250,
      tablet_lg: 1439,
      tablet: 1439,
      desktop: Infinity,
    },
    defaultBreakpoint: 'mobile'
  })

  export default {
    // el: 'Dashboard',

    data: function() {
      return {  
        window: {
          width: 0,
          height: 0
        }
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
      },

      handleResize: function() {
        this.window.width = window.innerWidth;
        this.window.height = window.innerHeight;
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

      cardDecimalPlaces() {
        // var width = this.window.width
        // if (this.window.width > 1440)
        //   return 2
        // else 
        //   return 0
        return 2
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
      areaChartOptions() {
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
      netWorthAreaChartOptions() {          
        var options = Object.assign({}, this.areaChartOptions)
        options.title = {
          text: 'Net Worth',
          align: 'center',
          style: {
            fontSize:  '20px',
            color:  '#027BE3'
          },
        }
        return options
      },
      assetsLiabilitiesAreaChartOptions() {          
        var options = Object.assign({}, this.areaChartOptions)
        options.title = {
          text: 'Assets and Liabilities',
          align: 'center',
          style: {
            fontSize:  '20px',
            color:  '#027BE3'
          },
        }
        return options
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
      },
      pieChartOptions() {  
        var mainState = this.$store.state.main
        var options = {
          dataLabels: {
            enabled: true,
            formatter: function (val) {
              return val.toFixed(1) + "%"
            },
            style: {
              fontSize: '16px',
              // fontFamily: 'Helvetica, Arial, sans-serif',
              // colors: undefined
            }
          },
          plotOptions: {
            pie: {
              customScale: 1.0,
              offsetX: 80,
              donut: {                
                labels: {
                  show: true,
                  value: {
                    formatter: function (val) {
                      return mainState.toUserCurrency(val)
                    },
                  }
                }
              }
            }
          }
        }
        return options
      },
      accountsPieChartOptions() {          
        var labels = []
        var series = []
        this.accountIdsWithVals.forEach(id => {
          if (this.accounts[id].balanceUserCurrency > 0) {
            labels.push(this.accounts[id].accountName)
            series.push(this.accounts[id].balanceUserCurrency)
          }
        });
        var options = Object.assign({}, this.pieChartOptions)
        options.series = series
        options.labels = labels
        options.title = {
          text: 'Account Balances',
          align: 'center',
          margin: 20,
          offsetX: 0,
          style: {
            fontSize:  '20px',
            color:  '#027BE3'
          },
        }
        return options
      },
      loansPieChartOptions() {        
        var labels = []
        var series = []
        this.loanIdsWithVals.forEach(id => {
          if (this.loans[id].balanceUserCurrency > 0) {
            labels.push(this.loans[id].loanName)
            series.push(this.loans[id].balanceUserCurrency)
          }
        });
        var options = Object.assign({}, this.pieChartOptions)
        options.series = series
        options.labels = labels
        options.title = {
          text: 'Loan Balances',
          align: 'center',
          margin: 20,
          offsetX: 0,
          style: {
            fontSize:  '20px',
            color:  '#a24a01'
          },
        }
        return options
      },
      barChartOptions() {  
        var mainState = this.$store.state.main
        var options = {
          dataLabels: {
            enabled: true,
            formatter: function (val) {
              // return val.toFixed(1) + "%"
              return mainState.toUserCurrency(val)
            },
            style: {
              fontSize: '16px',
              // fontFamily: 'Helvetica, Arial, sans-serif',
              // colors: undefined
            }
          },
          plotOptions: {
            bar: {
              horizontal: false,
              endingShape: 'flat',
              columnWidth: '70%',
              barHeight: '70%',
              distributed: false,
              colors: {
                  ranges: [{
                      from: 0,
                      to: 0,
                      color: undefined
                  }],
                  backgroundBarColors: [],
                  backgroundBarOpacity: 1,
              },
              dataLabels: {
                  position: 'top',
                  maxItems: 100,
                  hideOverflowingLabels: true,
              }
            }
          }
        }
        return options
      },
      accountsBarChartOptions() {   
        var dataObj = {}
        this.accountIdsWithVals.forEach(id => {
          if (this.accounts[id].balanceUserCurrency > 0) {
            dataObj[this.accounts[id].accountName] = this.accounts[id].balanceUserCurrency
          }
        });
        var dataArr = this.$store.state.main.getChart_XY_DataFromObj(dataObj)
        var series = [
          { name: "Accounts", data: dataArr }
        ]
        var options = Object.assign({}, this.barChartOptions)
        options.series = series
        options.title = {
          text: 'Account Balances',
          align: 'center',
          margin: 20,
          offsetX: 0,
          style: {
            fontSize:  '20px',
            color:  '#027BE3'
          },
        }
        return options
      },
      loansBarChartOptions() {        
        var dataObj = {}
        this.loanIdsWithVals.forEach(id => {
          if (this.loans[id].balanceUserCurrency > 0) {
            dataObj[this.loans[id].loanName] = this.loans[id].balanceUserCurrency
          }
        });
        var dataArr = this.$store.state.main.getChart_XY_DataFromObj(dataObj)
        var series = [
          { name: "Loans", data: dataArr }
        ]
        var options = Object.assign({}, this.barChartOptions)
        options.colors = ['#a24a01']
        options.series = series
        options.title = {
          text: 'Loan Balances',
          align: 'center',
          margin: 20,
          offsetX: 0,
          style: {
            fontSize:  '20px',
            color:  '#a24a01'
          },
        }
        console.log('loans Bar Chart options');
        console.log(options);
        
        return options
      }
      // END OF CHART PROPERTIES 
    },

    created() {
      window.addEventListener('resize', this.handleResize)
      this.handleResize();
    },

    mounted () {    
      this.calculateLineGraphSeries()
    },

    destroyed() {
      window.removeEventListener('resize', this.handleResize)
    },

    components : {
      'card-total' : require('components/Dashboard/cardTotal.vue').default
    },
  }
</script>


<style lang="scss">

/* @media only screen and (min-width: 1440px) {
  .tabletCards {
    display: none;
  }
}
  
@media only screen and (max-width: 1440px) {
  .desktopCards {
    display: none;
  }
} */

/* @media only screen and (min-width: 1024px) { */

// @media (min-width $breakpoint-md-min) {
//   div#cardDiv-networth {
//     order: 1;
//   }
//   div#cardDiv-assets {
//     order: 2;
//   }
//   div#cardDiv-liabilities {
//     order: 3;
//   }
// }

// @media only screen and (min-width: 768px) {
//   div#cardDiv-assets {
//     order: 1;
//   }
//   div#cardDiv-liabilities {
//     order: 2;
//   }
//   div#cardDiv-networth {
//     order: 3;
//   }
// }

#cardDiv-networth.col-12 { order: 1 }
#cardDiv-networth.col-4 { order: 3 }

#cardDiv-assets.col-12 { order: 2 }
#cardDiv-assets.col-6 { order: 2 }
#cardDiv-assets.col-4 { order: 1 }

#cardDiv-liabilities.col-12 { order: 3 }
#cardDiv-liabilities.col-6 { order: 3 }
#cardDiv-liabilities.col-4 { order: 2 }


</style>