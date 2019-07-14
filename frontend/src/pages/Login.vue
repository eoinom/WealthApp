<template>
  <div class="q-pa-md authForm">
    <div class="q-gutter-y-md" style="max-width: 600px">
      <img alt="WealthApp Logo" src="~assets/Logo.png" id="logo-auth">
      <q-card>
          
        <q-tabs
          v-model="tab"
          dense
          class="bg-grey-3 text-black"
          active-color="primary"
          indicator-color="primary"
          align="justify"
          narrow-indicator
        >
          <q-tab name="login" label="Login" />
          <q-tab name="register" label="Register" />
        </q-tabs>

        <q-separator />

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="login">
            <div class="text-h6 form-title">Login</div>
            
            <form @submit.prevent.stop="onSubmitLogin" @reset.prevent.stop="onResetLogin">                                
              <q-input
                ref="email"
                filled
                v-model="email"
                type="email" 
                label="Email *"
                lazy-rules
                :rules="[ val => val && val.length > 0 || 'Please enter a valid email address']"
              >
                <template v-slot:prepend>
                  <q-icon name="mail" />
                </template>
              </q-input>

              <q-input 
                v-model="password" 
                label="Password *"
                filled 
                :type="isPwd ? 'password' : 'text'" 
              >
                <template v-slot:append>
                  <q-icon
                    :name="isPwd ? 'visibility_off' : 'visibility'"
                    class="cursor-pointer"
                    @click="isPwd = !isPwd"
                  />
                </template>
              </q-input>
              
              <div>
                <q-btn label="Login" type="submit" color="primary" />
                <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
              </div>
            </form>
          </q-tab-panel>

          <q-tab-panel name="register">
              <div class="text-h6 form-title">Sign up for free</div>
              
              <form @submit.prevent.stop="onSubmitRegister" @reset.prevent.stop="onResetRegister">
                <q-input
                  ref="firstName"
                  filled
                  v-model="firstName"
                  label="First name *"
                  lazy-rules
                  :rules="[ val => val && val.length > 0 || 'Please type something']"
                />

                <q-input
                  ref="surname"
                  filled
                  v-model="surname"
                  label="Surname *"
                  lazy-rules
                  :rules="[ val => val && val.length > 0 || 'Please type something']"
                />

                <q-input
                  ref="email"
                  filled
                  v-model="email"
                  type="email" 
                  label="Email *"
                  lazy-rules
                  :rules="[ val => val && val.length > 0 || 'Please enter a valid email address']"
                >
                  <template v-slot:prepend>
                    <q-icon name="mail" />
                  </template>
                </q-input>

                <q-input 
                  v-model="password" 
                  label="Choose Password *"
                  filled 
                  :type="isPwd ? 'password' : 'text'" 
                  lazy-rules
                  :rules="[ val => val && val.length > 6 || 'Please enter a password at least 7 characters long']"
                >
                  <template v-slot:append>
                    <q-icon
                      :name="isPwd ? 'visibility_off' : 'visibility'"
                      class="cursor-pointer"
                      @click="isPwd = !isPwd"
                    />
                  </template>
                </q-input>

                <q-input 
                  v-model="confirmPassword" 
                  label="Confirm Password *"
                  filled 
                  :type="isPwd ? 'password' : 'text'" 
                  lazy-rules
                  :rules="[ val => val && val.length > 6 || 'Please enter a password at least 7 characters long']"
                >
                  <template v-slot:append>
                    <q-icon
                      :name="isPwd ? 'visibility_off' : 'visibility'"
                      class="cursor-pointer"
                      @click="isPwd = !isPwd"
                    />
                  </template>
                </q-input>
                
                <q-checkbox
                  v-model="subscribed"
                  color="secondary"
                  label="Do you wish to subscribe for updates?"
                />

                <q-checkbox
                  v-model="agreeTerms"
                  color="secondary"
                  label="Do you agree with the terms & conditions?"
                />

                <div>
                  <q-btn label="Register" type="register" color="primary" />
                  <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
                </div>
              </form>        
          </q-tab-panel>
        </q-tab-panels>
      </q-card>        
    </div>
  </div>
</template>

<script type="text/javascript">
  import AppVue from '../App.vue';
  import router from '../router';
  import { mapActions } from 'vuex'
  import { mapGetters } from 'vuex'    

  export default {
    data () {
      return {
        email: '',
        password: '',
        confirmPassword: '',
        isPwd: true,
        firstName: '',
        surname: '',
        subscribed: true,
        agreeTerms: false,
        tab: 'login',
        // authenticated: false
      }
    },
    computed: {
    },
    methods: {      
      ...mapActions('main', ['login', 'updateUser']),
      ...mapActions('accounts', ['initialiseAccounts']),
      ...mapActions('loans', ['initialiseLoans']),
      ...mapGetters('main', ['authenticated', 'user']),

      onSubmitLogin () { 
        this.$q.loading.show({
          delay: 400, // ms
          message: 'Loading accounts<br/><span class="text-white">Hang on...</span>'
        })
        this.login(this.email, this.password).then(authorised => {          
          if (authorised) {
            console.log("user:");
            console.log(this.user());
            this.$q.notify({
              color: 'green-4',
              textColor: 'white',
              icon: 'fas fa-check-circle',
              message: 'Welcome back ' + this.user().firstName + '!'
            });

            // hiding for 2s
            this.timer = setTimeout(() => {
              this.$q.loading.hide()
              this.timer = void 0
              this.$router.push('/accounts')
            }, 2000)

            // this.$router.push('/accounts')                                        
          }
          else {
            this.$q.notify({
              color: 'red-7',
              textColor: 'white',
              icon: 'fas fa-exclamation-triangle',
              message: 'Invalid credentials, please try again.'
            })
            this.$q.loading.hide()
            this.$router.push('/login')
          }
        });        
      },

      sleep (milliseconds) {
        var start = new Date().getTime();
        for (var i = 0; i < 1e7; i++) {
          if ((new Date().getTime() - start) > milliseconds){
            break;
          }
        }
      },

      onResetLogin () {
        this.email = null
        this.password = null
        this.confirmPassword = false
      },

      onSubmitRegister () {
        if (this.agreeTerms !== true) {
          this.$q.notify({
          color: 'red-5',
          textColor: 'white',
          icon: 'fas fa-exclamation-triangle',
          message: 'You need to accept the terms and conditions first'
          })
        }
        else {
          this.$q.notify({
          color: 'green-4',
          textColor: 'white',
          icon: 'fas fa-check-circle',
          message: 'Submitted'
          })
        }
      },

      onResetRegister () {
        this.firstName = null
        this.surname = null
        this.email = null
        this.password = null
        this.confirmPassword = null
        this.subscribed = true
        this.agreeTerms = false
      },


      async login(email, password) {                
        const axios = require("axios")
        try {
          var response = await axios({
            method: "POST",
            url: "/",
            data: {
              query: `                    
              {
                user_queries {
                  userLogin(email: "` + email + `", password:"` + password + `") {
                    userId
                    firstName
                    lastName
                    email
                    newsletterSub
                    country {
                      iso2Code
                      name
                    }
                    displayCurrency {
                      code
                      nameShort
                      nameLong 
                    }
                    accounts {
                      accountId
                      accountName
                      description      
                      type
                      isActive
                      institution
                      quotedCurrency {
                        code
                        nameShort
                        nameLong        
                      }
                      accountValues {
                        accountValueId
                        date
                        value
                        rateToUserCurrency
                        valueUserCurrency
                        account {
                          accountId
                        }
                      }
                    }
                    loans {
                      loanId
                      loanName
                      description      
                      type
                      startPrincipal
                      startDate
                      totalTerm
                      fixedTerm
                      rateType
                      aprRate
                      repaymentFrequency
                      repaymentAmount
                      isActive
                      institution
                      quotedCurrency {
                        code
                        nameShort
                        nameLong        
                      }
                      loanValues {
                        loanValueId
                        date
                        value
                        rateToUserCurrency
                        valueUserCurrency
                        loan {
                          loanId
                        }
                      }
                    }
                  }
                }  
              }
              `
            }
          });            
          
          if (response.data.data.user_queries.userLogin != null) {
            
            this.updateUser(response.data.data.user_queries.userLogin);
            this.initialiseAccounts(response.data.data.user_queries.userLogin.accounts);
            this.initialiseLoans(response.data.data.user_queries.userLogin.loans);

            if (this.user() != null) {
              return true
            }
          }          
          console.log("Login failed")
          return false
            
        } catch (error) {
            console.error(error); 
        }
        return false
      },

      beforeDestroy () {
        if (this.timer !== void 0) {
          clearTimeout(this.timer)
          this.$q.loading.hide()
        }
      }
    }
  }
</script>

<style lang="scss">
  form button {
    margin-bottom: 4%;
    margin-top: 4%;
  }
  .authForm {
    background:rgba(#027BE3,.9);
    padding: 40px;
    max-width:600px;
    margin:40px auto;
    border-radius: 6px;
    box-shadow:0 4px 10px 4px rgba(#027BE3,.3);
  }
  .form-title {
      padding-bottom: 6px;
  }
  #logo-auth {
      height:30px; 
      display:block; 
      margin:auto
  }
</style>