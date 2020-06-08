import api from '@/api'
import { resetStore } from '@/store'
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

    async login({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        let { token } = await api.loginUser(payload)
        if (token) {
          localStorage.setItem('access_token', token)
          setState(commit, { isAuth: true })
          await dispatch('fetchUserInfo')
        }
        router.push('/editor')
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async register({ commit }, payload) {
      try {
        setState(commit, { loading: true })
        await api.registerUser(payload)
        router.push({ name: 'email-check', params: { email: payload.email } })
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async confirmEmail({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        await api.confirmEmail(payload)
        localStorage.setItem('access_token', payload.token)
        setState(commit, { isAuth: true })
        await dispatch('fetchUserInfo')
        router.push('/editor')
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async update({ commit, dispatch }, { username }) {
      try {
        setState(commit, { loading: true })
        await api.updateUser({ username })
        await dispatch('fetchUserInfo')
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async delete({ commit, dispatch }) {
      try {
        setState(commit, { loading: true })
        await api.deleteUser()
        showSuccessNotify(i18n.t('status.success'))
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    reset({ commit }) {
      setState(commit, { state: 'loading', payload: false })
      setState(commit, { state: 'isAuth', payload: false })
      setState(commit, { state: 'user', payload: {} })
    }
  }
}
