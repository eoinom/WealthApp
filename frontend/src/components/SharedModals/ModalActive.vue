<template>
  <div class="row q-mb-sm">  
    <q-checkbox
      :value="isActive"
      @input="$emit('update:isActive', $event)"
      color="primary"
      :label="label"
      class="col"
    />
  </div>    
</template>

<script>
  export default {
    props: ['isActive', 'label']
  }
</script>

