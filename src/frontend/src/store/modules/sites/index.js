import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'

export default {
  namespaced: true,
  state: {
    loading: false,
    sites: []
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    sites: state => state.sites
  },

  mutations: {
    SET_STATE
  },

  actions: {
    async fetchUserSites({ commit }) {
      setState(commit, { loading: true })
      const sites = await api.getUserSites()
      setState(commit, { state: 'sites', payload: sites })
      setState(commit, { loading: false })
    },

    async deleteUserSite({ commit, dispatch }, siteId) {
      try {
        setState(commit, { loading: true })
        await api.deleteUserSite(siteId)
        await dispatch('fetchUserSites')
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    }
  }
}
