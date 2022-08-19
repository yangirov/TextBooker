import { SET_STATE } from '@/store/helpers'

export default {
  namespaced: true,
  state: {
    loading: false,
    deployed: false
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    deployed: (state) => state.deployed
  },

  mutations: {
    SET_STATE
  },

  actions: {

  }
}
