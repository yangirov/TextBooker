<template>
  <div class="content_editor">
    <el-drawer
      :title="title ? title : $t('common.preview')"
      :visible="previewVisible"
      @close="closeDrawer"
    >
      <div v-html="value"></div>
    </el-drawer>

    <wysiwyg v-if="!htmlMode" v-model="value" />

    <VueAceEditor
      v-if="htmlMode"
      v-model="value"
      :options="aceOptions"
      class="mt-1"
    ></VueAceEditor>
  </div>
</template>

<script>
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

  methods: {
    closeDrawer() {
      this.$emit('close-previewer')
    }
  }
}
</script>

<style lang="sass">
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
