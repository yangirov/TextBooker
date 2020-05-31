export const getSettings = async () => (await axios.get('/settings')).data

export const sendFeedback = async payload =>
  (await axios.post('/feedback', payload)).data

const common = {
  getSettings,
  sendFeedback
}

export default common
