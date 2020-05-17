import Vue from 'vue'
import store from '@/store'

const Modal = {
  install(Vue, store) {
    Vue.prototype.$modal = new Vue({
      computed: {
        isVisible: () => name => store.state.modals[name]
      },
      methods: {
        open(name) {
          store.dispatch('modals/open', name)
        },
        close(name) {
          store.dispatch('modals/close', name)
        }
      }
    })
  }
}

Vue.use(Modal, store)
