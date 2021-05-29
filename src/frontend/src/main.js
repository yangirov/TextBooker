import Vue from 'vue'
import App from './App.vue'
import i18n from './libs/VueI18n'
import 'element-ui/lib/theme-chalk/index.css'
import Element from 'element-ui'
import router from './router'
import store from './store'
import './utils'
import './mixins'
import './directives'
import './libs'
import ButtonAction from './components/ButtonAction'
import ButtonDelete from './components/ButtonDelete'
import SelectList from './components/SelectList'
import { VueReCaptcha } from 'vue-recaptcha-v3'

Vue.use(VueReCaptcha, {
  siteKey: process.env.VUE_APP_RECAPTCHA_SITE,
  loaderOptions: {
    useRecaptchaNet: true,
    autoHideBadge: true
  }
})

Vue.use(SelectList)
Vue.use(ButtonAction)
Vue.use(ButtonDelete)

Vue.use(Element, {
  i18n: (key, value) => i18n.t(key, value)
})

Vue.config.productionTip = false

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
