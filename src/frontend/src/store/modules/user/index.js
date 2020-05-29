import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import i18n from '@/libs/VueI18n'
import router from '@/router'

export default {
  namespaced: true,
  state: {
    loading: false,
    isAuth: false,
    user: {}
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    isAuth: state => state.isAuth,
    user: state => state.user
  },

  mutations: {
    SET_STATE
  },

  actions: {
    async init({ commit, dispatch }) {
      const isAuth = await api.isAuth()
      setState(commit, { state: 'isAuth', payload: isAuth })

      if (isAuth) {
        await dispatch('fetchUserInfo')
      }
    },

    async fetchUserInfo({ commit }) {
      const user = await api.getUser()
      setState(commit, { state: 'user', payload: user })
    },

    async login({ commit, dispatch }, { email, password }) {
      try {
        setState(commit, { loading: true })
        let { token } = await api.loginUser({ email, password })
        if (token) {
          localStorage.setItem('access_token', token)
          setState(commit, { isAuth: true })
          await dispatch('fetchUserInfo')
        }
        router.push('/user/projects')
      } catch (error) {
        showErrorNotify(i18n.t('status.authError'))
      } finally {
        setState(commit, { loading: false })
      }
    },

    async register({ commit }, { email, password }) {
      try {
        setState(commit, { loading: true })
        let result = await api.registerUser({ email, password })
      } catch (error) {
        showErrorNotify(error)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async logout({ commit }) {
      try {
        setState(commit, { loading: true })
        localStorage.removeItem('access_token')
        setState(commit, { isAuth: false, user: {} })
      } finally {
        setState(commit, { loading: false })
      }
    }
  }
}
