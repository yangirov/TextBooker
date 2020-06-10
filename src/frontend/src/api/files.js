export const uploadFile = async payload => {
  return await axios({
    method: 'post',
    url: '/file/upload',
    data: payload,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

const files = {
  uploadFile
}

export default files
