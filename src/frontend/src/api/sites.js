export const getSites = async () => (await axios.get('/editor/projects')).data

export const addSite = async payload =>
  (await axios.post('/editor/site', payload)).data

export const updateSite = async payload =>
  (await axios.put('/editor/site', payload)).data

export const deleteSite = async siteId =>
  (await axios.delete(`/editor/site?id=${siteId}`)).data

export const getSiteInfo = async siteId =>
  (await axios.get(`/editor/site?id=${siteId}`)).data

export const getTemplates = async () =>
  (await axios.get('/editor/templates')).data

export const getTemplateKeys = async () =>
  (await axios.get('/editor/template-keys')).data

const sites = {
  getSites,
  addSite,
  updateSite,
  deleteSite,
  getTemplates,
  getTemplateKeys,
  getSiteInfo
}

export default sites