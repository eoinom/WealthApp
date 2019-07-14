<template>
  <q-card>
    
    <modal-header>Edit Loan</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-name 
          :name.sync="loanToSubmit.loanName" 
          label="Loan Name"
          ref="modalLoanName"/>  

        <modal-description 
          :description.sync="loanToSubmit.description" 
          label="Description"
          ref="modalLoanDescription"/>   

        <modal-institution 
          :institution.sync="loanToSubmit.institution" 
          label="Institution"
          ref="modalLoanInstitution"/>   

        <modal-select 
          :selectValue.sync="loanToSubmit.type" 
          :selectArr="loanTypes"
          label="Type"
          ref="modalLoanType"/>   

        <modal-currency-select
          :currencyCode.sync="loanToSubmit.currencyCode" 
          ref="modalLoanCurrency"/>  

        <modal-currency-value 
          :currencyValue.sync="loanToSubmit.startPrincipal" 
          v-model="loanToSubmit.startPrincipal"
          :currencySymbol="getCurrencySymbol(loanToSubmit.currencyCode)"
          label="Starting Principal"
          ref="modalStartPrincipal"/>  

        <modal-date-uptoToday 
          :date.sync="loanToSubmit.startDate"
          label="Start Date"           
          ref="modalStartDate"/>  

        <modal-select 
          :selectValue.sync="loanToSubmit.repaymentFrequency" 
          v-model="loanToSubmit.repaymentFrequency"
          :selectArr="repaymentPeriods"
          label="Repayent Frequency"          
          ref="modalRepaymentFrequency"/> 

        <modal-select 
          :selectValue.sync="loanToSubmit.rateType" 
          :selectArr="rateTypes"
          label="Rate Type"          
          ref="modalRateType"/> 

        <modal-decimalValue 
          :decimalValue.sync="loanToSubmit.aprRate" 
          v-model="loanToSubmit.aprRate"
          :decimals="3" 
          step="0.001" 
          label="APR Rate"
          suffix="%"
          ref="modalAprRate"/>
        
        <q-badge color="secondary">
          Total Term: {{ loanToSubmit.totalTerm }} Years
        </q-badge>
        <q-slider
          v-model="loanToSubmit.totalTerm"
          :min="0"
          :max="50"
          :step="0.5" 
          class="q-mb-md" />

        <div v-if="loanToSubmit.rateType === 'Variable'">
          <q-badge color="secondary">
            Fixed Term: {{ loanToSubmit.fixedTerm }} Years
          </q-badge>
          <q-slider
            v-model="loanToSubmit.fixedTerm"
            :min="0"
            :max="loanToSubmit.totalTerm"
            :step="0.5" 
            color="red"
            class="q-mb-md" />
        </div>
        
        <div class="row q-mb-sm">        
          <q-input 
            outlined
            type="number"    
            step="0.01"         
            :decimals="2"      
            v-model="loanToSubmit.repaymentAmount"
            :prefix="getCurrencySymbol(loanToSubmit.currencyCode)"
            label="Repayment Amount"
            :rules="[val => !!val || 'Field is required', val => val >= 0 || 'Can not be negative']"
            ref="currencyValue"
            class="col"
            clearable
          >
            <template v-slot:append>
              <q-icon name="fas fa-calculator" v-on:click="calculateRepayment()"/>
            </template>
          </q-input>
        </div>

        <!-- <modal-active
          :isActive.sync="loanToSubmit.isActive" 
          label="Generate Repayment Values?" 
          ref="modalLoanIsActive"/>   -->
          
        <modal-active
          :isActive.sync="loanToSubmit.isActive" 
          label="Loan active?" 
          ref="modalLoanIsActive"/>  

      </q-card-section>

      <modal-buttons />

    </form>
    
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'

  export default {
    props: ['loan', 'loanId'],

    data() {
      return {
        loanToSubmit: {
          loanName: '',          
          description: '',
          institution: '',
          type: '',
          startPrincipal: 0.00,
          startDate: '',
          totalTerm: 0,
          fixedTerm: 0,
          rateType: '',
          aprRate: 0.00,
          repaymentFrequency: '',
          repaymentAmount: 0.00,
          isActive: false,
          currencyCode: '',          
        }
      }
    },

    computed: {
      ...mapGetters('loans', ['loanTypes', 'rateTypes', 'repaymentPeriods']),
      ...mapGetters('main', ['userDisplayCurrencyCode'])
    },

    methods: {
      ...mapActions('loans', ['updateLoan']),
      ...mapActions('main', ['formatDate_User2Iso']), 

      calculateRepayment() {        
        if (this.loanToSubmit.repaymentFrequency !== '' && this.loanToSubmit.startPrincipal > 0 && 
        this.loanToSubmit.totalTerm > 0 && this.loanToSubmit.aprRate > 0) 
        {
          var noPeriods = 0
          switch ( this.loanToSubmit.repaymentFrequency.toLowerCase() ) {
            case 'weekly':          noPeriods = 52;            break;
            case 'fortnightly':     noPeriods = 26;            break;
            case 'monthly':         noPeriods = 12;            break;
            case 'bi-monthly':      noPeriods = 6;            break;
            case 'quarterly':       noPeriods = 4;            break;
            case 'half-annually':
            case 'half-yearly': 
            case 'bi-annually':     noPeriods = 2;            break;
            case 'annually':
            case 'yearly':          noPeriods = 1;            break;
            default:
              noPeriods = 12;
          }
          var n = noPeriods * this.loanToSubmit.totalTerm
          var i = this.loanToSubmit.aprRate / 100 / noPeriods
          var D = ( Math.pow(1.0 + i, n) - 1.0) / (i * Math.pow(1.0 + i, n))
          this.loanToSubmit.repaymentAmount = (this.loanToSubmit.startPrincipal / D).toFixed(2)
        }
        else 
          this.loanToSubmit.repaymentAmount = 0.0
      },

      submitForm() {
        this.$refs.modalLoanName.$refs.name.validate()
        if (!this.$refs.modalLoanName.$refs.name.hasError) {
          this.loanToSubmit.quotedCurrency = this.loanToSubmit.currencyCode
          delete this.loanToSubmit.currencyCode
          this.loanToSubmit.aprRate /= 100
          this.formatDate_User2Iso(this.loanToSubmit.startDate).then(res=>
          {
            this.loanToSubmit.startDate = res; 
            this.submitLoan()
          });
        }
      },
      submitLoan() {
        this.updateLoan(this.loanToSubmit)
        this.$emit('close')
      }
    },

    components: {
      'modal-active':           require('components/SharedModals/ModalActive.vue').default,
      'modal-buttons':          require('components/SharedModals/ModalButtons.vue').default,
      'modal-currency-select':  require('components/SharedModals/ModalCurrencySelect.vue').default,
      'modal-currency-value':   require('components/SharedModals/ModalCurrencyValue.vue').default,
      'modal-date-uptoToday':   require('components/SharedModals/ModalDateUptoToday.vue').default,
      'modal-decimalValue':     require('components/SharedModals/ModalDecimalValue.vue').default,
      'modal-description':      require('components/SharedModals/ModalDescription.vue').default,
      'modal-header':           require('components/SharedModals/ModalHeader.vue').default,
      'modal-institution':      require('components/SharedModals/ModalInstitution.vue').default,
      'modal-intValue':         require('components/SharedModals/ModalIntegerValue.vue').default,
      'modal-name':             require('components/SharedModals/ModalName.vue').default,
      'modal-select':           require('components/SharedModals/ModalSelect.vue').default,
      'modal-value-date':       require('components/SharedModals/ModalValueDate.vue').default
    },

    mounted() {
      this.loanToSubmit = Object.assign({}, this.loan) 
      this.loanToSubmit.currencyCode = this.loan.quotedCurrency.code
      delete this.loanToSubmit.quotedCurrency
    } 
  }
</script>

<style>

</style>

