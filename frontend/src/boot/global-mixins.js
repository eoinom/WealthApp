// import something here

// "async" is optional
export default /* async */ ({ Vue /* app, router, Vue, ... */ }) => {  
  Vue.mixin({
    methods: {
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
      }
    }
  })
}
