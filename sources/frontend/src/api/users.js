export const isAuth = async () => (await axios.get('/user/is-auth')).data

export const getUser = async () => (await axios.get('/user/info')).data

export const getUsers = async () => (await axios.get('/user/all')).data

export const loginUser = async data =>
  (await axios.post('/user/login', data)).data

export const updateUser = async payload =>
  (await axios.put('/user/update', payload)).data

export const registerUser = async payload =>
  (await axios.post('/user/register', payload)).data

const users = {
  isAuth,
  getUsers,
  getUser,
  loginUser,
  updateUser,
  registerUser
}

export default users
