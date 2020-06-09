export const getBlocks = async siteId =>
  (await axios.get(`/editor/block/all?siteId=${siteId}`)).data

export const addBlock = async payload =>
  (await axios.post('/editor/block', payload)).data

export const getBlock = async payload =>
  (await axios.get('/editor/block', { params: { ...payload } })).data

export const updateBlocks = async payload =>
  (await axios.put('/editor/block', payload)).data

export const deleteBlock = async payload =>
  (await axios.delete('/editor/block', { params: { ...payload } })).data

const blocks = {
  getBlocks,
  addBlock,
  getBlock,
  updateBlocks,
  deleteBlock
}

export default blocks
