<template>
  <div class="row q-mb-sm">  
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

