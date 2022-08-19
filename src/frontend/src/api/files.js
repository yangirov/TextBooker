import axiosInstance from 'axios'

export const uploadFile = async payload => {
  return axios({
    method: 'post',
    url: '/file/upload',
    data: payload,
    headers: {'Content-Type': 'multipart/form-data'}
  });
}

export const uploadImage = async payload => {
  const { VUE_APP_IMAGE_STORAGE_URL: url, VUE_APP_IMAGE_STORAGE_API_KEY: apiKey } = process.env;

  return axiosInstance({
    method: 'post',
    url: `${url}?key=${apiKey}`,
    data: payload,
    headers: {'Content-Type': 'multipart/form-data'}
  });
}

const files = {
  uploadFile,
  uploadImage
}

export default files
