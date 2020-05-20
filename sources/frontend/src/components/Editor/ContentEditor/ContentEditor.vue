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

    <prism-editor
      v-if="htmlMode"
      v-model="content"
      language="html"
      lineNumbers="true"
      :style="style"
    ></prism-editor>
  </div>
</template>

<script>
import { lodash as _ } from '@/utils'

import PrismEditor from 'vue-prism-editor'

import { VueEditor, Quill } from 'vue2-editor'
import BlotFormatter from 'quill-blot-formatter'
import QuillPasteSmart from 'quill-paste-smart'

Quill.register('modules/clipboard', QuillPasteSmart, true)
Quill.register('modules/blotFormatter', BlotFormatter)

export default {
  components: {
    VueEditor,
    PrismEditor
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
                'span'
              ],
              attributes: ['href', 'rel', 'target', 'class']
            },
            keepSelection: true
          }
        }
      })
    }
  },

  data() {
    return {
      content: ''
    }
  },

  watch: {
    content(newValue) {
      this.handler(newValue)
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
