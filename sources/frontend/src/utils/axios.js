import axiosInstance from 'axios'
import { showErrorNotify } from '@/utils/notify'

const axios = axiosInstance.create({
    baseURL: process.env.NODE_ENV == "production" ? 'api/' : process.env.VUE_APP_HOST,
    timeout: 1000 * 60 * 10,
    responseType: 'json',
    headers: {
      'Content-Type': 'application/json charset=utf-8',
    }
})

axios.CancelToken = axiosInstance.CancelToken
axios.isCancel = axiosInstance.isCancel

axios.interceptors.request.use(
    config => {
        config.headers.Authorization = `Bearer ${localStorage.getItem('access_token')}`
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

        let responseURL = response.request.responseURL

        if (/\/auth\/expired/.test(responseURL)) {
            showErrorNotify('Повторно авторизуйтесь в системе', {
                title: 'Сессия авторизации закончилась',
            })

            setTimeout(() => {
                window.location.href = '/auth/expired'
            }, 2000)
            return Promise.reject(response)
        }

        if (/\/auth\/accessdenied/.test(responseURL)) {
            window.location.href = '/auth/accessdenied'
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
