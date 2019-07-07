<template>
  <div class="row q-mb-sm">        
    <q-select 
      outlined 
      :options="options"
      :value="type" 
      @input="$emit('update:type', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="type"
      :label="label" 
      class="col" 
    />
  </div>    
</template>

<script>
  export default {
    props: ['type', 'label', 'options'],
    data () {
      return {
        model: null
      }
    }
  }
</script>

