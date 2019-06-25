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
            :selected.sync="selected"
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

            <template v-slot:body="props">
              <q-tr :props="props">
                <q-td key="date">
                  {{ props.row.date }}
                  <q-popup-edit v-model="props.row.date">
                    <q-date
                      v-model="props.row.date"
                      mask="YYYY-MM-DD"
                    />
                  </q-popup-edit>
                </q-td>

                <q-td key="value" :props="props">
                  {{ props.row.value }}
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

    <div class="q-mt-md">
      Selected: {{ JSON.stringify(selected) }}
    </div>

    <!-- <div class="absolute-bottom text-center q-mb-lg">
      <q-btn
      @click="showAddAccount = true"
        round
        color="primary"
        size="24px"
        icon="add"
      />
    </div> -->

    <q-dialog v-model="showAddAccount">
      <add-account @close="showAddAccount = false" />
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
        selected: [],
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

      getSelectedString () {
        if (this.selected.length === 0 )
          return ''
        else if (this.selected.length === 1 )
          return `1 record selected of ${this.bankAccountValuesByAccountId( this.selectedAccountId() ).length}`
        else
          return `${this.selected.length} records selected of ${this.bankAccountValuesByAccountId( this.selectedAccountId() ).length}`
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
      'add-account' : require('components/Accounts/Modals/AddAccount.vue').default
    },

    mounted () {
      this.updateSelectedAccountId( this.getInitialFirstBankAccountId() )
    }
  }
</script>


<style>
  
</style>