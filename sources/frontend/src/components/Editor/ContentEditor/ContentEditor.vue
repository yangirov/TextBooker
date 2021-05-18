<template>
  <div class="content_editor">
    <el-drawer :title="$t('common.preview')" :visible.sync="previewerVisible">
      <div v-html="content"></div>
    </el-drawer>

    <vue-editor
      v-if="!htmlMode"
      v-model="content"
      :editorOptions="editorSettings"
      :editorToolbar="customToolbar"
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

  data() {
    return {
      content: '',
      style: '',

      previewerVisible: false,
      htmlMode: false,

      customToolbar: [
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
      ],
      editorSettings: {
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
      }
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
    }, 150)
  }
}
</script>
