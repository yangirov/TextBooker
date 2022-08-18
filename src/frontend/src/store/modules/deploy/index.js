import api from '@/api'
import { SET_STATE, setState } from '@/store/helpers'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import i18n from '@/libs/VueI18n'
import router from '@/router'

export default {
  namespaced: true,
  state: {
    loading: false,
  },

  getters: {
    loading: state => state.loading,
    data: (state, getters) => getters,
    deployed: (state, deployed) => deployed
  },

  mutations: {
    SET_STATE
  },

  actions: {

  }
}
