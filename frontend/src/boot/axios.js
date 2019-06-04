import axios from 'axios'

export default async ({ Vue }) => {
  axios.defaults.baseURL = 'http://localhost:51980/graphql'
  Vue.prototype.$axios = axios
}
