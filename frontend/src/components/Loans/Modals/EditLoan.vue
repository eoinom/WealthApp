<template>
  <q-card>
    
    <modal-header>Edit Loan</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-name 
          :loanName.sync="loanToSubmit.loanName" 
          label="Loan name"
          ref="modalLoanName"/>  

        <modal-description 
          :description.sync="loanToSubmit.description" 
          label="Loan description"
          ref="modalLoanDescription"/>    

        <modal-type 
          :type.sync="loanToSubmit.type" 
          label="Loan type"
          :options="loanTypes"
          ref="modalLoanType"/>   

        <modal-institution 
          :institution.sync="loanToSubmit.institution" 
          label="Institution"
          ref="modalLoanInstitution"/>  

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
    props: ['loan', 'loanId'],

    data() {
      return {
        loanToSubmit: {
          loanId: 0,
          loanName: '',          
          description: '',
          type: '',
          institution: '',
          currencyCode: '',
          isActive: false
        }
      }
    },

    computed: {
      ...mapGetters('loans', ['loanTypes'])
    },

    methods: {
      ...mapActions('loans', ['updateLoan']),

      submitForm() {
        this.$refs.modalLoanName.$refs.loanName.validate()
        if (!this.$refs.modalLoanName.$refs.loanName.hasError) {
          this.submitLoan()
        }
      },
      submitLoan() {
        console.log('in submitLoan, this.loanToSubmit:')
        console.log(this.loanToSubmit)
        this.updateLoan(this.loanToSubmit)
        this.$emit('close')
      }
    },

    components: {
      'modal-header': require('components/SharedModals/ModalHeader.vue').default,
      'modal-name': require('components/SharedModals/ModalName.vue').default,
      'modal-description': require('components/SharedModals/ModalDescription.vue').default,
      'modal-type': require('components/SharedModals/ModalType.vue').default,
      'modal-institution': require('components/SharedModals/ModalInstitution.vue').default,
      'modal-currency': require('components/SharedModals/ModalCurrency.vue').default,
      'modal-active': require('components/SharedModals/ModalActive.vue').default,
      'modal-buttons': require('components/SharedModals/ModalButtons.vue').default
    },

    mounted() {
      // this.loanToSubmit = Object.assign({}, this.loan) 
      this.loanToSubmit.loanId = this.loan.loanId
      this.loanToSubmit.loanName = this.loan.loanName 
      this.loanToSubmit.description = this.loan.description
      this.loanToSubmit.type = this.loan.type
      this.loanToSubmit.institution = this.loan.institution
      this.loanToSubmit.currencyCode = this.loan.quotedCurrency.code
      this.loanToSubmit.isActive = this.loan.isActive
    } 
  }
</script>

<style>

</style>

