<template>
  <div class="block_editor">
    <el-form :model="form" :rules="rules" ref="blockEditorForm" size="small">
      <el-form-item prop="title">
        <el-input
          v-model="form.title"
          :placeholder="$t('tabs.blocks.title')"
        ></el-input>
      </el-form-item>

      <el-form-item>
        <vue-editor v-model="form.content" :style="height"></vue-editor>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { VueEditor, Quill } from 'vue2-editor'

let initState = {
  title: '',
  content: ''
}

export default {
  name: 'BlockEditor',

  components: {
    VueEditor
  },

  data() {
    return {
      form: { ...initState },
      height: ''
    }
  },

  computed: {
    rules() {
      return {
        ...this.mapRules(['title'])
      }
    }
  },

  methods: {
    heightCss(value) {
      this.height = `height: ${value}px;`
    }
  },

  mounted() {
    setTimeout(() => {
      let menuHeight = document.getElementById('blocks_list').offsetHeight - 115
      this.heightCss(menuHeight)
    }, 100)
  }
}
</script>

<style lang="sass">
#editor
  height: 500px
</style>
