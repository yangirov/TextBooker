import Vue from 'vue'
import App from './App.vue'

import i18n from './libs/VueI18n'
import Element from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'

import router from './router'
import store from './store'

import './utils'
import './mixins'
import './directives'
import './libs'

Vue.use(Element, {
  i18n: (key, value) => i18n.t(key, value),
  size: 'medium'
})

new Vue({
  router,
  store,
  i18n,
  render: h => h(App)
}).$mount('#app')

window.Vue = Vue

const isDevelopment = process.env.NODE_ENV === 'development'

Vue.config.productionTip = isDevelopment
Vue.config.devtools = true
Vue.config.debug = isDevelopment
Vue.config.silent = true
