<template>
  <q-card>
    
    <modal-header>Add Loan Value</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-value-date 
          :valueDate.sync="loanValueToSubmit.date"           
          ref="modalLoanValueDate"
          />  

        <modal-value 
          :value.sync="loanValueToSubmit.value" 
          :currencySymbol="selectedLoanCurrencySymbol"
          ref="modalLoanValue"/>    

      </q-card-section>

      <modal-buttons />

    </form>
    
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'
  import { constants } from 'crypto'
  var moment = require('moment')

  export default {
    props: ['loanId'],
    data() {
      return {
        loanValueToSubmit: {
          date: '',
          value: '',
          loanId: this.loanId,
        }
      }
    },

    computed: {
      ...mapGetters('loans', ['selectedLoanCurrencySymbol']),
      ...mapGetters('main', ['getDateFormat']),
    },

    methods: {
      ...mapActions('loans', ['addLoanValue']),      

      submitForm() {
        // this.$refs.modalLoanValueDate.$refs.loanValueDate.validate()
        // Need to do date validation
        this.$refs.modalLoanValue.$refs.value.validate()
        if (!this.$refs.modalLoanValue.$refs.value.hasError) {
          this.submitLoanValue()
        }
      },
      submitLoanValue() {
        this.loanValueToSubmit.date = this.formatDate_User2Iso(this.loanValueToSubmit.date)
        console.log(this.loanValueToSubmit)
        this.addLoanValue(this.loanValueToSubmit)
        this.$emit('close')
      }
    },

    components: {
      'modal-header': require('components/SharedModals/ModalHeader.vue').default,
      'modal-value-date': require('components/SharedModals/ModalValueDate.vue').default,
      'modal-value': require('components/SharedModals/ModalValue.vue').default,
      'modal-buttons': require('components/SharedModals/ModalButtons.vue').default,
    },

    mounted () {      
      this.loanValueToSubmit.date = moment().format(this.getDateFormat)
    }         
  }
</script>

<style>

</style>

