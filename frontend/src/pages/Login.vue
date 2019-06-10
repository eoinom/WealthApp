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
import ForceUpdate from '../App.vue';
//   import {mapMutations} from 'vuex'
//   import Vivus from 'vivus'
//   import logoData from './logoData'
import router from '../router';
  export default {
    created() {
	},
    mounted () {
    //   this.startAnimation()
    },
    beforeDestroy () {
    },
    computed: {
    //   heightSize (){
    //     if (Platform.is.desktop) {
    //       return 'items-center'
    //     }
    //     return ''
    //   },
    //   logoMethod () {
    //     return logoData[this.logo]
    //   }
    },
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
        // vivus: '',
        tab: 'login',
        user: {},
        authenticated: false
      }
    },
    methods: {
        onSubmitLogin () {  
            checkAuth(this.email, this.password).then(authorised => {
                if (authorised) {
                    console.log("user:");
                    console.log(user);
                    this.$q.notify({
                        color: 'green-4',
                        textColor: 'white',
                        icon: 'fas fa-check-circle',
                        message: 'Welcome back ' + user.firstName + '!'
                    });
                    this.$router.push('/accounts')                                        
                }
                else {
                    this.$q.notify({
                        color: 'red-7',
                        textColor: 'white',
                        icon: 'fas fa-exclamation-triangle',
                        message: 'Invalid credentials, please try again.'
                    })
                    this.$router.push('/login')
                }
            }).catch(error => {
                console.log(error)
            });

            async function checkAuth(email, password) {                
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
                                        userId,
                                        firstName,
                                        lastName,
                                        country {
                                            iso2Code
                                        },
                                        displayCurrency {
                                            code
                                        }
                                        newsletterSub
                                    }
                                }  
                            }
                            `
                        }
                    });                    
                    this.user = response.data.data.user_queries.userLogin; 
                    
                    if (this.user != null) {
                        return true
                    }
                    else {
                        console.log("Login failed")
                        return false
                    }
                } catch (error) {
                    console.error(error); 
                    console.log("Error")
                }
                return false
            };
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

        onResetLogin () {
            this.email = null
            this.password = null
            this.confirmPassword = false
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
    //   startAnimation () {
    //     this.vivus = new Vivus('logo', {
    //         duration: 400,
    //       forceRender: false
    //       }, function(element) {
    //         for (let item of element.el.children[0].children) {
    //           item.setAttribute('style', 'fill:white')
    //           item.setAttribute('style', 'fill:white')
    //         }
    //       }
    //     )
    //   }
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