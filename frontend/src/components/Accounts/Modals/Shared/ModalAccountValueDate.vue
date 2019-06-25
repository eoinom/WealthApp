<template>
  <div class="row q-mb-sm">  
    <q-input 
      outlined 
      label="Date"
      autofocus
      :value="valueDate"
      @input="$emit('update:valueDate', $event)"
      class="col" >
      <template v-slot:append> 
        <q-icon 
          name="cancel" 
          v-if="valueDate"
          @click="$emit('clear')" 
          class="cursor-pointer" />               

        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy ref="qDateProxy" transition-show="scale" transition-hide="scale">
            <q-date 
              :value="valueDate" 
              @input="$refs.qDateProxy.hide(); $emit('update:valueDate', $event)" />
          </q-popup-proxy>
        </q-icon>              
      </template>
    </q-input>
  </div>
</template>

<script>
  export default {
    props: ['valueDate'],
    methods: {    
      
    }
  }
</script>

