<template>
  <div class="row q-mb-sm">        
    <!-- <q-input 
      outlined 
      :value="active" 
      @input="$emit('update:active', $event)"
      :rules="[val => !!val || 'Field is required']"
      autofocus
      ref="active"
      label="Account institution" 
      class="col"
      clearable /> -->

    <q-checkbox
      v-model="isActive"
      @input="$emit('update:active', $event)"
      color="primary"
      label="Account active?"
      class="col"
    />
  </div>    
</template>

<script>
  export default {
    props: ['isActive']
  }
</script>

