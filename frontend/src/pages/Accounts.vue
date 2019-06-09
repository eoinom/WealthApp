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
          style="height: 600px; min-width: 400px; max-width: 600px;">

          <div v-for="a in userAccounts" v-bind:key="a.bankAccountId" class="q-pa-sm">
            <q-card
            v-on:click="
              selectedAccountId = a.bankAccountId; 
              columns[1].label = 'Value ' + a.quotedCurrency.code; 
              "
            class="my-card text-white"
            style="background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)" >
              <q-card-section>
                <div class="text-h6">{{ a.accountName }}</div>
                <div class="text-subtitle2">{{ a.institution }}</div>
              </q-card-section>
              <q-card-section>
                Type: {{ a.type }}
                <br />Currency: {{ a.quotedCurrency.code }}
                <br />Balance: {{ getAccountBalance(a.bankAccountId) }}
              </q-card-section>

              <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
                content-class="bg-deep-orange" content-style="font-size: 14px" >
                {{ a.description }}
              </q-tooltip>
            </q-card>
          </div>

        </q-scroll-area>
      </div>

      <div class="col-auto q-ml-sm">
        <h5 class="q-my-md">Account Values</h5>
        
        <div class="q-pa-md">
          <q-table
            title="AccountValues"
            :data="accountValues[selectedAccountId - firstAccountId]"
            :columns="columns"
            row-key="id"
            :filter="filter"
            :loading="loading"
            binary-state-sort >

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
  export default {
    name: 'UserAccounts',
    data () {
      return {
        userId: 2,
        userAccounts: [],
        accountValues: [[]],
        firstAccountId: -1,
        selectedAccountId: -1,
        loading: false,
        filter: '',
        rowCount: 10,
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
            label: 'Value (EUR)', 
            field: 'value',
            format: val => Number(val).toFixed(2),
            sortable: true 
          }
        ]        
      }
    },
    async created () {
      try {
        var response = await this.$axios({
          method: "POST",
          url: "/",
          data: {
            query: `
              {
                bankAccount_queries {
                  userBankAccounts(userId: ` + this.userId + `) {
                    bankAccountId
                    accountName
                    description      
                    type
                    isActive
                    institution
                    quotedCurrency {
                      code
                      nameShort
                      nameLong        
                    }
                  }
                }
              }
            `
          }
        });
        this.userAccounts = response.data.data.bankAccount_queries.userBankAccounts;
        this.firstAccountId = this.userAccounts[0].bankAccountId;
        this.selectedAccountId = this.firstAccountId;
        console.log("selectedAccountId: " + this.selectedAccountId);
      } catch (error) {
        console.error(error); 
      }
      
      for (var i = 0; i < this.userAccounts.length; i++) {
        try {
          var response = await this.$axios({
            method: "POST",
            url: "/",
            data: {
              query: `
                {
                  accountValue_queries {
                    accountValues(accountId: ` + this.userAccounts[i].bankAccountId + `) {
                      date
                      value
                    }
                  }
                }
              `
            }
          });
          this.accountValues[i] = response.data.data.accountValue_queries.accountValues.sort(function(a, b) {
              var dateA = new Date(a.date);
              var dateB = new Date(b.date);
              return dateA - dateB;
          });
        } catch (error) {
          console.error(error); 
        }
        console.log("i = " + i);
        console.log(this.accountValues[i]);
      }
      
    },
    methods: {
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
      testMethod () {
        console.log(2)
        populateAccountValues(7)
      },
      populateAccountValues: function (accountId) {
        try {
          var response = this.$axios({
            method: "POST",
            url: "/",
            data: {
              query: `
                {
                  accountValue_queries {
                    accountValues(accountId: ` + accountId +`) {
                      date
                      value
                      }
                    }
                  }
                }
              `
            }
          });
          this.accountValues = response.data.data.accountValue_queries.accountValues;
        } catch (error) {
          console.error(error); 
        }
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
          console.log("Account Values: ");
          console.log(accountValues);
          var numOfValues = accountValues[accountId - firstAccountId].length;
          console.log(numOfValues);
          return accountValues[accountId - firstAccountId][ numOfValues - 1 ].value;
        }
        catch (error) {
          console.error(error); 
          return "";
        }        
      }
    },
    computed: {
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
    }
  }
</script>


<style>
  
</style>