<template>
  <q-page padding>

    <div class="row justify-center q-ma-md">
      <div class="col-6 q-mb-lg q-mr-sm">
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
          class=""
          style="height: 540px; min-width: 400px; max-width: 600px;">

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

      <div class="col-auto q-ml-sm">
        <h5 class="q-my-md">Account Values</h5>
        
        <!-- Account Values Table -->
        <div class="q-pa-md">          
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

    <div class="row justify-center q-ma-md">
      <div class="col-12 q-ml-sm">
        <apexchart width="100%" height="500" type="area" :options="chartOptions" :series="series"></apexchart>
      </div>
    </div>
    

    <br /><br /><br /><br /><br /><br /><br />
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
  var moment = require('moment');

  export default {
    name: 'UserAccounts',

    mixins: [scrollAreaMixin, tableMixin],

    data () {
      return {
        showAddAccount: false,
        showAddAccountValue: false
      }
    },  

    methods: {           
      ...mapGetters('accounts', ['getInitialFirstAccountId', 'selectedAccountId', 'tableColumns']),
      ...mapActions('accounts', ['updateSelectedAccountId', 'updateAccountValue', 'deleteAccountValues', 'updateSelectedAccountCurrencySymbol',
      'updateTableColumn', 'addToVisibleColumns', 'removeFromVisibleColumns']),

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
      this.updateTableColumn({ columnNo: 1, columnObj: { label: 'Value (' + selectedAccount.quotedCurrency.code + ' ' + symbol + ')' } });
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
    }
  }
</script>


<style>
  .footerNotes a {
    text-decoration: none;
    /* font-size: 12pt; */
  }
</style>