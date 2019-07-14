<template>
  <div class="row q-mb-sm">        
    <q-input 
      outlined
      type="number"        
      :value="intValue" 
      :label="label"
      @input="$emit('update:intValue', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="intValue"
      class="col"
      clearable />
  </div>    
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    props: ['intValue', 'label'],
  }  
</script>

