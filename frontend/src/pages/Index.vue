<template>
  <q-page class="flex flex-center">

    <div class="col-12 justify-center">
      <div class="row">
        <img alt="Quasar logo" src="~assets/quasar-logo-full.svg">
      </div>
      <p></p>
      <div class="row justify-center">
        {{ userEmail }}
      </div>    
    </div>

  </q-page>
</template>

<style>
</style>

<script>
export default {
  name: 'UserEmail',
  data () {
    return {
      userEmail: ''
    }
  },
  async created () {
    try {
      var response = await this.$axios({
        method: "POST",
        url: "/",
        data: {
          query: `
            {
              user_queries {
                user (id: 3) {
                  email
                }
              }
            }
          `
        }
      });
      this.userEmail = response.data.data.user_queries.user.email;
    } catch (error) {
      console.error(error); 
    }
  }
}
</script>
