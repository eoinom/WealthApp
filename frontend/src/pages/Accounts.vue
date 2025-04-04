<template>
  <q-page padding>

    <div class="row justify-around q-col-gutter-lg">

<!-- ACCOUNTS LIST -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', mobile_md: 'col-12', tablet_md: 'col-12', tablet_lg: 'col-5'})">
        <div :class="$mq | mq({ mobile_sm: 'accounts-list-div-mbl', mobile_lg: 'accounts-list-div', tablet_lg: 'accounts-list-div'})">
            <h5 class="q-my-md">Accounts</h5>
            <div class="q-mb-sm">
              <q-btn
                @click="showAddAccount = true"
                color="primary"
                icon="add"
                label="Add Account"
                class="q-mb-sm"
                rounded
              />
            </div>
            
            <q-scroll-area 
              :thumb-style="thumbStyle"
              :content-style="contentStyle"
              :content-active-style="contentActiveStyle"
              class="accounts-scroll-area">

              <template v-if="Object.keys(accounts).length > 0">
                
                <account
                  v-for="(account, key) in accounts"
                  :key="key"
                  :account="account"
                  :id="key"
                  class="q-mb-md q-mr-sm">
                </account>

              </template>
            </q-scroll-area>
          </div>
      </div>

<!-- ACCOUNT VALUES TABLE -->
      <div :class="$mq | mq({ mobile_sm: 'col-12', tablet_md: 'col-12', tablet_lg: 'col-5'})">
        <div :class="$mq | mq({ mobile_sm: 'accounts-table-div-mbl', mobile_lg: 'accounts-table-div', tablet_lg: 'accounts-table-div'})">
          <h5 :class="$mq | mq({ mobile_sm: 'q-my-md', tablet_md: 'q-my-md', tablet_lg: 'q-mt-xl q-mb-md'})">Account Balances</h5>
          <div>          
            <q-table
              title="AccountValues"
              :data="selectedAccountValues"
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
                    @click="promptToDeleteAccountValue()" 
                  />
                </div>

                <div class="col">
                  <div class="text-h6 text-primary text-center">{{ selectedAccountName }}</div>
                </div>

                <div class="col-2">
                  <q-btn 
                    round 
                    icon="add" 
                    dense 
                    class="q-ml-md q-my-md"
                    color="primary" 
                    :disable="loading" 
                    @click="showAddAccountValue = true" 
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
                        @input="(val, initval) => onUpdateAccountValue(val, props.row, 'date')"
                      />
                    </q-popup-edit>
                  </q-td>

                  <!-- Value -->
                  <q-td key="value" :props="props">
                    {{ toLocaleFixed(props.row.value, 2) }}

                    <q-popup-edit 
                      v-model="popupEditValue" 
                      @show="() => showPopupValue(props.row, 'value')" 
                      @save="(val, initval) => onUpdateAccountValue(val, props.row, 'value')"
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

    <q-dialog v-model="showAddAccount">
      <add-account @close="showAddAccount = false" />
    </q-dialog>

    <q-dialog v-model="showAddAccountValue">
      <add-account-value 
        :accountId="selectedAccountId()"
        @close="showAddAccountValue = false" />
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
    name: 'UserAccounts',

    mixins: [scrollAreaMixin, tableMixin],

    data () {
      return {
        showAddAccount: false,
        showAddAccountValue: false,
        isMobile: false,
        window: {
          width: 0,
          height: 0
        }
      }
    },  

    methods: {           
      ...mapGetters('accounts', ['getInitialFirstAccountId', 'selectedAccountId', 'tableColumns']),
      ...mapActions('accounts', ['updateSelectedAccountId', 'updateAccountValue', 'deleteAccountValues', 'updateSelectedAccountCurrencySymbol',
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

      onUpdateAccountValue(val, row, col) {
        // this.setLoading(true);
        const updatedRow = Object.assign({}, row);
        updatedRow[col] = val;
        const res = this.updateAccountValue(updatedRow);
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

      promptToDeleteAccountValue() {
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
            message: `Are you sure you want to delete the ${this.selectedValues.length} selected account value(s)? This cannot be undone.`,
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
              valueIds.push(el.accountValueId);
            });
            this.deleteAccountValues({ accountId: this.selectedAccountId(), accountValueIds: valueIds }); 
            this.selectedValues = [];
          })
        }
        
      },

      getSelectedString () {
        if (this.selectedValues.length === 0 )
          return ''
        else if (this.selectedValues.length === 1 )
          return `1 record selected of ${this.accountValuesByAccountId( this.selectedAccountId() ).length}`
        else
          return `${this.selectedValues.length} records selected of ${this.accountValuesByAccountId( this.selectedAccountId() ).length}`
      },

      log: function(str) {
        console.log(str);
        var num = 0;
        num = str;
        console.log(num);
        console.log(this.accountValues[num]);
      }
    },

    computed: {
      ...mapGetters('accounts', ['accounts', 'accountById', 'accountValuesByAccountId', 'accountName', 'visibleColumns']),
      ...mapGetters('main', ['userDisplayCurrencyCode']),

      selectedAccountName() {
        return this.accountName( this.selectedAccountId() )
      },
      selectedAccountValues() {
        var storeAccountVals = this.accountValuesByAccountId(this.selectedAccountId());   // get array from store
        return storeAccountVals.map((b, idx) => Object.assign({ index: idx }, b));   // return a cloned array
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
          series: [{
            name: 'Account Values',
            data: [1000.00, 2000.50, 1500.54, 1856.42, 2254.24, 2354.11]
          }],
           title: {
            text: this.selectedAccountName,
            align: 'center',
            offsetY: 20,
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

    components : {
      'account' : require('components/Accounts/Account.vue').default,
      'add-account' : require('components/Accounts/Modals/AddAccount.vue').default,
      'add-account-value' : require('components/Accounts/Modals/AddAccountValue.vue').default
    },

    mounted () {      
      this.updateSelectedAccountId( this.getInitialFirstAccountId() )

      var selectedAccount = this.accountById(this.getInitialFirstAccountId())      
      var symbol = this.getCurrencySymbol(selectedAccount.quotedCurrency.nameShort);
      this.updateSelectedAccountCurrencySymbol(symbol);
      this.updateTableColumn({ columnNo: 1, columnObj: { label: 'Balance (' + selectedAccount.quotedCurrency.code + ' ' + symbol + ')' } });
      if (this.userDisplayCurrencyCode !== selectedAccount.quotedCurrency.code) {
        symbol = this.getCurrencySymbol(this.userDisplayCurrencyCode);
        console.log('userDisplayCurrencyCode: ' + this.userDisplayCurrencyCode);
        console.log('symbol: ' + symbol);
        
        // this.updateSelectedAccountCurrencySymbol(symbol);
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

  .accounts-list-div {
    min-width: 485px;
    max-width: 700px;
    margin: 0 auto;
  }

  .accounts-list-div-mbl {
    max-width: 100%;
    width: 100%;
    margin: 0 auto;
  }

  .accounts-scroll-area {
    height: 560px; 
  }

  .accounts-table-div {
    min-width: 485px;
    max-width: 800px;
    margin: 0 auto;
  }

  .accounts-table-div-mbl {
    max-width: 100%;
    width: 100%;
    margin: 0 auto;
  }
</style>