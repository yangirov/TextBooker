export const isAuth = async () => (await axios.get('/user/is-auth')).data

export const getUser = async () => (await axios.get('/user/info')).data

export const getUsers = async () => (await axios.get('/user/all')).data

export const loginUser = async data =>
  (await axios.post('/user/login', data)).data

export const updateUser = async payload =>
  (await axios.put('/user/update', payload)).data

export const addUser = async payload =>
  (await axios.post('/user/add', payload)).data

const users = {
  isAuth,
  getUsers,
  getUser,
  loginUser,
  updateUser,
  addUser
}

export default users
