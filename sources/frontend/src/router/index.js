import Vue from 'vue'
import VueRouter from 'vue-router'

import Home from '@/views/Home.vue'
import Templates from '@/views/Templates.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '*',
    name: 'Home',
    component: Home
  },
  {
    path: '/templates',
    name: 'templates',
    component: Templates
  }
]

const router = new VueRouter({
  mode: 'hash',
  base: process.env.BASE_URL,
  routes
})

// router.beforeEach((to, from, next) => {
//   let isAuth = localStorage.getItem('access_token')

//   if (to.name !== 'Auth' && !isAuth) next({ name: 'Auth' })
//   else next()
// })

export default router
