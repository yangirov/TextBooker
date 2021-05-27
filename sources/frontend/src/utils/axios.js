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
    config.headers.Authorization = `Bearer ${localStorage.getItem(
      'access_token'
    )}`
    return config
  },
  error => {
    return error
  }
)

axios.interceptors.response.use(
  response => {
    const { status } = response

    if (status >= 500) {
      return Promise.reject(response)
    }

    return response
  },
  error => {
    return Promise.reject(error)
  }
)

window._http = axios
window.axios = axios

export { axios }
