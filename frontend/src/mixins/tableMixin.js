var moment = require('moment');

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
      return moment(date, "YYYY/MM/DD") <= moment()
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
            var xDate = moment(x.date, "YYYY-MM-DD");
            var yDate = moment(y.date, "YYYY-MM-DD");
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