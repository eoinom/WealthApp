<template>
  <q-card>
    
    <modal-header>Add Account Value</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-account-value-date 
          :name.sync="accountValueToSubmit.date" 
          ref="modalAccountValueDate"
          />  

        <modal-account-value 
          :name.sync="accountValueToSubmit.value" 
          ref="modalAccountValue"/>    

      </q-card-section>

      <modal-buttons />

    </form>
    
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'

  export default {
    data() {
      return {
        accountValueToSubmit: {
          date: '',
          value: '',
        }
      }
    },
    methods: {
      ...mapActions('main', ['addAccountValue']),
      submitForm() {
        this.$refs.modalAccountName.$refs.name.validate()
        if (!this.$refs.modalAccountName.$refs.name.hasError) {
          this.submitAccount()
        }
      },
      submitAccount() {
        this.addAccount(this.accountToSubmit)
        this.$emit('close')
      }
    },
    components: {
      'modal-header': require('components/Accounts/Modals/Shared/ModalHeader.vue').default,
      'modal-account-value-date': require('components/Accounts/Modals/Shared/ModalAccountValueDate.vue').default,
      'modal-account-value': require('components/Accounts/Modals/Shared/ModalAccountValue.vue').default,
      'modal-buttons': require('components/Accounts/Modals/Shared/ModalButtons.vue').default,
    }      
  }
</script>

<style>

</style>

