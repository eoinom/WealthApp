<template>
  <q-card               
    @click="
      updateSelectedLoanId(loan.loanId);
      var symbol = getCurrencySymbol(loan.quotedCurrency.nameShort);
      updateSelectedLoanCurrencySymbol(symbol);
      updateTableColumn({ columnNo: 1, columnObj: { label: 'Value (' + loan.quotedCurrency.code + ' ' + symbol + ')' } });
      "
    class="my-card text-white"
    style="background: radial-gradient(circle, #BB5601 0%, #883F01 100%)" 
    >
    <q-card-section>
      <div class="row">
        
        <div class="col-9 text-h6">{{ loan.loanName }}</div>

        <div class="col-3">
          <div class="float-right">
            <q-btn 
              @click.stop="showEditLoan = true"
              flat 
              round 
              dense
              color="cyan-2" 
              icon="edit" /> 
            <q-btn 
              @click.stop="promptToDeleteLoan(loan.loanId)"
              flat 
              round 
              dense
              color="red-5" 
              icon="delete" />  
          </div>        
        </div>
      </div>

      <div class="text-subtitle2">{{ loan.institution }}</div>
    </q-card-section>
    <q-card-section>
      Type: {{ loan.type }}
      <br />Currency: {{ loan.quotedCurrency.code }}
      <br />Balance: {{ getCurrencySymbol(loan.quotedCurrency.nameShort) + getLoanBalance(loan.loanId) }}
    </q-card-section>

    <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
      content-class="bg-deep-orange" content-style="font-size: 14px" >
      {{ loan.description }}
    </q-tooltip>

    <q-dialog v-model="showEditLoan">
      <edit-loan 
        @close="showEditLoan = false" 
        :loan="loan"
        :loanId="loan.loanId" />
    </q-dialog>
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'

  export default {
    props: ['loan'],
    data() {
      return {
        showEditLoan: false
      }      
    },

    computed: {
      ...mapGetters('loans', ['loans', 'loanValuesByLoanId', 'getLoanBalance', 'selectedLoanId']),
    },
    
    methods: {
      ...mapActions('loans', ['updateSelectedLoanId', 'updateSelectedLoanCurrencySymbol', 'updateTableColumn', 'deleteLoan']),

      promptToDeleteLoan(id) {
        this.$q.dialog({
          title: 'Confirm',
          message: 'Are you sure you want to delete this loan and all associated data? This cannot be undone.',
          ok: {
            label: 'Yes'
          },
          cancel: {
            color: 'negative'
          },
          persistent: true
        }).onOk(() => {
          this.deleteLoan(id)
        })
      },
      log: function(str) {
        console.log(str);
      },
      getLoanBalance: function (loanId) {
        try {
          var numOfValues = this.loanValuesByLoanId(loanId).length;
          if (numOfValues == 0) {
            console.log('No loan values for loanId: ' + loanId);
            return " No loan values";
          }
          else {
            return this.getLoanBalance(loanId).toFixed(2);
          }          
        }
        catch (error) {
          console.error(error); 
          return " Not available";
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

    components: {
      'edit-loan': require('components/Loans/Modals/EditLoan.vue').default
    }
  }
</script>

<style>

</style>