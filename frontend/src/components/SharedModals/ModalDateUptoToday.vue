<template>
  <div class="row q-mb-lg">  
    <q-input 
      outlined 
      clearable
      :value="date"
      :label="label"
      @input="$emit('update:date', $event)"
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
              :value="date" 
              :mask="getDateFormat"
              today-btn
              :options="date => dateOptionsFn(date)"
              @input="$refs.qDateProxy.hide(); $emit('update:date', $event)" />
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
    props: ['date', 'label'],
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

