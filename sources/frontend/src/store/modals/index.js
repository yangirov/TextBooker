const modal = {
  USER_MODAL: 'USER_MODAL'
}

export const {
  USER_MODAL
} = modal

export default {
  namespaced: true,

  state: Object.keys(modal).reduce((acc, modalName) => (acc[modalName] = false, acc), {}),

  mutations: {
    SET_MODAL(state, payload) {
      Object.assign(state, payload)
    }
  },

  actions: {
    open({ commit }, name) {
      commit('SET_MODAL', { [name]: true })
    },

    close({ commit }, name) {
      commit('SET_MODAL', { [name]: false })
    }
  }
}
