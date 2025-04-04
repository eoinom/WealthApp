<template>
  <q-card               
    @click="updateForSelectedId()"
    class="loan-card text-white"
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
      <div class="row"> 
        <div class="col-9">
          Type: {{ loan.type }}
          <br />Currency: {{ loan.quotedCurrency.code }}
          <br />Balance: {{ getCurrencySymbol(loan.quotedCurrency.nameShort) + getLoanBalance(loan.loanId) }}
          <br />Start Principal: {{ getCurrencySymbol(loan.quotedCurrency.nameShort) + toLocaleFixed(loan.startPrincipal, 2) }}
          <br />Start Date: {{ getFormattedDate(loan.startDate) }}
          <br />Total Term: {{ getTermStr(loan.totalTerm, loan.repaymentFrequency) }}
          <br />Rate: {{ (100*loan.aprRate).toFixed(2) + '% (' + loan.rateType + ')' }}
          <br />Repayment: {{ getCurrencySymbol(loan.quotedCurrency.nameShort) + toLocaleFixed(loan.repaymentAmount) + ' ' + loan.repaymentFrequency }}
        </div>  
        <div class="col self-center institutionLogo" v-if="getInstitutionUrl(loan.institution) != ''">
          <img :src="getInstitutionLogoSrc(loan.institution, undefined)">
        </div>
      </div>
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
  import { institutionUrlsMixin } from '../../mixins/institutionUrlsMixin'

  export default {
    props: ['loan'],
    mixins: [institutionUrlsMixin],
    data() {
      return {
        showEditLoan: false,
        formattedDate: ''
      }      
    },

    computed: {
      ...mapGetters('loans', ['loans', 'loanValuesByLoanId', 'loanBalance', 'selectedLoanId']),
      ...mapGetters('main', ['userDisplayCurrencyCode'])
    },
    
    methods: {
      ...mapActions('loans', ['updateSelectedLoanId', 'updateSelectedLoanCurrencySymbol', 'updateTableColumn', 'deleteLoan',
      'addToVisibleColumns', 'removeFromVisibleColumns']),

      ...mapActions('main', ['formatDate_Iso2User']),

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
      getFormattedDate: function(date) {        
        this.formatDate_Iso2User(date).then(res => {this.formattedDate = res})        
        return this.formattedDate 
      },
      getLoanBalance: function (loanId) {
        try {
          var numOfValues = this.loanValuesByLoanId(loanId).length;
          if (numOfValues == 0) {
            console.log('No loan values for loanId: ' + loanId);
            return " No loan values";
          }
          else {
            // return this.loanBalance(loanId).toFixed(2);
            return this.toLocaleFixed(this.loanBalance(loanId), 2);
          }          
        }
        catch (error) {
          console.error(error); 
          return " Not available";
        }        
      },
      getTermStr: function (totalTerm, repaymentFrequency) {
        try {
          switch ( repaymentFrequency.toLowerCase() ) {
            case 'weekly':
              return totalTerm + ' weeks (' + totalTerm/52 + ' years)';
              break;
            case 'bi-weekly':
            case 'fortnightly':
              return totalTerm + ' fortnights (' + totalTerm/26 + ' years)';
              break;
            case 'monthly':
              return totalTerm + ' months (' + totalTerm/12 + ' years)';
              break;
            case 'bi-monthly':
              return 2*totalTerm + ' months (' + totalTerm/6 + ' years)';
              break;
            case 'quarterly':
              return totalTerm + ' quarters (' + totalTerm/4 + ' years)';
              break;
            case 'half-annually':
            case 'half-yearly':
            case 'bi-annually':
            case 'bi-yearly':
              return totalTerm/2 + ' years';
              break;
            case 'annually':
            case 'yearly':
              return totalTerm + ' years';
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
      updateForSelectedId: function () {
        this.updateSelectedLoanId(this.loan.loanId);
        var symbol = this.getCurrencySymbol(this.loan.quotedCurrency.nameShort);
        this.updateSelectedLoanCurrencySymbol(symbol);
        this.updateTableColumn({ columnNo: 1, columnObj: { label: 'Balance (' + this.loan.quotedCurrency.code + ' ' + symbol + ')' } });
        
        if (this.userDisplayCurrencyCode !== this.loan.quotedCurrency.code) {
          symbol = this.getCurrencySymbol(this.userDisplayCurrencyCode);          
          this.updateTableColumn({ columnNo: 3, columnObj: { label: 'Value (' + this.userDisplayCurrencyCode + ' ' + symbol + ')' } })
          this.addToVisibleColumns('rateToUserCurrency')
          this.addToVisibleColumns('valueUserCurrency')
        }
        else {
          this.removeFromVisibleColumns('rateToUserCurrency')
          this.removeFromVisibleColumns('valueUserCurrency')
        }
      }
    },

    components: {
      'edit-loan': require('components/Loans/Modals/EditLoan.vue').default
    }
  }
</script>

<style>
  .institutionLogo {
    text-align: right;
  }

  .institutionLogo img {
    max-width: 150px;
    max-height: 70px;
    height: auto;
  }

  .loan-card {
    background: radial-gradient(circle, #BB5601 0%, #883F01 100%)
  }
</style>