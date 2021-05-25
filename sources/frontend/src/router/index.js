import Vue from 'vue'
import VueRouter from 'vue-router'

import Editor from '@/views/Editor.vue'
import Feedback from '@/views/Feedback.vue'
import About from '@/views/About.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '*',
    name: 'About',
    component: About
  },
  {
    path: '/about',
    name: 'about',
    component: About
  },
  {
    path: '/feedback',
    name: 'feedback',
    component: Feedback
  },
  {
    path: '/editor',
    name: 'editor',
    component: Editor
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
