<template>
  <q-card>
    
    <modal-header>Edit Account</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-name 
          :name.sync="accountToSubmit.accountName" 
          label="Account name"
          ref="modalAccountName"/>  

        <modal-description 
          :description.sync="accountToSubmit.description" 
          label="Account description"
          ref="modalAccountDescription"/>    

        <modal-type 
          :type.sync="accountToSubmit.type" 
          label="Account type"
          :options="accountTypes"
          ref="modalAccountType"/>   

        <modal-institution 
          :institution.sync="accountToSubmit.institution" 
          label="Institution"
          ref="modalAccountInstitution"/>  

        <modal-currency 
          :currencyCode.sync="accountToSubmit.currencyCode" 
          ref="modalAccountCurrency"/>   

        <modal-active
          :isActive.sync="accountToSubmit.isActive" 
          label="Account active?" 
          ref="modalAccountIsActive"/>  

      </q-card-section>

      <modal-buttons />

    </form>
    
  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'

  export default {
    props: ['account', 'accountId'],

    data() {
      return {
        accountToSubmit: {
          accountId: 0,
          accountName: '',          
          description: '',
          type: '',
          institution: '',
          currencyCode: '',
          isActive: false
        }
      }
    },

    computed: {
      ...mapGetters('accounts', ['accountTypes'])
    },

    methods: {
      ...mapActions('accounts', ['updateAccount']),
      submitForm() {
        this.$refs.modalAccountName.$refs.name.validate()
        if (!this.$refs.modalAccountName.$refs.name.hasError) {
          this.submitAccount()
        }
      },
      submitAccount() {
        console.log('in submitAccount, this.accountToSubmit:')
        console.log(this.accountToSubmit)
        this.updateAccount(this.accountToSubmit)
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
    },

    mounted() {
      // this.accountToSubmit = Object.assign({}, this.account) 
      this.accountToSubmit.accountId = this.account.accountId
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

