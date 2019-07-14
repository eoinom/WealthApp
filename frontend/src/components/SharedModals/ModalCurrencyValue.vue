<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined
      type="number"    
      step="0.01"         
      :decimals="2"      
      :value="currencyValue" 
      :prefix="currencySymbol"
      :label="label"
      @input="$emit('update:currencyValue', $event)"
      :rules="[val => !!val || 'Field is required', val => val >= 0 || 'Can not be negative']"
      ref="currencyValue"
      class="col"
      clearable
      :readonly="readonly" />
  </div>    
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    props: ['currencyValue', 'currencySymbol', 'label', 'readonly'],
    data: {
      readonly: false
    }
  }  
</script>

