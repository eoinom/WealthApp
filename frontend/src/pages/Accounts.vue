<template>
  <q-page padding>

    <div class="row justify-center q-ma-md">
      <div class="col-6 q-mb-lg q-mr-sm">
        <h5 class="q-my-md">Accounts</h5>
        <q-scroll-area 
          :thumb-style="thumbStyle"
          :content-style="contentStyle"
          :content-active-style="contentActiveStyle"
          class="xxx"
          style="height: 800px; min-width: 400px; max-width: 600px;">

          <template v-if="Object.keys(bankAccounts).length > 0">
            <div v-for="(a, key) in bankAccounts" 
              v-bind:key="key"             
              class="q-pb-md q-px-sm">

                <q-card               
                  v-on:click="
                    selectedAccountId = a.bankAccountId; 
                    columns[1].label = 'Value (' + a.quotedCurrency.code + ' ' + getCurrencySymbol(a.quotedCurrency.nameShort) + ')'; 
                    "
                  class="my-card text-white"
                  style="background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)" 
                  >
                  <q-card-section>
                    <div class="text-h6">{{ a.accountName }}</div>
                    <div class="text-subtitle2">{{ a.institution }}</div>
                  </q-card-section>
                  <q-card-section>
                    Type: {{ a.type }}
                    <br />Currency: {{ a.quotedCurrency.code }}
                    <br />Balance: {{ getCurrencySymbol(a.quotedCurrency.nameShort) + getAccountBalance(a.bankAccountId).toFixed(2) }}
                    <!-- <br />Balance: {{ getCurrencySymbol(a.quotedCurrency.nameShort) }} -->
                  </q-card-section>

                  <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
                    content-class="bg-deep-orange" content-style="font-size: 14px" >
                    {{ a.description }}
                  </q-tooltip>
                </q-card>
            </div>
          </template>
        </q-scroll-area>
      </div>

      <div class="col-auto q-ml-sm">
        <h5 class="q-my-md">Account Values</h5>
        
        <div class="q-pa-md">
          <q-table
            title="AccountValues"
            :data="bankAccountValuesByAccountId(selectedAccountId)"
            :columns="columns"
            row-key="id"            
            :filter="filter"
            :loading="loading"
            :pagination.sync="pagination"
            >

            <template v-slot:top>
              <q-btn flat dense color="primary" :disable="loading" label="Add row" @click="addRow" />
              <q-btn class="on-right" flat dense color="primary" :disable="loading" label="Remove row" @click="removeRow" />
              <q-space />
              <q-input borderless dense debounce="300" color="primary" v-model="filter">
                <template v-slot:append>
                  <q-icon name="search" />
                </template>
              </q-input>
            </template>

          </q-table>
        </div>

      </div> 

    </div>
  </q-page>
</template>


<script>
  import { mapGetters } from 'vuex'
  import { mapActions } from 'vuex'
  import { mapMutations } from 'vuex'

  export default { 
    name: 'UserAccounts',

    data () {
      return {
        accountBalances: [],
        selectedAccountId: 0,
        loading: false,
        filter: '',
        rowCount: 10,
        pagination: {
          sortBy: 'date',
          descending: true,
          rowsPerPage: 10
        },
        columns: [
          {
            name: 'date',
            required: true,
            label: 'Date',
            align: 'left',
            field: row => row.date,
            format: val => `${val}`,
            sortable: true
          },
          { 
            name: 'value', 
            align: 'center', 
            label: 'Value (EUR €)', 
            field: 'value',
            format: val => Number(val).toFixed(2),
            sortable: true 
          }
        ]        
      }
    },  

    methods: {           
      ...mapGetters('main', ['firstBankAccountId', 'getInitialFirstBankAccountId']),
      ...mapActions('main', ['sortBankAccountValues']),
      addRow () {
        this.loading = true
        setTimeout(() => {
          const
            index = Math.floor(Math.random() * (this.data.length + 1)),
            row = this.original[Math.floor(Math.random() * this.original.length)]
          if (this.data.length === 0) {
            this.rowCount = 0
          }
          row.id = ++this.rowCount
          const addRow = { ...row } // extend({}, row, { name: `${row.name} (${row.__count})` })
          this.data = [...this.data.slice(0, index), addRow, ...this.data.slice(index)]
          this.loading = false
        }, 500)
      },

      removeRow () {
        this.loading = true
        setTimeout(() => {
          const index = Math.floor(Math.random() * this.data.length)
          this.data = [...this.data.slice(0, index), ...this.data.slice(index + 1)]
          this.loading = false
        }, 500)
      },

      log: function(str) {
        console.log(str);
        var num = 0;
        num = str;
        console.log(num);
        console.log(this.accountValues[num]);
      },

      getAccountBalance: function (accountId) {
        try {
          var numOfValues = this.bankAccountValuesByAccountId(accountId).length;
          var accountValsSorted = this.bankAccountValuesByAccountId(accountId).slice().sort(function(a, b) {
              var dateA = new Date(a.date);
              var dateB = new Date(b.date);
              return dateA - dateB;
          });
          return accountValsSorted[ numOfValues - 1 ].value;
        }
        catch (error) {
          console.error(error); 
          return "";
        }        
      },

      getCurrencySymbol: function (shortName) {
        try {
          switch ( shortName.toLowerCase() ) {
            case 'euro':
              return '€';
              break;
            case 'dollar':
            case 'peso':
              return '$';
              break;
            case 'pound':
              return '£';
              break;
            default:
              return '';
          }
        }
        catch (error) {
          console.error(error); 
          return "";
        } 
      },
    },

    computed: {
      ...mapGetters('main', ['bankAccounts', 'bankAccountValuesByAccountId', 'getBankAccountBalance']),
      
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

    mounted () {
      this.selectedAccountId = this.getInitialFirstBankAccountId()
      // console.log('in mounted')
      // var initialSelectedAccountId = this.selectedAccountId
      // console.log('initialSelectedAccountId= ' + initialSelectedAccountId)
      // var firstAcc = firstBankAccountId()
      // // var firstAcc = this.firstBankAccountId(() => {
      // //   console.log('firstAcc= ' + firstAcc)
      // //   this.selectedAccountId = firstAcc
      // //   console.log('new selectedAccountId = ' + this.selectedAccountId)
      // //   this.deleteBankAccount(initialSelectedAccountId)
      // // })
      // console.log('firstAcc= ' + firstAcc)
      // // this.selectedAccountId = firstAcc
      // // console.log('new selectedAccountId = ' + this.selectedAccountId)
      // // this.deleteBankAccount(initialSelectedAccountId)
    }
  }
</script>


<style>
  
</style>