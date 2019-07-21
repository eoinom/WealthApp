<template>
  <div class="row q-mb-sm">        
    <q-select 
      outlined 
      :options="currencyCodes"
      :value="currencyCode" 
      @input="$emit('update:currencyCode', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="currencyCode"
      label="Currency" 
      class="col" 
      :filled = "isFilled"
    />
  </div>    
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    props: ['currencyCode', 'isFilled'],
    data () {
      return {
        model: null
      }
    },
    computed: {           
      ...mapGetters('main', ['currencyCodes'])
    }
  }
</script>

