<template>
  <div class="block_editor">
    <el-form :model="form" :rules="rules" ref="blockEditorForm" size="small">
      <el-form-item prop="title">
        <el-input
          id="block_title"
          v-model="form.title"
          :placeholder="$t('tabs.blocks.title')"
        >
          <template #append>
            <div id="editorActions">
              <el-button icon="el-icon-view" @click="drawer = true">
                {{ $t('common.preview') }}
              </el-button>
              <el-button @click="enabledHtmlMode = !enabledHtmlMode">
                <i :class="editorButtonIcon"></i>
                {{
                  enabledHtmlMode
                    ? $t('common.wysiwygEditor')
                    : $t('common.htmlEditor')
                }}
              </el-button>
            </div>
          </template>
        </el-input>
      </el-form-item>
    </el-form>

    <ContentEditor @change-content="handler" />
  </div>
</template>

<script>
import ContentEditor from '@/components/Editor/ContentEditor/ContentEditor.vue'

let initState = {
  title: '',
  content: ''
}

export default {
  name: 'BlockEditor',

  components: {
    ContentEditor
  },

  data() {
    return {
      form: { ...initState }
    }
  },

  computed: {
    rules() {
      return {
        ...this.mapRules(['title'])
      }
    },

    editorButtonIcon() {
      return this.enabledHtmlMode ? 'el-icon-picture' : 'el-icon-edit-outline'
    }
  },

  methods: {
    handler(data) {
      this.form.content = data
    },

    calculateStyles(height, width) {
      this.style = `height: calc(${height}px - 7em); width: calc(${width}px + 2.7em);`
    },

    resizeHandler() {
      let menuHeight = document.getElementById('blocks_list').clientHeight
      let inputWidth = document.getElementById('block_title').clientWidth
      let actionsWidth = document.getElementById('editorActions').clientWidth
      this.calculateStyles(menuHeight, inputWidth + actionsWidth)
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
    window.removeEventListener('resize', this.myEventHandler)
  }
}
</script>
