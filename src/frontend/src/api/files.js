import axiosInstance from 'axios'

export const uploadFile = async payload => {
  return axios({
    method: 'post',
    url: '/file/upload',
    data: payload,
    headers: {'Content-Type': 'multipart/form-data'}
  });
}

export const downloadSite = async ({title, id}) => {
  axios({
    method: 'get',
    url: `/file/download/${id}`,
    responseType: 'blob'
  })
    .then(blobby => {
      let anchor = document.createElement("a");
      document.body.appendChild(anchor);

      let objectUrl = window.URL.createObjectURL(blobby.data);

      anchor.href = objectUrl;
      anchor.download = `${title}.zip`;
      anchor.click();

      window.URL.revokeObjectURL(objectUrl);
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
  uploadImage,
  downloadSite
}

export default files
