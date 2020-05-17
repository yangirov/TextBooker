import Vue from 'vue'
import Vuex from 'vuex'

import modals from './modals'
import appState from './modules/appState'

Vue.use(Vuex)
window.Vuex = Vuex

let store = new Vuex.Store({
  modules: {
    appState,
    modals
  }
})

;(async () => await store.dispatch('appState/init'))()

export default store
