<template>
  <div class="row q-mb-sm">        
    <q-input 
      autofocus
      outlined 
      clearable
      autogrow
      :value="name" 
      ref="name" 
      class="col"
      label="Account name"
      @input="$emit('update:name', $event)"
      :rules="[val => !!val || 'Field is required']" 
    />
  </div>    
</template>

<script>
  export default {
      props: ['name']
  }
</script>

