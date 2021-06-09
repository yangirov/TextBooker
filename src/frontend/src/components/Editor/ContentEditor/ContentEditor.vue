<template>
  <div class="content_editor">
    <el-drawer
      :title="title ? title : $t('common.preview')"
      :visible="previewVisible"
      @close="closeDrawer"
    >
      <div v-html="content"></div>
    </el-drawer>

    <vue-editor
      v-if="!htmlMode"
      v-model="content"
      :editor-options="editorSettings"
      :editor-toolbar="customToolbar"
      :style="style"
    ></vue-editor>

    <VueAceEditor
      v-if="htmlMode"
      v-model="content"
      :options="aceOptions"
      id="ace-container"
      class="mt-1"
    ></VueAceEditor>
  </div>
</template>

<script>
import { lodash as _ } from '@/utils'

import VueAceEditor from './VueAceEditor.vue'

import { VueEditor, Quill } from 'vue2-editor'
import BlotFormatter from 'quill-blot-formatter'
import QuillPasteSmart from 'quill-paste-smart'

Quill.register('modules/clipboard', QuillPasteSmart, true)
Quill.register('modules/blotFormatter', BlotFormatter)

export default {
  components: {
    VueEditor,
    VueAceEditor
  },

  props: {
    style: {
      type: String,
      default: ''
    },
    title: {
      type: String,
      default: ''
    },
    text: {
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
    },
    customToolbar: {
      type: Array,
      default: () => [
        ['bold', 'italic', 'underline', 'strike'],
        ['blockquote', 'code-block'],

        [{ header: 1 }, { header: 2 }],
        [{ list: 'ordered' }, { list: 'bullet' }],
        [{ script: 'sub' }, { script: 'super' }],
        [{ indent: '-1' }, { indent: '+1' }],
        [{ direction: 'rtl' }],

        [{ size: ['small', false, 'large', 'huge'] }],
        [{ header: [1, 2, 3, 4, 5, 6, false] }],

        [{ color: [] }, { background: [] }],
        [{ font: [] }],
        [{ align: [] }],

        ['clean']
      ]
    },
    editorSettings: {
      type: Object,
      default: () => ({
        modules: {
          blotFormatter: {},
          clipboard: {
            allowed: {
              tags: [
                'a',
                'b',
                'strong',
                'u',
                's',
                'i',
                'p',
                'br',
                'ul',
                'ol',
                'li',
                'span',
                'script'
              ],
              attributes: ['href', 'rel', 'target', 'class', 'src']
            },
            keepSelection: true
          }
        }
      })
    }
  },

  data: () => ({
    content: '',

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
    content(newValue) {
      this.handler(newValue)
    },
    text(newValue) {
      this.content = newValue
    }
  },

  methods: {
    handler: _.debounce(function(data) {
      this.$emit('change-content', data)
    }, 150),

    closeDrawer() {
      this.$emit('close-previewer')
    }
  }
}
</script>

<style lang="sass">
.el-drawer__header
  margin-bottom: 0

.el-drawer__body
  overflow-y: auto
  overflow: -moz-scrollbars-none
  -ms-overflow-style: none
  &::-webkit-scrollbar
    width: 0
</style>
