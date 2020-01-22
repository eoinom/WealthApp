import axios from 'axios'

export default async ({ Vue }) => {
  // axios.defaults.baseURL = 'https://localhost:5001/graphql'
  // axios.defaults.baseURL = 'https://192.168.1.3:5001/graphql'
  // axios.defaults.baseURL = 'https://api.wealthapp.io/graphql'
  axios.defaults.baseURL = 'https://wealthapp-backendapi.azurewebsites.net/graphql/'
  Vue.prototype.$axios = axios
}
