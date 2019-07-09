<template>
  <q-page padding>

    <div class="row justify-center q-ma-md">
      <div class="col-6 q-mb-lg q-mr-sm">
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
          class=""
          style="height: 540px; min-width: 400px; max-width: 600px;">

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

      <div class="col-auto q-ml-sm">
        <h5 class="q-my-md">Loan Values</h5>
        
        <div class="q-pa-md">
          <!-- Loan Values Table -->
          <q-table
            title="LoanValues"
            :data="selectedLoanValues"
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
                  @click="promptToDeleteLoanValue()" 
                />
              </div>

              <div class="col">
                <div class="text-h6 text-primary text-center">{{ selectedLoanName }}</div>
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
                  {{ props.row.date }}
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
                  <!-- {{ props.row.value.toFixed(2) }} -->
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

  export default {
    name: 'UserLoans',
    data () {
      return {
        showAddLoan: false,
        showAddLoanValue: false,
        selectedValues: [],         // selected rows in table
        popupEditDate: '2019-01-31',
        popupEditValue: 0,
        loading: false,
        filter: '',
        pagination: {
          sortBy: 'date',
          descending: true,
          rowsPerPage: 7
        },
      }
    },  

    methods: {           
      ...mapGetters('loans', ['getInitialFirstLoanId', 'selectedLoanId', 'tableColumns']),
      ...mapActions('loans', ['updateSelectedLoanId', 'updateLoanValue', 'deleteLoanValues']),

      showPopupDate(row, col) {
        this.popupEditDate = row[col];
      },
      showPopupValue(row, col) {
        console.log('row: ' + row + ' , col: ' + col + ' row[col]: ' + row[col])
        this.popupEditValue = row[col];
      },
      dateOptionsFn(date) {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth()+1; 
        var yyyy = today.getFullYear();
        if(dd<10) 
            dd='0'+dd;
        if(mm<10) 
            mm='0'+mm;
        return date <= yyyy + '/' + mm + '/' + dd;
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
          
              switch(this.getDateFormat) {
                case "YYYY-MM-DD":
                case "MM/DD/YYYY":
                  xDate = new Date(x[sortBy]);
                  yDate = new Date(y[sortBy]);
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
      ...mapGetters('loans', ['loans', 'loanValuesByLoanId', 'loanName']),
      ...mapGetters('main', ['getDateFormat']),

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
            align: 'center'
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
      },
// END OF CHART PROPERTIES
      
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
      'loan' : require('components/Loans/Loan.vue').default,
      'add-loan' : require('components/Loans/Modals/AddLoan.vue').default,
      'add-loan-value' : require('components/Loans/Modals/AddLoanValue.vue').default
    },

    mounted () {      
      this.updateSelectedLoanId( this.getInitialFirstLoanId() )
    }
  }
</script>


<style>
  
</style>