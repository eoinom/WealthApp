<template>
  <div class="row q-mb-sm">        
    <q-input 
      autofocus
      outlined 
      clearable
      autogrow
      type="text"
      :value="name" 
      ref="name" 
      class="col"
      :label="label"
      @input="$emit('update:name', $event)"
      :rules="[val => !!val || 'Field is required']" 
    />
  </div>    
</template>

<script>
  export default {
    props: ['name', 'label'],
  }
</script>

