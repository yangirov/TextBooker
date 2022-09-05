import {SET_STATE, setState} from '@/store/helpers'
import api from "@/api";

export default {
  namespaced: true,
  state: {
    loading: false,
    ftpStatus: false,
    vercelStatus: false
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    vercelStatus: (state) => state.vercelStatus,
  },

  mutations: {
    SET_STATE
  },

  actions: {
    async downloadSite({ commit }, payload) {
      await api.downloadSite(payload);
    },
  }
}
