import Vue from 'vue'
import wysiwyg from 'vue-wysiwyg'
import 'vue-wysiwyg/dist/vueWysiwyg.css'

const baseURL =
  process.env.NODE_ENV == 'production' ? 'api/' : process.env.VUE_APP_HOST

const editorOptions = {
  hideModules: { bold: true },
  iconOverrides: { bold: "<i class='icon'></i>" },
  image: {
    uploadURL: `${baseURL}/upload`,
    dropzoneOptions: {
      thumbnailWidth: 150,
      maxFilesize: 2 // MB
    }
  },
  forcePlainTextOnPaste: false,
  locale: process.env.VUE_APP_I18N_LOCALE || 'en'
}

Vue.use(wysiwyg, editorOptions)
