export const institutionUrlsMixin = {
  methods: {
    getInstitutionUrl(companyName) {
      try {
        switch ( companyName.toLowerCase() ) {
          case 'aib':
          case 'allied irish bank':   return 'aib.ie';     break;
          case 'boi':
          case 'bank of ireland':   return 'bankofireland.com';     break;
          case 'kbc':   return 'kbc.ie';     break;
          case 'permanent tsb':   return 'bankofireland.com';     break;
          case 'ptsb':   return 'permanenttsb.ie';     break;
          case 'ulster bank':   return 'ulsterbank.ie';     break;
          case 'bank of america':   return 'bankofamerica.com';     break;
          case 'commonwealth bank of australia':   return 'commbank.com.au';     break;
          case 'j.p. morgan':
          case 'jp morgan':   return 'jpmorgan.com';     break;
          default:
          return '';
        }
      }
      catch (error) {
        console.error(error); 
        return "";
      } 
    },
    getInstitutionLogoSrc(institutionName, size) {
      var url = this.getInstitutionUrl(institutionName)
      if (url != '') {
        if (size != undefined) {
          return '//logo.clearbit.com/' + url + '?size=' + size
        }
        else {
          return '//logo.clearbit.com/' + url
        }
      }
      else {
        return ''
      }
    }
  }
}