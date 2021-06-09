import common from './common'
import users from './users'
import sites from './sites'
import blocks from './blocks'
import pages from './pages'

const api = {
  ...common,
  ...users,
  ...sites,
  ...blocks,
  ...pages
}

export default api
