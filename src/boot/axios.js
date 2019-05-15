import axios from 'axios'

export default async ({ Vue }) => {
  axios.defaults.baseURL = '/api'
  Vue.prototype.$axios = axios
}
