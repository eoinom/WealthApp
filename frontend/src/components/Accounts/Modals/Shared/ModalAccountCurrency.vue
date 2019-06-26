<template>
  <div class="row q-mb-sm">        
    <q-select 
      outlined 
      v-model="model"
      :options="options"
      :value="currencyCode" 
      @input="$emit('update:currencyCode', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="currencyCode"
      label="Currency" 
      class="col"
      clearable />
  </div>    
</template>

<script>
  export default {
      props: ['currencyCode'],
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

