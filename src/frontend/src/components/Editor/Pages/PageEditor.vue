<template>
  <div class="page_editor">
    <el-form :model="form" :rules="rules" ref="pageEditorForm" size="small">
      <el-form-item prop="title" id="page_title">
        <el-input v-model="title" :placeholder="$t('tabs.pages.title')">
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
      :style="style"
      :title="title"
      @change-content="changeContent"
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
    style: '',

    previewVisible: false,
    enabledHtmlMode: false,

    title: '',
    content: ''
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
    changeContent(data) {
      this.form.content = data
    },

    closePreview() {
      this.previewVisible = false
    },

    resizeHandler() {
      let menuHeight = document.getElementById('pages_list').clientHeight
      let inputWidth = document.getElementById('page_title').clientWidth

      this.style = `height: calc(${menuHeight}px - 7em);
                    width: calc(${inputWidth}px - 0.1em);`
    }
  },

  created() {
    window.addEventListener('resize', this.resizeHandler)
  },

  mounted() {
    setTimeout(() => {
      this.resizeHandler()
    }, 100)
  },

  destroyed() {
    window.removeEventListener('resize', this.resizeHandler)
  }
}
</script>
