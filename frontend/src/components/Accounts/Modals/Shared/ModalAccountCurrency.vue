<template>
  <div class="row q-mb-sm">        
    <q-select 
      outlined 
      v-model="model"
      :options="options"
      :value="currency" 
      @input="$emit('update:currency', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="currency"
      label="Account currency" 
      class="col"
      clearable />
  </div>    
</template>

<script>
  export default {
      props: ['currency'],
      data () {
      return {
        model: null,
        options: [
          'AUD',
          'CAD',
          'EUR', 
          'GBP', 
          'USD',
          'NZD'
        ]
      }
    }
  }
</script>

