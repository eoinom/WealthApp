<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined 
      :value="description" 
      @input="$emit('update:description', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="description"
      label="Account description" 
      class="col"
      clearable
      autogrow />
  </div>    
</template>

<script>
  export default {
      props: ['description']
  }
</script>

