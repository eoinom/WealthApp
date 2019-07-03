<template>
  <div class="row q-mb-sm">        
    <q-input 
      autofocus
      outlined 
      clearable
      autogrow
      type="text"
      :value="accountName" 
      ref="accountName" 
      class="col"
      label="Account name"
      @input="$emit('update:accountName', $event)"
      :rules="[val => !!val || 'Field is required']" 
    />
  </div>    
</template>

<script>
  export default {
    props: ['accountName']
  }
</script>

