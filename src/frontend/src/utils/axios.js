import axiosInstance from 'axios'

const baseURL =
  process.env.NODE_ENV == 'production' ? 'api/' : process.env.VUE_APP_HOST

const axios = axiosInstance.create({
  baseURL,
  timeout: 1000 * 60 * 10,
  responseType: 'json',
  headers: {
    'Content-Type': 'application/json charset=utf-8'
  }
})

axios.CancelToken = axiosInstance.CancelToken
axios.isCancel = axiosInstance.isCancel

axios.interceptors.request.use(
  config => {
    let token = localStorage.getItem('access_token')
    config.headers.Authorization = `Bearer ${token}`
    return config
  },
  error => {
    return error
  }
)

axios.interceptors.response.use(
  response => {
    return response
  },
  error => {
    let data = error.response.data
    return Promise.reject(data)
  }
)

window._http = axios
window.axios = axios

export { axios }
