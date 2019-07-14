<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined
      type="number"  
      :decimals="decimals"   
      :step="step"          
      :value="decimalValue" 
      :label="label"
      :suffix="suffix"
      @input="$emit('update:decimalValue', $event)"
      :rules="[val => !!val || 'Field is required', val => val >= 0 || 'Can not be negative']"
      ref="decimalValue"
      class="col"
      clearable />
  </div>    
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    props: ['decimalValue', 'decimals', 'step', 'label', 'suffix'],
  }  
</script>

