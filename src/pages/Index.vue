<template>
  <q-page class="flex flex-center">

    <div class="col-12 justify-center">
      <div class="row">
        <img alt="Quasar logo" src="~assets/quasar-logo-full.svg">
      </div>
      <p></p>
      <div class="row justify-center">
        {{ textFromApi }}
      </div>    
    </div>

  </q-page>
</template>

<style>
</style>

<script>
export default {
  name: 'PageIndex',
  data () {
    return {
      textFromApi: null
    }
  },

  async created () {
    var response = await this.$axios.get('/values')
    this.textFromApi = response.data.text
  }
}
</script>
