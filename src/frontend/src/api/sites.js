export const getUserSites = async () => (await axios.get('/editor/site')).data

export const deleteUserSite = async siteId =>
  (await axios.delete(`/editor?siteId=${siteId}`)).data

const sites = {
  getUserSites,
  deleteUserSite
}

export default sites
