<template>
  <q-page padding>

    <div class="row justify-center q-ma-md">
      <div class="col-6 q-mb-lg q-mr-sm">
        <h5 class="q-my-md">Accounts</h5>
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
              class="q-mb-md q-mx-sm">
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
  // import { mapMutations } from 'vuex'

  export default { 
    name: 'UserAccounts',

    data () {
      return {
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
      ...mapGetters('main', ['getInitialFirstBankAccountId']),
      ...mapGetters('accounts', ['selectedAccountId', 'tableColumns']),
      ...mapActions('accounts', ['updateSelectedAccountId']),

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
    },

    computed: {
      ...mapGetters('main', ['bankAccounts', 'bankAccountValuesByAccountId']),
      
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
    },

    mounted () {
      this.updateSelectedAccountId( this.getInitialFirstBankAccountId() )
    }
  }
</script>


<style>
  
</style>