export const getUserSites = async () => (await axios.get('/editor/site')).data

export const deleteUserSite = async siteId =>
  (await axios.delete(`/editor?siteId=${siteId}`)).data

export const getTemplates = async () =>
  (await axios.get('/editor/templates')).data

const sites = {
  getUserSites,
  deleteUserSite,
  getTemplates
}

export default sites
