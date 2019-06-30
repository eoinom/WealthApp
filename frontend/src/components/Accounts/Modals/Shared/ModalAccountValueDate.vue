<template>
  <div class="row q-mb-sm">  
    <q-input 
      outlined 
      label="Date"
      autofocus
      clearable
      :value="accountValueDate"
      @input="$emit('update:accountValueDate', $event)"
      class="col" >
      <template v-slot:append> 
        <!-- <q-icon 
          name="cancel" 
          v-if="accountValueDate"
          @click="$emit('clear')" 
          class="cursor-pointer" />                -->

        <q-icon name="event" class="cursor-pointer">
          <q-popup-proxy ref="qDateProxy" transition-show="scale" transition-hide="scale">
            <q-date 
              :value="accountValueDate" 
              :mask="getDateFormat"
              @input="$refs.qDateProxy.hide(); $emit('update:accountValueDate', $event)" />
          </q-popup-proxy>
        </q-icon>              
      </template>
    </q-input>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {    
    props: ['accountValueDate'],
    computed: {   
      ...mapGetters('main', ['getDateFormat']),
      
    }
  }
</script>

