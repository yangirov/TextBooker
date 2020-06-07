import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'

export default {
  namespaced: true,
  state: {
    loading: false,
    templates: [],
    templateKeys: [],
    sites: [],
    site: {}
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    templates: state => state.templates,
    templateKeys: state => state.templateKeys,
    sites: state => state.sites,
    site: state => state.site
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
    },

    async fetchTemplates({ commit }) {
      setState(commit, { loading: true })
      const templates = await api.getTemplates()
      setState(commit, { state: 'templates', payload: templates })
      setState(commit, { loading: false })
    },

    async fetchTemplateKeys({ commit }) {
      setState(commit, { loading: true })
      const keys = await api.getTemplateKeys()
      setState(commit, { state: 'templateKeys', payload: keys })
      setState(commit, { loading: false })
    }
  }
}
