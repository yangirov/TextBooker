import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import { lodash as _ } from '@/utils'

export default {
  namespaced: true,

  state: {
    loading: false,
    pages: [],
    page: {}
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    pages: state => state.pages,
    page: state => state.page
  },

  mutations: {
    SET_STATE,

    DELETE_PAGE(state, id) {
      state.pages = state.pages.splice(
        state.pages.findIndex(item => item.id === id),
        1
      )
    },

    SET_PAGE(state, data = {}) {
      state.page = { ...state.page, ...data }

      state.pages = _.values(
        _.merge(_.keyBy(state.pages, 'id'), _.keyBy([data], 'id'))
      )
    },

    UPDATE_PAGES(state, data = []) {
      state.pages = data
    }
  },

  actions: {
    async fetchPages({ commit }, siteId) {
      setState(commit, { loading: true })
      const pages = await api.getPages(siteId)
      setState(commit, { state: 'pages', payload: pages })
      setState(commit, { loading: false })
    },

    async addPage({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        let page = await api.addPage(payload)
        setState(commit, { state: 'page', payload: page })
        await dispatch('fetchPages', payload.siteId)
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async updatePages({ commit, state }) {
      try {
        setState(commit, { loading: true })
        let result = await api.updatePages(state.pages)
        setState(commit, { state: 'pages', payload: result })
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async deletePage({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        commit('DELETE_PAGE', payload.id)
        let result = await api.deletePage(payload)
        setState(commit, { state: 'page', payload: {} })
        await dispatch('fetchPages', payload.siteId)
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    reset({ commit }) {
      setState(commit, { state: 'loading', payload: false })
      setState(commit, { state: 'pages', payload: [] })
      setState(commit, { state: 'page', payload: {} })
    }
  }
}
