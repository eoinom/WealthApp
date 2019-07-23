import axios from 'axios'

export default async ({ Vue }) => {
  // axios.defaults.baseURL = 'https://localhost:5001/graphql'
  axios.defaults.baseURL = 'https://api.wealthapp.io/graphql'
  Vue.prototype.$axios = axios
}
