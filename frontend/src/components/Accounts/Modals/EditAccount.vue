<template>
  <q-card>
    
    <modal-header>Edit Account</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-account-name 
          :accountName.sync="accountToSubmit.accountName" 
          ref="modalAccountName"/>  

        <modal-account-description 
          :description.sync="accountToSubmit.description" 
          ref="modalAccountDescription"/>    

        <modal-account-type 
          :type.sync="accountToSubmit.type" 
          ref="modalAccountType"/>   

        <modal-account-institution 
          :institution.sync="accountToSubmit.institution" 
          ref="modalAccountInstitution"/>  

        <modal-account-currency 
          :currencyCode.sync="accountToSubmit.currencyCode" 
          ref="modalAccountCurrency"/>   

        <modal-account-isActive
          :isActive.sync="accountToSubmit.isActive" 
          ref="modalAccountIsActive"/>  

      </q-card-section>

      <modal-buttons />

    </form>
    
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'

  export default {
    props: ['account', 'accountId'],
    data() {
      return {
        accountToSubmit: {
          bankAccountId: 0,
          accountName: '',          
          description: '',
          type: '',
          institution: '',
          currencyCode: '',
          isActive: false
        }
      }
    },
    methods: {
      ...mapActions('main', ['updateBankAccount']),
      submitForm() {
        this.$refs.modalAccountName.$refs.accountName.validate()
        if (!this.$refs.modalAccountName.$refs.accountName.hasError) {
          this.submitAccount()
        }
      },
      submitAccount() {
        console.log('in submitAccount, this.accountToSubmit:')
        console.log(this.accountToSubmit)
        this.updateBankAccount(this.accountToSubmit)
        this.$emit('close')
      }
    },
    components: {
      'modal-header': require('components/Accounts/Modals/Shared/ModalHeader.vue').default,
      'modal-account-name': require('components/Accounts/Modals/Shared/ModalAccountName.vue').default,
      'modal-account-description': require('components/Accounts/Modals/Shared/ModalAccountDescription.vue').default,
      'modal-account-type': require('components/Accounts/Modals/Shared/ModalAccountType.vue').default,
      'modal-account-institution': require('components/Accounts/Modals/Shared/ModalAccountInstitution.vue').default,
      'modal-account-currency': require('components/Accounts/Modals/Shared/ModalAccountCurrency.vue').default,
      'modal-account-isActive': require('components/Accounts/Modals/Shared/ModalAccountIsActive.vue').default,
      'modal-buttons': require('components/Accounts/Modals/Shared/ModalButtons.vue').default,
    },
    mounted() {
      // this.accountToSubmit = Object.assign({}, this.account) 
      this.accountToSubmit.bankAccountId = this.account.bankAccountId
      this.accountToSubmit.accountName = this.account.accountName 
      this.accountToSubmit.description = this.account.description
      this.accountToSubmit.type = this.account.type
      this.accountToSubmit.institution = this.account.institution
      this.accountToSubmit.currencyCode = this.account.quotedCurrency.code
      this.accountToSubmit.isActive = this.account.isActive
    } 
  }
</script>

<style>

</style>

