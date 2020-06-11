<template>
  <div class="content_editor">
    <el-drawer
      :title="title ? title : $t('common.preview')"
      :visible="previewVisible"
      @close="closeDrawer"
    >
      <div v-html="value"></div>
    </el-drawer>

    <el-upload
      v-if="!htmlMode"
      action=""
      :http-request="uploadImage"
      :on-change="uploadChanged"
      :before-upload="beforeIconUpload"
      :multiple="false"
      :auto-upload="true"
      :show-file-list="false"
      accept="image/png, image/jpg"
      id="image-btn"
    >
      <i class="icon el-icon-picture-outline"></i>
    </el-upload>

    <wysiwyg v-if="!htmlMode" v-model="value" ref="editor" />

    <VueAceEditor
      v-if="htmlMode"
      v-model="value"
      :options="aceOptions"
      class="mt-1"
    ></VueAceEditor>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import api from '@/api'

import VueAceEditor from './VueAceEditor.vue'

export default {
  components: {
    VueAceEditor
  },

  props: {
    value: {
      type: String,
      default: ''
    },

    title: {
      type: String,
      default: ''
    },

    style: {
      type: String,
      default: ''
    },

    previewVisible: {
      type: Boolean,
      default: false
    },

    htmlMode: {
      type: Boolean,
      default: false
    }
  },

  data: () => ({
    file: {},
    aceOptions: {
      mode: 'html',
      theme: 'clouds',
      fontSize: 16,
      fontFamily: 'monospace',
      highlightActiveLine: true,
      highlightGutterLine: true,
      maxLines: 35,
      wrap: true,
      autoScrollEditorIntoView: true,
      showPrintMargin: false
    }
  }),

  watch: {
    value() {
      this.$emit('input', this.value)
    }
  },

  computed: {
    ...mapGetters('sites', ['site'])
  },

  methods: {
    closeDrawer() {
      this.$emit('close-previewer')
    },

    uploadChanged(file, fileList) {
      this.file = file.raw
    },

    uploadImage() {
      const formData = new FormData()
      formData.append('siteId', this.site.id)
      formData.append('type', 2)
      formData.append('file', this.file)

      api
        .uploadFile(formData)
        .then(res => {
          let position = this.$refs.editor.selection
          this.$refs.editor.exec(
            'insertHTML',
            `<img src=/static/${res.data}>`,
            position
          )
        })
        .catch(err => {
          showErrorNotify(err?.detail)
        })
    },

    beforeIconUpload(file) {
      const isImage = file.type === 'image/jpeg' || 'image/png'
      const isLimit2Mb = file.size / 1024 <= 2048

      if (!isImage) {
        showErrorNotify(this.$t('tabs.settings.imageFormatError'))
      }

      if (!isLimit2Mb) {
        showErrorNotify(
          this.$t('tabs.settings.iconSizeError', { size: '2 Mb' })
        )
      }

      return isImage && isLimit2Mb
    }
  }
}
</script>

<style lang="sass">
.el-upload
  border: none

.editr--toolbar
  margin-left: 35px

#image-btn
  position: absolute
  width: 36px
  height: 33px
  background: #f6f6f6
  border: 1px solid #e4e4e4
  border-right: none
  &:hover
    background: rgba(0,0,0,0.1)
  .vw-btn-image
    font-size: 1.1em
    width: 100%
    height: 100%
    position: absolute
    top: 1px
    padding-left: 2px
    display: flex !important
    justify-content: center
    align-items: center
    .icon
      color: #333333

.editr--content img
  max-width: 500px

.editr--content
  min-height: 583px !important
  max-height: 63vh

.el-drawer__header
  margin-bottom: 0

.el-drawer__body
  overflow-y: auto
  overflow: -moz-scrollbars-none
  -ms-overflow-style: none
  &::-webkit-scrollbar
    width: 0
</style>
