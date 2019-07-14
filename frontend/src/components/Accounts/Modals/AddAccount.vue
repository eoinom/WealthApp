<template>
  <q-card>
    
    <modal-header>Add Account</modal-header>

    <form @submit.prevent="submitForm">
      <q-card-section>

        <modal-name 
          :name.sync="accountToSubmit.accountName" 
          label="Account Name"
          ref="modalAccountName"/>  

        <modal-description 
          :description.sync="accountToSubmit.description" 
          label="Description"
          ref="modalAccountDescription"/>    

        <modal-select 
          :selectValue.sync="accountToSubmit.type" 
          :selectArr="accountTypes"
          label="Type"
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
    data() {
      return {
        accountToSubmit: {
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
      ...mapActions('accounts', ['addAccount']),      
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
      'modal-active':       require('components/SharedModals/ModalActive.vue').default,
      'modal-buttons':      require('components/SharedModals/ModalButtons.vue').default,
      'modal-currency':     require('components/SharedModals/ModalCurrencySelect.vue').default,
      'modal-description':  require('components/SharedModals/ModalDescription.vue').default,
      'modal-header':       require('components/SharedModals/ModalHeader.vue').default,
      'modal-institution':  require('components/SharedModals/ModalInstitution.vue').default,
      'modal-name':         require('components/SharedModals/ModalName.vue').default,
      'modal-select':       require('components/SharedModals/ModalSelect.vue').default      
    }      
  }
</script>

<style>

</style>

