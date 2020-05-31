import Vue from 'vue'
import VueRouter from 'vue-router'

import Editor from '@/views/Editor.vue'
import Feedback from '@/views/Feedback.vue'
import About from '@/views/About.vue'
import NotFound from '@/views/NotFound.vue'

import SignIn from '@/views/User/SignIn.vue'
import SignUp from '@/views/User/SignUp.vue'
import Confirm from '@/views/User/Confirm.vue'
import Projects from '@/views/User/Projects.vue'
import Settings from '@/views/User/Settings.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    component: About
  },
  {
    path: '*',
    redirect: '/404'
  },
  {
    path: '/404',
    name: '404',
    component: NotFound
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
    path: '/signin',
    name: 'signin',
    component: SignIn
  },
  {
    path: '/signup',
    name: 'signup',
    component: SignUp
  },
  {
    path: '/user/confirm',
    name: 'confirm',
    component: Confirm
  },
  {
    path: '/user/projects',
    name: 'projects',
    component: Projects
  },
  {
    path: '/user/settings',
    name: 'settings',
    component: Settings
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

router.beforeEach((to, from, next) => {
  let isAuth = localStorage.getItem('access_token')

  if (!isAuth && ['editor', 'settings', 'projects'].includes(to.name))
    next({ name: 'signin' })
  else next()
})

export default router
