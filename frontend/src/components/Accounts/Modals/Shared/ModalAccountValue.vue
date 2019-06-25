<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined
      type="number"  
      :value="accountValue" 
      @input="$emit('update:accountValue', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="accountValue"
      label="Value" 
      class="col"
      clearable
      autogrow />
  </div>    
</template>

<script>
  export default {
      props: ['accountValue']
  }
</script>

