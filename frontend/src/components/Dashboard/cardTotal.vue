<template>
  <q-card               
    class="total-card text-black"
    :style="cardStyle" 
    >
    <q-card-section>  
      <!-- <div class="row text-h6">{{ title }}</div> -->
      <div class="row text-subtitle1" style="font-weight: bold">{{ title }}</div>
    </q-card-section>

    <q-card-section>
      <div class="row"> 
        <div class="col-9">
          <span class="countup">
            <ICountUp
              :startVal="startVal"
              :endVal="endVal"
              :duration="duration"
              :options="options"
              @ready="onReady"
              />
          </span>
        </div>
        
        <div class="col icon">
          <q-icon 
            :name="iconName" 
            style="font-size: 3em;"
            color="grey-3"/>
        </div>
      </div>
    </q-card-section>
    
    <q-tooltip anchor="top right" self="top middle" :offset="[10, 10]" 
      content-class="bg-deep-orange" content-style="font-size: 14px;" v-if="showTooltipText">
      <span v-html="tooltipText"></span>
    </q-tooltip>

  </q-card>
</template>

<script>
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'
  import ICountUp from 'vue-countup-v2';

  export default {
    name: 'CardTotal',
    props: ['title', 'currencySymbol', 'total', 'decimalPlaces', 'cardStyle', 'iconName', 'breakdown'],

    data() {
      return {      
        startVal: 0,
        endVal: this.total,
        duration: 2.5,
        options: {
            useEasing: true,                  // ease animation 
            useGrouping: true,                // example: 1,000 vs 1000
            separator: ',',                   // grouping separator
            decimal: '.',
            prefix: this.currencySymbol,
            suffix: '',
            decimalPlaces: this.decimalPlaces,
        },
        showTooltipText: false
      }      
    },

    computed: {
      tooltipText () {
        var text = ''
        console.log('this.breakdown.length: ' + this.breakdown.length);
        
        if (this.breakdown.length > 0) {
          for (var i = 0; i < this.breakdown.length; i++) {
            var el = this.breakdown[i]
            if (i > 0) {
              text += '<br>'
            }
            text += el.name + ' = ' + el.symbol + this.toLocaleFixed(el.value, 2)
          }
          this.showTooltipText = true
          return text
        }
        this.showTooltipText = false
        return text
      }
    },

    methods: {
      onReady: function(instance, CountUp) {
        const that = this;
        instance.update(that.endVal + 100);
      }
    },

    mounted () {
      if (this.breakdown != undefined && this.breakdown.length > 0) {        
        this.showTooltipText = true
      }
    },

    components: {
      ICountUp
    }
  }
</script>

<style>
  .icon {
    text-align: right;
  }

  .icon img {
    max-width: 150px;
    max-height: 70px;
    height: auto;
  }

  .countup {
    font-size: 3em;
    margin: 0;
    color: #d3d5e2;
  }
</style>