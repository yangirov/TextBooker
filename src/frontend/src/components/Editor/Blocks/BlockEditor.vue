<template>
  <div class="block_editor">
    <el-form
      :model="blockForm"
      :rules="rules"
      ref="blockEditorForm"
      size="small"
    >
      <el-form-item prop="title" id="block_title">
        <el-input
          v-model="blockForm.title"
          :placeholder="$t('tabs.blocks.title')"
        >
          <template #prepend>
            <el-tooltip
              effect="dark"
              :content="$t('tabs.blocks.description')"
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
      :title="blockForm.title"
      :text="blockForm.content"
      @change-content="changeContent"
      @close-previewer="closePreview"
    ></ContentEditor>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import ContentEditor from '@/components/Editor/ContentEditor/ContentEditor.vue'

export default {
  name: 'BlockEditor',

  components: {
    ContentEditor
  },

  data: () => ({
    style: '',

    previewVisible: false,
    enabledHtmlMode: false,

    blockForm: {
      title: '',
      content: ''
    }
  }),

  watch: {
    block: {
      handler(newValue, oldValue) {
        this.blockHandler(newValue, oldValue)
      },
      deep: true
    },

    blockForm: {
      handler(newValue) {
        this.$store.commit('blocks/UPDATE_BLOCKS', newValue)
        this.$store.commit('blocks/SET_BLOCK', newValue)
      },
      deep: true
    }
  },

  computed: {
    ...mapGetters('blocks', ['blocks', 'block']),

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
    blockHandler: _.debounce(function(newValue, oldValue) {
      if (!_.isEqual(newValue, oldValue)) {
        this.blockForm = newValue
      }
    }, 100),

    changeContent(data) {
      this.blockForm.content = data
    },

    closePreview() {
      this.previewVisible = false
    },

    resizeHandler() {
      let menuHeight = document.getElementById('blocks_list').clientHeight
      let inputWidth = document.getElementById('block_title').clientWidth

      this.style = `height: calc(${menuHeight}px - 4em);
                    width: calc(${inputWidth}px - 0.1em);`
    }
  },

  created() {
    window.addEventListener('resize', this.resizeHandler)
    this.blockForm = this.block
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
