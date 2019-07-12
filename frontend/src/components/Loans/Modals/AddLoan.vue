<template>
  <q-card>
    
    <modal-header>Add Loan</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-name 
          :name.sync="loanToSubmit.loanName" 
          label="Loan name"
          ref="modalLoanName"/>  

        <modal-description 
          :description.sync="loanToSubmit.description" 
          label="Loan description"
          ref="modalLoanDescription"/>   

        <modal-institution 
          :institution.sync="loanToSubmit.institution" 
          label="Institution"
          ref="modalLoanInstitution"/>   

        <modal-type 
          :type.sync="loanToSubmit.type" 
          label="Loan type"
          :options="loanTypes"
          ref="modalLoanType"/>   

        

        <modal-currency 
          :currencyCode.sync="loanToSubmit.currencyCode" 
          ref="modalLoanCurrency"/>   

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
      ...mapGetters('loans', ['loanTypes'])
    },

    methods: {
      ...mapActions('loans', ['addLoan']),   

      submitForm() {
        this.$refs.modalLoanName.$refs.name.validate()
        if (!this.$refs.modalLoanName.$refs.name.hasError) {
          this.submitLoan()
        }
      },
      submitLoan() {
        this.addLoan(this.loanToSubmit)
        this.$emit('close')
      }
    },

    components: {
      'modal-header': require('components/SharedModals/ModalHeader.vue').default,
      'modal-name': require('components/SharedModals/ModalName.vue').default,
      'modal-description': require('components/SharedModals/ModalDescription.vue').default,
      'modal-type': require('components/SharedModals/ModalType.vue').default,
      'modal-institution': require('components/SharedModals/ModalInstitution.vue').default,
      'modal-currency': require('components/SharedModals/ModalCurrencySelect.vue').default,
      'modal-active': require('components/SharedModals/ModalActive.vue').default,
      'modal-buttons': require('components/SharedModals/ModalButtons.vue').default
    }      
  }
</script>

<style>

</style>

