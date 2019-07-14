<template>
  <div class="row q-mb-sm">        
    <q-select 
      outlined 
      :options="selectArr"
      :value="selectValue" 
      :label="label" 
      @input="$emit('update:selectValue', $event)"
      :rules="[val => !!val || 'Field is required']"
      ref="selectValue"      
      class="col" 
    />
  </div>    
</template>

<script>
  export default {
    props: ['selectValue', 'selectArr', 'label'],
    data () {
      return {
        model: null
      }
    }
  }
</script>

