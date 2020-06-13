<template>
  <div class="site-options">
    <el-form
      ref="siteForm"
      :model="siteForm"
      label-width="300px"
      v-loading="loading"
      size="small"
    >
      <el-form-item :label="$t('tabs.settings.siteTitle')" prop="title">
        <el-input v-model="siteForm.title"></el-input>
      </el-form-item>

      <el-form-item :label="$t('tabs.settings.shortDescription')" prop="title">
        <el-input v-model="siteForm.description"></el-input>
      </el-form-item>

      <el-form-item :label="$t('tabs.settings.keywords')" prop="title">
        <el-input v-model="siteForm.keywords"></el-input>
      </el-form-item>

      <el-form-item :label="$t('tabs.settings.icon')" prop="title">
        <ImageUpload
          v-model="file"
          :upload="uploadIcon"
          :before-upload="beforeIconUpload"
          accept="image/png, image/x-icon"
        >
          <img
            v-if="siteForm.icon"
            :src="'/static/' + siteForm.icon"
            class="icon"
          />
          <i v-else class="el-icon-plus icon-uploader-icon"></i>
        </ImageUpload>
      </el-form-item>

      <el-form-item :label="$t('tabs.settings.userScripts')" prop="title">
        <el-checkbox v-model="siteForm.enabledUserScripts">
          {{
            siteForm.enabledUserScripts
              ? $t('common.enabled')
              : $t('common.disabled')
          }}
        </el-checkbox>
        <el-button
          class="ml-1"
          type="primary"
          icon="el-icon-edit"
          @click="openModal(row)"
          :disabled="!siteForm.enabledUserScripts"
        ></el-button>
      </el-form-item>
    </el-form>

    <UserScriptsModal />
  </div>
</template>

<script>
import { lodash as _ } from '@/utils'
import { mapGetters } from 'vuex'
import { showSuccessNotify, showErrorNotify } from '@/utils'
import { populateObject } from '@/utils'
import api from '@/api'

import ImageUpload from '@/components/Editor/ContentEditor/ImageUpload.vue'
import UserScriptsModal from './UserScripts/UserScriptsModal.vue'
import { USER_SCRIPTS_MODAL } from '@/store/modals'

let initState = {
  title: '',
  description: '',
  keywords: '',
  enabledUserScripts: false,
  templateId: 1,
  icon: '',
  userScripts: [],
  sectionNames: [],
  blocks: [],
  pages: []
}

export default {
  components: {
    UserScriptsModal,
    ImageUpload
  },

  data: () => ({
    file: {},
    siteForm: { ...initState },
    USER_SCRIPTS_MODAL
  }),

  watch: {
    siteForm: {
      handler(newValue) {
        this.handleData(newValue)
      },
      deep: true
    }
  },

  computed: {
    ...mapGetters('sites', ['site', 'loading'])
  },

  methods: {
    handleData: _.debounce(function(data) {
      this.$store.commit('sites/UPDATE_SITE', data)
    }, 1000),

    openModal(data) {
      this.$modal.open(USER_SCRIPTS_MODAL)
    },

    uploadIcon() {
      const formData = new FormData()
      formData.append('siteId', this.site.id)
      formData.append('type', 1)
      formData.append('file', this.file.raw)

      api
        .uploadFile(formData)
        .then(response => {
          this.siteForm.icon = response.data
        })
        .catch(err => {
          showErrorNotify(err?.detail)
        })
    },

    beforeIconUpload(file) {
      const isImage = file.type === 'image/x-icon' || 'image/png'
      const isLimit100Kb = file.size / 1024 <= 100

      if (!isImage) {
        showErrorNotify(this.$t('tabs.settings.iconFormatError'))
      }

      if (!isLimit100Kb) {
        showErrorNotify(
          this.$t('tabs.settings.iconSizeError', { size: '100 Kb' })
        )
      }

      return isImage && isLimit100Kb
    }
  },

  created() {
    this.siteForm = populateObject(this.site)
  }
}
</script>

<style lang="sass" scoped>
/deep/ .el-upload
  border: 1px dashed #d9d9d9
  border-radius: 3px
  cursor: pointer
  position: relative
  overflow: hidden
  height: 32px
  width: 32px
  display: flex
  justify-content: center
  align-items: center
  &:hover
    border-color: #409EFF

/deep/ .icon-uploader-icon
  font-size: 20px
  color: #8c939d
  width: 32px
  height: 32px
  line-height: 32px
  text-align: center
</style>
