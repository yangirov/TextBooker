import axiosInstance from 'axios'

const axios = axiosInstance.create({
  baseURL:
    process.env.NODE_ENV == 'production' ? 'api/' : process.env.VUE_APP_HOST,
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
    let { status, detail } = error.response.data
    return Promise.reject(detail)
  }
)

window._http = axios
window.axios = axios

export { axios }
