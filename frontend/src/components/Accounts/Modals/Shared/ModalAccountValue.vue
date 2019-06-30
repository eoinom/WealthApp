<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined
      type="number"             
      :decimals="2"
      step="0.01"
      :value="accountValue" 
      :prefix="currencySymbol"
      @input="$emit('update:accountValue', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="accountValue"
      label="Value" 
      class="col"
      clearable />
  </div>    
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    props: ['accountValue'],
    data () {
      return {
        accountVal: this.accountValue,
        currencySymbol: '€'
      }
    },
    computed: {
      ...mapGetters('accounts', ['selectedAccountCurrencySymbol']),
    },
    mounted() {
      this.currencySymbol = this.selectedAccountCurrencySymbol
    },
  }

  
</script>

