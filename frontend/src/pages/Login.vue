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
                v-model="userForm.email"
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
                v-model="userForm.password" 
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
                  v-model="userForm.firstName"
                  label="First name *"
                  lazy-rules
                  :rules="[ val => val && val.length > 0 || 'Please type something']"
                  id="register-firstname"
                />

                <q-input
                  ref="lastName"
                  filled
                  v-model="userForm.lastName"
                  label="Surname *"
                  lazy-rules
                  :rules="[ val => val && val.length > 0 || 'Please type something']"
                  id="register-lastname"
                />

                <q-input
                  ref="email"
                  filled
                  v-model="userForm.email"
                  type="email" 
                  label="Email *"
                  lazy-rules
                  :rules="[ val => val && val.length > 0 || 'Please enter a valid email address']"
                  id="register-email"
                >
                  <template v-slot:prepend>
                    <q-icon name="mail" />
                  </template>
                </q-input>

                <q-input 
                  v-model="userForm.password" 
                  label="Choose Password *"
                  filled 
                  :type="isPwd ? 'password' : 'text'" 
                  lazy-rules
                  :rules="[ val => val && val.length > 6 || 'Please enter a password at least 7 characters long']"
                  id="register-password"
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
                  v-model="userForm.confirmPassword" 
                  label="Confirm Password *"
                  filled 
                  :type="isPwd ? 'password' : 'text'" 
                  lazy-rules
                  :rules="[ val => val && val.length > 6 || 'Please enter a password at least 7 characters long']"
                  id="register-confirmPassword"
                >
                  <template v-slot:append>
                    <q-icon
                      :name="isPwd ? 'visibility_off' : 'visibility'"
                      class="cursor-pointer"
                      @click="isPwd = !isPwd"
                    />
                  </template>
                </q-input>

                <modal-select                   
                  :selectValue.sync="userCountry"
                  :selectArr="countries"
                  :isFilled = true
                  label="Country"
                  ref="modalUserCountry"
                  id="register-country"
                /> 

                <modal-currency 
                  :currencyCode.sync = "userForm.displayCurrency"
                  :isFilled = true
                  ref="modalUserDisplayCurrency"
                  id="register-currency"
                />   
                
                <q-checkbox
                  v-model="userForm.newsletterSub"
                  color="secondary"
                  label="Do you wish to subscribe for updates?"
                  id="register-newsletter"
                />

                <q-checkbox
                  v-model="userForm.agreeTerms"
                  color="secondary"
                  label="Do you agree with the terms & conditions?"
                  id="register-agreeTerms"
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
        isPwd: true,
        tab: 'login',
        userCountry: '',
        userForm: {
          email: '',
          password: '',
          confirmPassword: '',          
          firstName: '',
          lastName: '',
          countryIso2Code: '',
          displayCurrency: '',
          newsletterSub: true,
          agreeTerms: false    
        }
      }
    },
    computed: {
      ...mapGetters('main', ['countries', 'countryCodes']),
    },
    methods: {      
      ...mapActions('main', ['updateUser']),
      ...mapActions('accounts', ['initialiseAccounts']),
      ...mapActions('loans', ['initialiseLoans']),
      ...mapGetters('main', ['user']),

      onSubmitLogin () { 
        this.$q.loading.show({
          delay: 400, // ms
          message: 'Logging you in<br/><span class="text-white">Hang on...</span>'
        })
        this.login(this.userForm.email, this.userForm.password).then(authorised => {          
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
              this.$router.push('/dashboard')
            }, 2000)

            // this.$router.push('/dashboard')                                        
          }
          else {
            this.$q.notify({
              color: 'red-7',
              textColor: 'white',
              icon: 'fas fa-exclamation-triangle',
              message: 'Something went wrong, please check your login details and try again.'
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
        this.userForm.email = null
        this.userForm.password = null
        this.userForm.confirmPassword = false
      },

      onSubmitRegister () { 
        if (this.userForm.password !== this.userForm.confirmPassword) {
          this.$q.notify({
          color: 'red-5',
          textColor: 'white',
          icon: 'fas fa-exclamation-triangle',
          message: 'Passwords do not match'
          })
        }
        else if (this.userForm.agreeTerms !== true) {
          this.$q.notify({
          color: 'red-5',
          textColor: 'white',
          icon: 'fas fa-exclamation-triangle',
          message: 'You need to accept the terms and conditions first'
          })
        }
        else {
          // this.$q.notify({
          // color: 'green-4',
          // textColor: 'white',
          // icon: 'fas fa-check-circle',
          // message: 'Submitted'
          // })
        // }
          delete this.userForm.confirmPassword
          delete this.userForm.agreeTerms          
          let country = this.userCountry
          let index = this.countries.findIndex(function f(c) {return c === country})   
          this.userForm.countryIso2Code = this.countryCodes[index]         

          this.$q.loading.show({
            delay: 400, // ms
            message: 'Creating your account<br/><span class="text-white">Hang on...</span>'
          })
          this.register(this.userForm).then(authorised => {          
            if (authorised) {
              console.log("user:");
              console.log(this.user());
              this.$q.notify({
                color: 'green-4',
                textColor: 'white',
                icon: 'fas fa-check-circle',
                message: 'Account successfully created. Welcome ' + this.user().firstName + '!'
              });

              // hiding for 2s
              this.timer = setTimeout(() => {
                this.$q.loading.hide()
                this.timer = void 0
                this.$router.push('/dashboard')
              }, 2000)                                      
            }
            else {
              this.$q.notify({
                color: 'red-7',
                textColor: 'white',
                icon: 'fas fa-exclamation-triangle',
                message: 'Something went wrong, please try again.'
              })
              this.$q.loading.hide()
              this.$router.push('/login')
            }
          }); 
        }       
      },

      onResetRegister () {
        this.userForm.firstName = null
        this.userForm.lastName = null
        this.userForm.email = null
        this.userForm.password = null
        this.userForm.confirmPassword = null
        this.userForm.newsletterSub = true
        this.userForm.agreeTerms = false
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

      async register(user) {                
        const axios = require("axios")
        try {
          var response = await axios({
            method: "POST",
            url: "/",
            data: {
              query: `                    
              mutation ($user: UserInputType!) {
                user_mutations {
                  addUser(user: $user) {
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
                  }
                }  
              }
              `,
              variables: {
                user: user
              },
            }
          });            
          
          if (response.data.data.user_mutations.addUser != null) {            
            this.updateUser(response.data.data.user_mutations.addUser);
            if (this.user() != null) {
              return true
            }
          }          
          console.log("User registration failed")
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
    },

    components: {
      'modal-currency':     require('components/SharedModals/ModalCurrencySelect.vue').default,
      'modal-select':       require('components/SharedModals/ModalSelect.vue').default      
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