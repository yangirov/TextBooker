const modal = {
  USER_SCRIPTS_MODAL: 'USER_SCRIPTS_MODAL',
  TWITTER_MODAL: 'TWITTER_MODAL',
  FTP_MODAL: 'FTP_MODAL',
  GITHUB_MODAL: 'GITHUB_MODAL',
  VERCEL_MODAL: 'VERCEL_MODAL'
}

export const {
  USER_SCRIPTS_MODAL,
  TWITTER_MODAL,
  FTP_MODAL,
  GITHUB_MODAL,
  VERCEL_MODAL
} = modal

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
