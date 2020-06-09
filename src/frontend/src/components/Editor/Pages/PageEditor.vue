<template>
  <div class="page_editor">
    <el-form :model="siteForm" :rules="rules" ref="pageEditorForm" size="small">
      <el-form-item prop="title">
        <el-input
          v-model="siteForm.title"
          :placeholder="$t('tabs.pages.title')"
        >
          <template #prepend>
            <el-tooltip
              effect="dark"
              :content="$t('tabs.pages.description')"
              placement="left-start"
            >
              <el-button icon="el-icon-question"></el-button>
            </el-tooltip>
          </template>

          <template #append>
            <el-button icon="el-icon-view" @click="previewVisible = true">
              {{ $t('common.preview') }}
            </el-button>

            <el-button @click="enabledHtmlMode = !enabledHtmlMode">
              <i :class="editorButtonIcon"></i>
              {{ editorButtonText }}
            </el-button>
          </template>
        </el-input>
      </el-form-item>
    </el-form>

    <ContentEditor
      :html-mode="enabledHtmlMode"
      :preview-visible="previewVisible"
      :title="siteForm.title"
      v-model="siteForm.content"
      @close-previewer="closePreview"
    ></ContentEditor>
  </div>
</template>

<script>
import ContentEditor from '@/components/Editor/ContentEditor/ContentEditor.vue'

export default {
  name: 'PageEditor',

  components: {
    ContentEditor
  },

  data: () => ({
    previewVisible: false,
    enabledHtmlMode: false,

    siteForm: {
      title: '',
      content: ''
    }
  }),

  computed: {
    rules() {
      return {
        ...this.mapRules(['title'])
      }
    },

    editorButtonIcon() {
      return this.enabledHtmlMode ? 'el-icon-picture' : 'el-icon-edit-outline'
    },

    editorButtonText() {
      return this.enabledHtmlMode
        ? this.$t('common.wysiwygEditor')
        : this.$t('common.htmlEditor')
    }
  },

  methods: {
    closePreview() {
      this.previewVisible = false
    }
  },

  created() {}
}
</script>
