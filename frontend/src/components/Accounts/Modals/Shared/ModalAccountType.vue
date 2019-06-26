<template>
  <div class="row q-mb-sm">        
    <q-select 
      outlined 
      :options="options"
      :value="type" 
      @input="$emit('update:type', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="type"
      label="Account type" 
      class="col" 
    />
  </div>    
</template>

<script>
  export default {
    props: ['type'],
    data () {
      return {
        model: null,
        options: [
          'Current', 'Savings', 'Investment', 'Other'
        ]
      }
    }
  }
</script>

