import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import i18n from '@/libs/VueI18n'
import router from '@/router'

export default {
  namespaced: true,
  state: {
    loading: false,
    isInitAppState: false,
    settings: {}
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    isInitAppState: state => state.isInitAppState,
    systemName: ({ settings }) => settings.name,
    version: ({ settings }) => settings.version
  },

  mutations: {
    SET_STATE
  },

  actions: {
    async init({ state, commit, dispatch }) {
      if (state.isInitAppState) return
      await dispatch('fetchSettings')
      setState(commit, { state: 'isInitAppState', payload: true })
    },

    async fetchSettings({ commit }) {
      const settings = await api.getSettings()
      setState(commit, { state: 'settings', payload: settings })
    },

    async sendFeedback({ commit }, payload) {
      try {
        setState(commit, { loading: true })
        await api.sendFeedback(payload)
        showSuccessNotify(i18n.t('feedback.thanks'))
        router.push('/')
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    }
  }
}
