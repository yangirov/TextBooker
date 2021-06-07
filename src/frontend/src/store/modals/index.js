const modal = {
  USER_SCRIPTS_MODAL: 'USER_SCRIPTS_MODAL'
}

export const { USER_SCRIPTS_MODAL } = modal

export default {
  namespaced: true,

  state: Object.keys(modal).reduce(
    (acc, modalName) => ((acc[modalName] = false), acc),
    {}
  ),

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
