export const tableMixin = {
  data () {
    return {
      selectedValues: [],         // selected rows in table
      popupEditDate: '2019-01-31',
      popupEditValue: 0,
      loading: false,
      filter: '',
      pagination: {
        sortBy: 'date',
        descending: true,
        rowsPerPage: 7
      },
    }
  },  
  
  methods: {
    showPopupDate(row, col) {
      this.popupEditDate = row[col];
    },
    showPopupValue(row, col) {
      console.log('row: ' + row + ' , col: ' + col + ' row[col]: ' + row[col])
      this.popupEditValue = row[col];
    },
    dateOptionsFn(date) {
      var today = new Date();
      var dd = today.getDate();
      var mm = today.getMonth()+1; 
      var yyyy = today.getFullYear();
      if(dd<10) 
      dd='0'+dd;
      if(mm<10) 
      mm='0'+mm;
      return date <= yyyy + '/' + mm + '/' + dd;
    },
    customTableSort(rows, sortBy, descending) {
      let data = [...rows]
      
      if (sortBy) {
        data.sort((a, b) => {
          let x = descending ? b : a
          let y = descending ? a : b
          if (sortBy === 'date') {
            // string sort
            // return x[sortBy] > y[sortBy] ? 1 : x[sortBy] < y[sortBy] ? -1 : 0
            
            // date sort
            var xDate = new Date();
            var yDate = new Date();
            var dd = '';
            var mm = '';
            var yyyy = '';
            
            switch(this.getDateFormat) {
              case "YYYY-MM-DD":
              case "MM/DD/YYYY":
              xDate = new Date(x[sortBy]);
              yDate = new Date(y[sortBy]);
              break;
              case "DD-MM-YYYY":
              case "DD/MM/YYYY":
              dd = x[sortBy].slice(0,2)
              mm = x[sortBy].slice(3,5)
              yyyy = x[sortBy].slice(6,10)
              xDate = new Date(yyyy + '-' + mm + '-' + dd);
              dd = y[sortBy].slice(0,2)
              mm = y[sortBy].slice(3,5)
              yyyy = y[sortBy].slice(6,10)
              yDate = new Date(yyyy + '-' + mm + '-' + dd);
              break;
              case "MM-DD-YYYY":
              mm = x[sortBy].slice(0,2)
              dd = x[sortBy].slice(3,5)
              yyyy = x[sortBy].slice(6,10)
              xDate = new Date(yyyy + '-' + mm + '-' + dd);
              mm = y[sortBy].slice(0,2)
              dd = y[sortBy].slice(3,5)
              yyyy = y[sortBy].slice(6,10)
              yDate = new Date(yyyy + '-' + mm + '-' + dd);
              break;
              case "YYYY/MM/DD":
              yyyy = x[sortBy].slice(0,4)
              mm = x[sortBy].slice(5,7)
              dd = x[sortBy].slice(8,10)
              xDate = new Date(yyyy + '-' + mm + '-' + dd);
              yyyy = y[sortBy].slice(0,4)
              mm = y[sortBy].slice(5,7)
              dd = y[sortBy].slice(8,10)
              yDate = new Date(yyyy + '-' + mm + '-' + dd);
              break;
              default:
              // do nothing
            }
            return xDate - yDate;
          }
          else {
            // numeric sort
            return parseFloat(x[sortBy]) - parseFloat(y[sortBy])
          }
        })
      }
      return data
    }
  }
}