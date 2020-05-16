import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

import './libs'
import './utils'
import './mixins'
import './directives';

import ButtonAction from './components/ButtonAction';
import ButtonDelete from './components/ButtonDelete';

Vue.use(ButtonAction);
Vue.use(ButtonDelete);

Vue.config.productionTip = false
const isDevelopment = process.env.NODE_ENV === 'development'

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')

window.Vue = Vue

Vue.config.productionTip = isDevelopment
Vue.config.devtools = true
Vue.config.debug = isDevelopment
Vue.config.silent = true
