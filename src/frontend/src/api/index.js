import common from './common'
import users from './users'
import sites from './sites'
import blocks from './blocks'
import pages from './pages'
import files from './files'

const api = {
  ...common,
  ...users,
  ...sites,
  ...blocks,
  ...pages,
  ...files
}

export default api
