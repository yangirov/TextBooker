export const getUserSites = async () => (await axios.get('/editor/site')).data

export const deleteUserSite = async siteId =>
  (await axios.delete(`/editor?siteId=${siteId}`)).data

export const getTemplates = async () =>
  (await axios.get('/editor/templates')).data

export const getTemplateKeys = async () =>
  (await axios.get('/editor/template-keys')).data

const sites = {
  getUserSites,
  deleteUserSite,
  getTemplates,
  getTemplateKeys
}

export default sites
