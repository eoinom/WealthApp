<template>
  <div class="row q-mb-sm">  
    <q-input 
      outlined 
      label="Date"
      autofocus
      clearable
      :value="valueDate"
      @input="$emit('update:valueDate', $event)"
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
              :value="valueDate" 
              :mask="getDateFormat"
              today-btn
              :options="date => dateOptionsFn(date)"
              @input="$refs.qDateProxy.hide(); $emit('update:valueDate', $event)" />
          </q-popup-proxy>
        </q-icon>              
      </template>
    </q-input>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  var moment = require('moment');

  export default {    
    props: ['valueDate'],
    computed: {   
      ...mapGetters('main', ['getDateFormat'])      
    },
    methods: {
      dateOptionsFn(date) {
        return moment(date, "YYYY/MM/DD") <= moment()
      }
    }
  }
</script>

