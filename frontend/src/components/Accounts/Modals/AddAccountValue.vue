<template>
  <q-card>
    
    <modal-header>Add Account Value</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-value-date 
          :valueDate.sync="accountValueToSubmit.date"           
          ref="modalAccountValueDate"
          />  

        <modal-currency-value 
          :currencyValue.sync="accountValueToSubmit.value" 
          :currencySymbol="selectedAccountCurrencySymbol"
          label="Value"
          ref="modalAccountValue"/>    

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
    props: ['accountId'],
    data() {
      return {
        accountValueToSubmit: {
          date: '',
          value: '',
          accountId: this.accountId,
        }
      }
    },

    computed: {
      ...mapGetters('accounts', ['selectedAccountCurrencySymbol']),
      ...mapGetters('main', ['getDateFormat'])
    },

    methods: {
      ...mapActions('accounts', ['addAccountValue']),
      
      submitForm() {
        // this.$refs.modalAccountValueDate.$refs.accountValueDate.validate()
        // Need to do date validation
        this.$refs.modalAccountValue.$refs.currencyValue.validate()
        if (!this.$refs.modalAccountValue.$refs.currencyValue.hasError) {
          this.submitAccountValue()
        }
      },
      submitAccountValue() {
        console.log('this.accountValueToSubmit:')
        console.log(this.accountValueToSubmit)
        
        this.accountValueToSubmit.date = this.formatDate_User2Iso(this.accountValueToSubmit.date)
        console.log('this.accountValueToSubmit:')
        console.log(this.accountValueToSubmit)
        
        this.addAccountValue(this.accountValueToSubmit)
        this.$emit('close')
      }
    },

    components: {
      'modal-header': require('components/SharedModals/ModalHeader.vue').default,
      'modal-value-date': require('components/SharedModals/ModalValueDate.vue').default,
      'modal-currency-value': require('components/SharedModals/ModalCurrencyValue.vue').default,
      'modal-buttons': require('components/SharedModals/ModalButtons.vue').default,
    },

    mounted () {      
      this.accountValueToSubmit.date = moment().format(this.getDateFormat)
    }      
  }
</script>

<style>

</style>

