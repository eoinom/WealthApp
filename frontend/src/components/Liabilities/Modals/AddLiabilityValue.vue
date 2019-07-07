<template>
  <q-card>
    
    <modal-header>Add Liability Value</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-value-date 
          :valueDate.sync="accountValueToSubmit.date"           
          ref="modalAccountValueDate"
          />  

        <modal-value 
          :value.sync="accountValueToSubmit.value" 
          :currencySymbol="selectedAccountCurrencySymbol"
          ref="modalAccountValue"/>    

      </q-card-section>

      <modal-buttons />

    </form>
    
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'
import { constants } from 'crypto';

  export default {
    props: ['accountId'],
    data() {
      return {
        accountValueToSubmit: {
          date: this.getTodaysDate(),
          value: '',
          accountId: this.accountId,
        }
      }
    },
    computed: {
      ...mapGetters('accounts', ['selectedAccountCurrencySymbol'])
    },
    methods: {
      ...mapActions('main', ['addBankAccountValue']),
      ...mapGetters('main', ['getDateFormat']),
      submitForm() {
        // this.$refs.modalAccountValueDate.$refs.accountValueDate.validate()
        // Need to install Moment.js and do date validation
        this.$refs.modalAccountValue.$refs.accountValue.validate()
        if (!this.$refs.modalAccountValue.$refs.accountValue.hasError) {
          this.submitAccountValue()
        }
      },
      submitAccountValue() {
        this.accountValueToSubmit.date = this.convertDateToIso(this.accountValueToSubmit.date)
        console.log(this.accountValueToSubmit)
        this.addBankAccountValue(this.accountValueToSubmit)
        this.$emit('close')
      },
      getTodaysDate() {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth()+1;  // As January is 0
        var yyyy = today.getFullYear();

        if (dd < 10) 
          dd = '0' + dd;
        if (mm < 10) 
          mm = '0' + mm;
    
        switch(this.getDateFormat()) {
          case "DD-MM-YYYY":
            return (dd + '-' + mm + '-' + yyyy);
            break;
          case "DD/MM/YYYY":
            return (dd + '/' + mm + '/' + yyyy);
            break;
          case "MM-DD-YYYY":
            return (mm + '-' + dd + '-' + yyyy);
            break;
          case "MM/DD/YYYY":
            return (mm + '/' + dd + '/' + yyyy);
            break;
          case "YYYY-MM-DD":
            return (yyyy + '-' + mm + '-' + dd);
            break;
          case "YYYY/MM/DD":
            return (yyyy + '/' + mm + '/' + dd);
            break;
          default:
            return (yyyy + '-' + mm + '-' + dd);
        }
      },
      convertDateToIso(strDate) {        
        var date = new Date();
        var dd = '';
        var mm = '';
        var yyyy = '';
        
        switch(this.getDateFormat()) {
          case "YYYY-MM-DD":
            return strDate;
          case "DD-MM-YYYY":
          case "DD/MM/YYYY":
            dd = strDate.slice(0,2)
            mm = strDate.slice(3,5)
            yyyy = strDate.slice(6,10)
            return yyyy + '-' + mm + '-' + dd;
          case "MM-DD-YYYY":
          case "MM/DD/YYYY":
            mm = strDate.slice(0,2)
            dd = strDate.slice(3,5)
            yyyy = strDate.slice(6,10)
            return yyyy + '-' + mm + '-' + dd;
          case "YYYY/MM/DD":
            yyyy = strDate.slice(0,4)
            mm = strDate.slice(5,7)
            dd = strDate.slice(8,10)
            return yyyy + '-' + mm + '-' + dd;
          default:
            console.log('Could not convert date string ' + strDate + ' to Iso')
            return strDate
        }
      }
    },
    components: {
      'modal-header': require('components/SharedModals/ModalHeader.vue').default,
      'modal-value-date': require('components/SharedModals/ModalValueDate.vue').default,
      'modal-value': require('components/SharedModals/ModalValue.vue').default,
      'modal-buttons': require('components/SharedModals/ModalButtons.vue').default,
    }      
  }
</script>

<style>

</style>

