<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined 
      :value="institution" 
      @input="$emit('update:institution', $event)"
      :rules="[val => !!val || 'Field is required']"
      autofocus
      ref="institution"
      label="Account institution" 
      class="col"
      clearable 
      autogrow />
  </div>    
</template>

<script>
  export default {
      props: ['institution']
  }
</script>

