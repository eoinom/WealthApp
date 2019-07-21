import { mapGetters } from 'vuex'
import { mapActions } from 'vuex'
var moment = require('moment');

// "async" is optional
export default /* async */ ({ Vue /* app, router, Vue, ... */ }) => {  
  Vue.mixin({
    computed: {
      ...mapGetters('main', ['authenticated', 'getDateFormat', 'userDisplayCurrencyCode']),      
    },
    methods: {
      ...mapActions('main', ['logout']),

      formatDate_Iso2User(date) {   
        console.log('format: ' + this.getDateFormat)     
        return moment(date, "YYYY-MM-DD").format(this.getDateFormat)
      },
      formatDate_User2Iso(date) {        
        return moment(date, this.getDateFormat).format("YYYY-MM-DD")
      },  
      roundNumTo2Dp(num){
        return Math.round((balance + 0.00001) * 100) / 100
      },

      // https://stackoverflow.com/questions/31581011/how-to-use-tolocalestring-and-tofixed2-in-javascript
      // Number.prototype.toLocaleFixed = function(n) {
      //   return this.toLocaleString(undefined, {
      //     minimumFractionDigits: n,
      //     maximumFractionDigits: n
      //   });
      // },      
      toLocaleFixed(num, decimals) {
        return num.toLocaleString(undefined, {
          minimumFractionDigits: decimals,
          maximumFractionDigits: decimals
        })
      },
      getCurrencySymbol(shortNameOrCode) {
        try {
          if (shortNameOrCode === undefined || shortNameOrCode === '') {
            shortNameOrCode = this.userDisplayCurrencyCode
          }
          switch ( shortNameOrCode.toLowerCase() ) {
            case 'eur':
            case 'euro':
              return '€';
              break;
            case 'aud':
            case 'cad':
            case 'usd':
            case 'nzd':
            case 'dollar':
            case 'peso':
              return '$';
              break;
            case 'gbp':
            case 'pound':
              return '£';
              break;
            default:
              return '';
          }
        }
        catch (error) {
          console.error(error); 
          return "";
        } 
      }
    },
    beforeMount () {    
      if (this.$router !== undefined) {
        let currentPath = this.$router.history.current.path
        if (currentPath !== '/login' && currentPath !== '/' && currentPath !== '/#' && !this.authenticated) {
          this.logout()
          this.$router.push('/login')
          this.$q.notify({
            color: 'red-7',
            textColor: 'white',
            icon: 'fas fa-exclamation-triangle',
            message: 'Please login first!'
          })
        }
      }
    },
  })
}
