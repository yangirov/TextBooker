import Vue from 'vue'
import Vuex from 'vuex'

import modals from './modals'
import appState from './modules/appState'
import user from './modules/user'

Vue.use(Vuex)
window.Vuex = Vuex

let store = new Vuex.Store({
  modules: {
    modals,
    appState,
    user
  }
})

export default store
