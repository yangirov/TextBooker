import Vue from 'vue'
import Vuex from 'vuex'
import createPersistedState from 'vuex-persistedstate'

import modals from './modals'
import appState from './modules/appState'
import user from './modules/user'
import sites from './modules/sites'

Vue.use(Vuex)
window.Vuex = Vuex

let store = new Vuex.Store({
  modules: {
    modals,
    appState,
    user,
    sites
  },
  plugins: [createPersistedState()]
})

export default store
