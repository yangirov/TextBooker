import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import { lodash as _ } from '@/utils'

export default {
  namespaced: true,

  state: {
    loading: false,
    blocks: [],
    block: {}
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    blocks: state => state.blocks,
    block: state => state.block
  },

  mutations: {
    SET_STATE,

    DELETE_BLOCK(state, id) {
      state.blocks = state.blocks.splice(
        state.blocks.findIndex(item => item.id === id),
        1
      )
    },

    SET_BLOCK(state, data = {}) {
      state.block = { ...state.block, ...data }

      state.blocks = _.values(
        _.merge(_.keyBy(state.blocks, 'id'), _.keyBy([data], 'id'))
      )
    }
  },

  actions: {
    async fetchBlocks({ commit }, siteId) {
      setState(commit, { loading: true })
      const blocks = await api.getBlocks(siteId)
      setState(commit, { state: 'blocks', payload: blocks })
      setState(commit, { loading: false })
    },

    async addBlock({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        let block = await api.addBlock(payload)
        setState(commit, { state: 'block', payload: block })
        await dispatch('fetchBlocks', payload.siteId)
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async updateBlocks({ commit, state }) {
      try {
        setState(commit, { loading: true })
        let result = await api.updateBlocks(state.blocks)
        setState(commit, { state: 'blocks', payload: result })
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    async deleteBlock({ commit, dispatch }, payload) {
      try {
        setState(commit, { loading: true })
        commit('DELETE_BLOCK', payload.id)
        let result = await api.deleteBlock(payload)
        setState(commit, { state: 'block', payload: {} })
        await dispatch('fetchBlocks', payload.siteId)
        showSuccessNotify()
      } catch (error) {
        showErrorNotify(error.detail)
      } finally {
        setState(commit, { loading: false })
      }
    },

    reset({ commit }) {
      setState(commit, { state: 'loading', payload: false })
      setState(commit, { state: 'blocks', payload: [] })
      setState(commit, { state: 'block', payload: {} })
    }
  }
}
