<template>
  <div class="page_editor">
    <el-form :model="value" :rules="rules" size="small">
      <el-form-item prop="title">
        <el-input v-model="value.title" :placeholder="$t('tabs.pages.title')">
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
      :title="value.title"
      v-model="value.content"
      @close-previewer="closePreview"
    ></ContentEditor>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import ContentEditor from '@/components/Editor/ContentEditor/ContentEditor.vue'

export default {
  name: 'PageEditor',

  components: {
    ContentEditor
  },

  props: {
    value: {
      type: Object
    }
  },

  watch: {
    value() {
      this.$emit('input', this.value)
    }
  },

  data: () => ({
    previewVisible: false,
    enabledHtmlMode: false
  }),

  computed: {
    ...mapGetters('pages', ['pages', 'page']),

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
  }
}
</script>
