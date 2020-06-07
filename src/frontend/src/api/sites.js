export const getUserSites = async () =>
  (await axios.get('/editor/projects')).data

export const deleteUserSite = async siteId =>
  (await axios.delete(`/editor/site?id=${siteId}`)).data

export const getSiteInfo = async siteId =>
  (await axios.get(`/editor/site?id=${siteId}`)).data

export const getTemplates = async () =>
  (await axios.get('/editor/templates')).data

export const getTemplateKeys = async () =>
  (await axios.get('/editor/template-keys')).data

const sites = {
  getUserSites,
  deleteUserSite,
  getTemplates,
  getTemplateKeys,
  getSiteInfo
}

export default sites
