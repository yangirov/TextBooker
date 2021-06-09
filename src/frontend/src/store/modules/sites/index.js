import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import router from '@/router'

let initState = {
  title: '',
  description: '',
  keywords: '',
  enabledUserScripts: false,
  templateId: 1,
  icon: '',
  userScripts: [],
  sectionNames: []
}

export default {
  namespaced: true,

  state: {
    loading: false,
    templates: [],
    templateKeys: [],
    projects: [],
    site: { ...initState }
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    templates: state => state.templates,
    templateKeys: state => state.templateKeys,
    projects: state => state.projects,
    site: state => state.site
  },

  mutations: {
    SET_STATE,

    UPDATE_SITE(state, data = {}) {
      state.site = { ...state.site, ...data }
    }
  },

  actions: {
    async fetchProjects({ commit }) {
      setState(commit, { loading: true })
      const sites = await api.getProjects()
      setState(commit, { state: 'projects', payload: sites })
      setState(commit, { loading: false })
    },

    async deleteSite({ commit, dispatch }, siteId) {
      try {
        setState(commit, { loading: true })
        await api.deleteSite(siteId)
        await dispatch('reset')
        await dispatch('fetchProjects')
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
    },

    async fetchSite({ commit }, siteId) {
      try {
        setState(commit, { loading: true })
        let site = await api.getSite(siteId)
        setState(commit, { state: 'site', payload: site })
        router.push('/editor')
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async addSite({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        let siteId = await api.addSite(payload)
        await dispatch('fetchSite', siteId)
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async updateSite({ commit, state }) {
      try {
        setState(commit, { loading: true })
        let site = await api.updateSite(state.site)
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    reset({ commit }) {
      setState(commit, { state: 'loading', payload: false })
      setState(commit, { state: 'sites', payload: [] })
      setState(commit, { state: 'site', payload: {} })
    }
  }
}
