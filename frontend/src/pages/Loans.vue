<template>
  <q-page padding>

    <div class="row justify-around q-col-gutter-lg">

<!-- LOANS LIST -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', mobile_md: 'col-12', tablet_md: 'col-12', tablet_lg: 'col-5'})">
        <div :class="$mq | mq({ mobile_sm: 'loans-list-div-mbl', mobile_lg: 'loans-list-div', tablet_lg: 'loans-list-div'})">
          <h5 class="q-my-md">Loans</h5>
          <div class="q-mb-sm">
            <q-btn
              @click="showAddLoan = true"
              color="negative"
              icon="add"
              label="Add Loan"
              class="q-mb-sm"
              rounded
            />
          </div>

          <q-scroll-area 
            :thumb-style="thumbStyle"
            :content-style="contentStyle"
            :content-active-style="contentActiveStyle"
            class="loans-scroll-area">

            <template v-if="Object.keys(loans).length > 0">
              
              <loan
                v-for="(loan, key) in loans"
                :key="key"
                :loan="loan"
                :id="key"
                class="q-mb-md q-mr-sm">
              </loan>

            </template>
          </q-scroll-area>
        </div>
      </div>

<!-- LOAN VALUES TABLE -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col-5'})">
        <div :class="$mq | mq({ mobile_sm: 'loans-table-div-mbl', mobile_lg: 'loans-table-div', tablet_lg: 'loans-table-div'})">
          <h5 :class="$mq | mq({ mobile_sm: 'q-my-md', tablet_md: 'q-my-md', tablet_lg: 'q-mt-xl q-mb-lg'})">Loan Balances</h5>        
          <div>
            <q-table
              title="LoanValues"
              :data="selectedLoanValues"
              :columns="tableColumns()"
              :visible-columns="visibleColumns"
              row-key="date"            
              :filter="filter"
              :loading="loading"
              :pagination.sync="pagination"
              :selected-rows-label="getSelectedString"
              selection="multiple"
              :selected.sync="selectedValues"
              :sort-method="customTableSort"
              binary-state-sort
              :dense="isMobile"
              >
              <template v-slot:top>
                <div class="col-2">
                  <q-btn 
                    round 
                    icon="remove" 
                    dense 
                    color="red" 
                    class="q-mr-md q-my-md"
                    :disable="loading" 
                    @click="promptToDeleteLoanValue()" 
                  />
                </div>

                <div class="col">
                  <div class="text-h6 text-liabilities text-center">{{ selectedLoanName }}</div>
                </div>

                <div class="col-2">
                  <q-btn 
                    round 
                    icon="add" 
                    dense 
                    class="q-ml-md q-my-md"
                    color="primary" 
                    :disable="loading" 
                    @click="showAddLoanValue = true" 
                  />
                </div>
              </template>            

              <template v-slot:body="props">
                <q-tr :props="props">
                  <q-td auto-width>
                    <q-checkbox v-model="props.selected" />
                  </q-td>

                  <!-- Date -->
                  <q-td key="date" :props="props">
                    {{ formatDate_Iso2User(props.row.date) }}
                    <q-popup-edit 
                      v-model="popupEditDate" 
                      @show="() => showPopupDate(props.row, 'date')" 
                      >
                      <q-date
                        v-model="popupEditDate" 
                        today-btn
                        mask="YYYY-MM-DD"
                        :options="dateOptionsFn"
                        @input="(val, initval) => onUpdateLoanValue(val, props.row, 'date')"
                      />
                    </q-popup-edit>
                  </q-td>

                  <!-- Value -->
                  <q-td key="value" :props="props">
                    {{ toLocaleFixed(props.row.value, 2) }}

                    <q-popup-edit 
                      v-model="popupEditValue" 
                      @show="() => showPopupValue(props.row, 'value')" 
                      @save="(val, initval) => onUpdateLoanValue(val, props.row, 'value')"
                      title="Update value" 
                      buttons >
                      <q-input 
                        type="number" 
                        v-model="popupEditValue" 
                        dense 
                        autofocus />
                    </q-popup-edit>
                  </q-td>   

                  <!-- RateToUserCurrency -->
                  <q-td key="rateToUserCurrency" :props="props">
                    {{ props.row.rateToUserCurrency.toFixed(4) }}
                  </q-td>    

                  <!-- ValueUserCurrency -->
                  <q-td key="valueUserCurrency" :props="props">
                    {{ toLocaleFixed(props.row.valueUserCurrency, 2) }}
                  </q-td>                 
                </q-tr>              
              </template>
            </q-table>
          </div> 
        </div> 
      </div>
    </div>

<!-- LINE/AREA CHART -->
    <div class="row justify-center q-my-xl" :class="$mq | mq({ mobile_sm: '', tablet_md: 'q-mx-md', tablet_lg: 'q-mx-md'})">
      <div class="col-12 q-mr-md">
        <apexchart width="100%" height="500" type="area" :options="chartOptions" :series="series"></apexchart>
      </div>
    </div>


    <br />
    <div class="footerNotes text-center">
      <a href="https://clearbit.com" class="">Logos provided by Clearbit</a>
    </div>

    <q-dialog v-model="showAddLoan">
      <add-loan @close="showAddLoan = false" />
    </q-dialog>

    <q-dialog v-model="showAddLoanValue">
      <add-loan-value 
        :loanId="selectedLoanId()"
        @close="showAddLoanValue = false" />
    </q-dialog>
  </q-page>
</template>


<script>
  import { mapGetters } from 'vuex'
  import { mapActions } from 'vuex'
  import { scrollAreaMixin } from '../mixins/scrollAreaMixin'
  import { tableMixin } from '../mixins/tableMixin'
  import Vue from 'vue'
  import VueMq from 'vue-mq'

  Vue.use(VueMq, {
    breakpoints: {
      mobile_sm: 450,     // 0 - 450
      mobile_md: 767,     // 451 - 767
      mobile_lg: 1023,    // 768 - 1023
      tablet_md: 1250,    // 1024 - 1250
      tablet_lg: 1439,    // 1251 - 1439
      desktop: Infinity,  // 1440+
    },
    defaultBreakpoint: 'mobile_lg'
  })

  export default {
    name: 'UserLoans',

    mixins: [scrollAreaMixin, tableMixin],

    data () {
      return {
        showAddLoan: false,
        showAddLoanValue: false,
        isMobile: false,
        window: {
          width: 0,
          height: 0
        }
      }
    },  

    methods: {           
      ...mapGetters('loans', ['getInitialFirstLoanId', 'selectedLoanId', 'tableColumns']),
      ...mapActions('loans', ['updateSelectedLoanId', 'updateLoanValue', 'deleteLoanValues', 'updateSelectedLoanCurrencySymbol',
      'updateTableColumn', 'addToVisibleColumns', 'removeFromVisibleColumns']),

      handleResize: function() {
        this.window.width = window.innerWidth;
        this.window.height = window.innerHeight;
        if (this.window.width < 768) {
          this.isMobile = true
        }
        else {
          this.isMobile = false
        }
      },

      onUpdateLoanValue(val, row, col) {
        // this.setLoading(true);
        const updatedRow = Object.assign({}, row);
        updatedRow[col] = val;
        const res = this.updateLoanValue(updatedRow);
        res.then((response) => {
          // do nothing
        })
        .catch((err) => {
          err.log(-1);
        })
        .finally(() => {
          // this.setLoading(false);
        });
      },

      promptToDeleteLoanValue() {
        var numSelectedRows = this.selectedValues.length;
        if (numSelectedRows === 0) {
          this.$q.dialog({
            title: '',
            message: `No rows selected. Please check the boxes next to the rows which you want to delete.`,
            ok: {
              label: 'Ok'
            },
            persistent: true
          })
        }
        else {
          this.$q.dialog({
            title: 'Confirm',
            message: `Are you sure you want to delete the ${this.selectedValues.length} selected loan value(s)? This cannot be undone.`,
            ok: {
              label: 'Yes'
            },
            cancel: {
              color: 'negative'
            },
            persistent: true
          }).onOk(() => {
            var valueIds = [];
            this.selectedValues.forEach(el => {
              valueIds.push(el.loanValueId);
            });
            this.deleteLoanValues({ loanId: this.selectedLoanId(), loanValueIds: valueIds }); 
            this.selectedValues = [];
          })
        }
        
      },

      getSelectedString () {
        if (this.selectedValues.length === 0 )
          return ''
        else if (this.selectedValues.length === 1 )
          return `1 record selected of ${this.loanValuesByLoanId( this.selectedLoanId() ).length}`
        else
          return `${this.selectedValues.length} records selected of ${this.loanValuesByLoanId( this.selectedLoanId() ).length}`
      },

      log: function(str) {
        console.log(str);
        var num = 0;
        num = str;
        console.log(num);
        console.log(this.loanValues[num]);
      }
    },

    computed: {
      ...mapGetters('loans', ['loans', 'loanById', 'loanValuesByLoanId', 'loanName', 'visibleColumns']),
      ...mapGetters('main', ['userDisplayCurrencyCode']),

      selectedLoanName() {
        return this.loanName( this.selectedLoanId() )
      },

      selectedLoanValues() {
        var storeLoanVals = this.loanValuesByLoanId(this.selectedLoanId());   // get array from store
        return storeLoanVals.map((b, idx) => Object.assign({ index: idx }, b));   // return a cloned array
      },

// CHART PROPERTIES
      chartOptions() {
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
          colors: ['#BB5601'],
          series: [{
            name: 'Loan Values',
            data: [1000.00, 2000.50, 1500.54, 1856.42, 2254.24, 2354.11],
          }],
           title: {
            text: this.selectedLoanName,
            align: 'center',
            offsetY: 20,
            style: {
              fontSize:  '20px',
              color:  '#a24a01'
            },
          },
          labels: ['2001-01-28', '2001-03-28', '2001-05-28', '2001-07-28', '2001-09-28', '2001-11-28'],
          xaxis: {
            type: 'datetime',
          },
          fill: {
            colors: ['#883F01', '#BB5601']
          }
        }
      },
      series() {
        var storeLoanVals = this.loanValuesByLoanId(this.selectedLoanId());   // get array from store
        var loanVals = storeLoanVals.map((b, idx) => Object.assign({ index: idx }, b));   // clone the array, ref:https://stackoverflow.com/questions/44837957/how-to-clone-a-vuex-array

        loanVals.forEach(obj => {
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
          if (obj.loanValueId !== undefined) {
            delete obj["loanValueId"];
          }
        });

        return [{
            name: this.selectedLoanName,
            data: loanVals
          }]
      }
      // END OF CHART PROPERTIES      
    },

    components : {
      'loan' : require('components/Loans/Loan.vue').default,
      'add-loan' : require('components/Loans/Modals/AddLoan.vue').default,
      'add-loan-value' : require('components/Loans/Modals/AddLoanValue.vue').default
    },

    mounted () {      
      this.updateSelectedLoanId( this.getInitialFirstLoanId() )

      var selectedLoan = this.loanById(this.getInitialFirstLoanId())      
      var symbol = this.getCurrencySymbol(selectedLoan.quotedCurrency.nameShort);
      this.updateSelectedLoanCurrencySymbol(symbol);
      this.updateTableColumn({ columnNo: 1, columnObj: { label: 'Balance (' + selectedLoan.quotedCurrency.code + ' ' + symbol + ')' } });
      if (this.userDisplayCurrencyCode !== selectedLoan.quotedCurrency.code) {
        symbol = this.getCurrencySymbol(this.userDisplayCurrencyCode);
        console.log('userDisplayCurrencyCode: ' + this.userDisplayCurrencyCode);
        console.log('symbol: ' + symbol);
        
        // this.updateSelectedLoanCurrencySymbol(symbol);
        this.updateTableColumn({ columnNo: 3, columnObj: { label: 'Value (' + this.userDisplayCurrencyCode + ' ' + symbol + ')' } })
        this.addToVisibleColumns('rateToUserCurrency')
        this.addToVisibleColumns('valueUserCurrency')
      }
      else {
        this.removeFromVisibleColumns('rateToUserCurrency')
        this.removeFromVisibleColumns('valueUserCurrency')
      }
    },

    created() {
      window.addEventListener('resize', this.handleResize)
      this.handleResize();
    },

    destroyed() {
      window.removeEventListener('resize', this.handleResize)
    },
  }
</script>


<style>
  .footerNotes a {
    text-decoration: none;
    /* font-size: 12pt; */
  }

  .loans-list-div {
    min-width: 485px;
    max-width: 700px;
    margin: 0 auto;
  }

  .loans-list-div-mbl {
    max-width: 100%;
    width: 100%;
    margin: 0 auto;
  }

  .loans-scroll-area {
    height: 570px; 
  }

  .loans-table-div {
    min-width: 485px;
    max-width: 800px;
    margin: 0 auto;
  }

  .loans-table-div-mbl {
    max-width: 100%;
    width: 100%;
    margin: 0 auto;
  }
</style>