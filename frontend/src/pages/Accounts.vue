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

          <div v-for="n in 10" :key="n" class="q-pa-sm">
            <q-card
            class="my-card text-white"
            style="background: radial-gradient(circle, #35a2ff 0%, #014a88 100%)" >
              <q-card-section>
                <div class="text-h6">Main Current Account</div>
                <div class="text-subtitle2">Bank of Ireland</div>
              </q-card-section>
              <q-card-section>
                Type: Current
                <br />Currency: EUR
                <br />Balance: 1324.22
              </q-card-section>
            </q-card>
          </div>

        </q-scroll-area>
      </div>
    <!-- </div> -->

    <!-- <div class="q-ma-md"> -->
      <div class="col-auto q-ml-sm">
        <h5 class="q-my-md">Account Values</h5>
        
        <div class="q-pa-md">
          <q-table
            title="Treats"
            :data="data"
            :columns="columns"
            row-key="id"
            :filter="filter"
            :loading="loading" >

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
    data () {
      return {
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
          { name: 'value', align: 'center', label: 'Value (EUR)', field: 'value', sortable: true }
        ],
        data: [
          {
            id: 1,
            date: '2019-05-01',
            value: 159
          },
          {
            id: 2,
            date: '2019-04-01',
            value: 237
          },
          {
            id: 3,
            date: '2019-03-01',
            value: 262
          },
          {
            id: 4,
            date: '2019-02-01',
            value: 305
          },
          {
            id: 5,
            date: '2019-01-01',
            value: 356
          },
          {
            id: 6,
            date: '2018-12-01',
            value: 375
          },
          {
            id: 7,
            date: '2018-11-01',
            value: 392
          },
          {
            id: 8,
            date: '2018-10-01',
            value: 408
          },
          {
            id: 9,
            date: '2018-09-01',
            value: 452
          },
          {
            id: 10,
            date: '2018-08-01',
            value: 518
          }
        ],
        original: [
          {
            date: '2019-05-01',
            value: 159
          },
          {
            date: '2019-04-01',
            value: 237
          },
          {
            date: '2019-03-01',
            value: 262
          },
          {
            date: '2019-02-01',
            value: 305
          },
          {
            date: '2019-01-01',
            value: 356
          },
          {
            date: '2018-12-01',
            value: 375
          },
          {
            date: '2018-11-01',
            value: 392
          },
          {
            date: '2018-10-01',
            value: 408
          },
          {
            date: '2018-09-01',
            value: 452
          },
          {
            date: '2018-08-01',
            value: 518
          }
        ]
      }
    },
    methods: {
      // emulate fetching data from server
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