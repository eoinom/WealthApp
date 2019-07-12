<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined
      type="number"             
      :decimals="2"
      step="0.01"
      :value="value" 
      :prefix="currencySymbol"
      @input="$emit('update:value', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="value"
      label="Value" 
      class="col"
      clearable />
  </div>    
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    props: ['value', 'currencySymbol'],
  }  
</script>

