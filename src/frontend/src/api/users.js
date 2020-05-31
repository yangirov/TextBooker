export const isAuth = async () => (await axios.get('/user/is-auth')).data

export const getUser = async () => (await axios.get('/user/info')).data

export const loginUser = async data =>
  (await axios.post('/user/login', data)).data

export const updateUser = async payload =>
  (await axios.put('/user', payload)).data

export const deleteUser = async () => (await axios.delete('/user')).data

export const registerUser = async payload =>
  (await axios.post('/user/register', payload)).data

export const confirmEmail = async payload =>
  (await axios.get('/user/confirm', { params: { ...payload } })).data

const users = {
  isAuth,
  getUser,
  loginUser,
  updateUser,
  registerUser,
  deleteUser,
  confirmEmail
}

export default users
