import Vue from 'vue'
import Vuex from 'vuex'

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
  }
})

export default store
