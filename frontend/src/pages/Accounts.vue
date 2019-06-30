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
          style="height: 600px; min-width: 400px; max-width: 600px;">

          <template v-if="Object.keys(bankAccounts).length > 0">
            
            <account
              v-for="(account, key) in bankAccounts"
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
        
        <div class="q-pa-md">
          <q-table
            title="AccountValues"
            :data="bankAccountValuesByAccountId( selectedAccountId() )"
            :columns="tableColumns()"
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
                <div class="text-h6 text-primary text-center">{{ bankAccountName( selectedAccountId() ) }}</div>
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
                <q-td key="date">
                  {{ props.row.date }}
                  <q-popup-edit v-model="props.row.date">
                    <q-date
                      v-model="props.row.date"
                      mask="getDateFormat"
                    />
                  </q-popup-edit>
                </q-td>

                <q-td key="value" :props="props">
                  {{ props.row.value.toFixed(2) }}
                  <q-popup-edit v-model="props.row.value" title="Update value" buttons>
                    <q-input 
                      type="number" 
                      v-model="props.row.value" 
                      dense 
                      autofocus />
                  </q-popup-edit>
                </q-td>                
              </q-tr>              
            </template>
          </q-table>
        </div> 
      </div> 
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
  // import { mapMutations } from 'vuex'

  export default { 
    name: 'UserAccounts',

    data () {
      return {
        showAddAccount: false,
        showAddAccountValue: false,
        selectedValues: [],
        loading: false,
        filter: '',
        rowCount: 10,
        pagination: {
          sortBy: 'date',
          descending: true,
          rowsPerPage: 10
        },
      }
    },  

    methods: {           
      ...mapGetters('main', ['getInitialFirstBankAccountId', 'getDateFormat']),
      ...mapGetters('accounts', ['selectedAccountId', 'tableColumns']),
      ...mapActions('main', ['deleteBankAccountValues']),
      ...mapActions('accounts', ['updateSelectedAccountId']),

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
            this.deleteBankAccountValues({ accountId: this.selectedAccountId(), valueIds: this.selectedValues }); 
            this.selectedValues = [];
          })
        }
        
      },

      getSelectedString () {
        if (this.selectedValues.length === 0 )
          return ''
        else if (this.selectedValues.length === 1 )
          return `1 record selected of ${this.bankAccountValuesByAccountId( this.selectedAccountId() ).length}`
        else
          return `${this.selectedValues.length} records selected of ${this.bankAccountValuesByAccountId( this.selectedAccountId() ).length}`
      },

      log: function(str) {
        console.log(str);
        var num = 0;
        num = str;
        console.log(num);
        console.log(this.accountValues[num]);
      },

      customTableSort(rows, sortBy, descending) {
        let data = [...rows]

        if (sortBy) {
          data.sort((a, b) => {
            let x = descending ? b : a
            let y = descending ? a : b
            if (sortBy === 'date') {
              // string sort
              // return x[sortBy] > y[sortBy] ? 1 : x[sortBy] < y[sortBy] ? -1 : 0
              
              // date sort
              var xDate = new Date();
              var yDate = new Date();
              var dd = '';
              var mm = '';
              var yyyy = '';
          
              switch(this.getDateFormat()) {
                case "YYYY-MM-DD":
                case "MM/DD/YYYY":
                  xDate = new Date(x[sortBy]);
                  d2 = new Date(y[sortBy]);
                  break;
                case "DD-MM-YYYY":
                case "DD/MM/YYYY":
                  dd = x[sortBy].slice(0,2)
                  mm = x[sortBy].slice(3,5)
                  yyyy = x[sortBy].slice(6,10)
                  xDate = new Date(yyyy + '-' + mm + '-' + dd);
                  dd = y[sortBy].slice(0,2)
                  mm = y[sortBy].slice(3,5)
                  yyyy = y[sortBy].slice(6,10)
                  yDate = new Date(yyyy + '-' + mm + '-' + dd);
                  break;
                case "MM-DD-YYYY":
                  mm = x[sortBy].slice(0,2)
                  dd = x[sortBy].slice(3,5)
                  yyyy = x[sortBy].slice(6,10)
                  xDate = new Date(yyyy + '-' + mm + '-' + dd);
                  mm = y[sortBy].slice(0,2)
                  dd = y[sortBy].slice(3,5)
                  yyyy = y[sortBy].slice(6,10)
                  yDate = new Date(yyyy + '-' + mm + '-' + dd);
                  break;
                case "YYYY/MM/DD":
                  yyyy = x[sortBy].slice(0,4)
                  mm = x[sortBy].slice(5,7)
                  dd = x[sortBy].slice(8,10)
                  xDate = new Date(yyyy + '-' + mm + '-' + dd);
                  yyyy = y[sortBy].slice(0,4)
                  mm = y[sortBy].slice(5,7)
                  dd = y[sortBy].slice(8,10)
                  yDate = new Date(yyyy + '-' + mm + '-' + dd);
                  break;
                default:
                  // do nothing
              }
              return xDate - yDate;
            }
            else {
              // numeric sort
              return parseFloat(x[sortBy]) - parseFloat(y[sortBy])
            }
          })
        }
        return data
      }
    },

    computed: {
      ...mapGetters('main', ['bankAccounts', 'bankAccountValuesByAccountId', 'bankAccountName']),
      
      contentStyle () {
        return {
          backgroundColor: 'rgba(0,0,0,0.02)',
          color: '#555'
        }
      },

      contentActiveStyle () {
        return {
          backgroundColor: '#eee',
          color: 'black'
        }
      },

      thumbStyle () {
        return {
          right: '2px',
          borderRadius: '5px',
          backgroundColor: '#027be3',
          width: '5px',
          opacity: 0.75
        }
      }
    },

    components : {
      'account' : require('components/Accounts/Account.vue').default,
      'add-account' : require('components/Accounts/Modals/AddAccount.vue').default,
      'add-account-value' : require('components/Accounts/Modals/AddAccountValue.vue').default
    },

    mounted () {
      this.updateSelectedAccountId( this.getInitialFirstBankAccountId() )
    }
  }
</script>


<style>
  
</style>