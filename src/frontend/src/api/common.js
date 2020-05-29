export const getSettings = async () => (await axios.get('/settings')).data

const common = {
  getSettings
}

export default common
