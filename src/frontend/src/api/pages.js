export const getPages = async siteId =>
  (await axios.get(`/editor/page/all?siteId=${siteId}`)).data

export const addPage = async payload =>
  (await axios.post('/editor/page', payload)).data

export const getPage = async payload =>
  (await axios.get('/editor/page', { params: { ...payload } })).data

export const updatePages = async payload =>
  (await axios.put('/editor/page', payload)).data

export const deletePage = async payload =>
  (await axios.delete('/editor/page', { params: { ...payload } })).data

const pages = {
  getPages,
  addPage,
  getPage,
  updatePages,
  deletePage
}

export default pages
