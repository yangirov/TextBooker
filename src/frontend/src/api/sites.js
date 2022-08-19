export const addSite = async payload =>
  (await axios.post('/editor/site', payload)).data

export const getSite = async siteId =>
  (await axios.get(`/editor/site?id=${siteId}`)).data

export const updateSite = async payload =>
  (await axios.put('/editor/site', payload)).data

export const deleteSite = async siteId =>
  (await axios.delete(`/editor/site?id=${siteId}`)).data

export const getProjects = async () =>
  (await axios.get('/editor/projects')).data

export const getTemplates = async () =>
  (await axios.get('/editor/templates')).data

export const getTemplateKeys = async () =>
  (await axios.get('/editor/template-keys')).data

export const generateSite = async siteId =>
  (await axios.get(`/editor/generate?siteId=${siteId}`)).data

const sites = {
  addSite,
  getSite,
  updateSite,
  deleteSite,
  getProjects,
  getTemplates,
  getTemplateKeys,
  generateSite
}

export default sites
