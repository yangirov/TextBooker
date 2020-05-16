<template>
  <el-tabs tab-position="left">
    <div class="columns">
      <div class="column">
        <el-menu default-active="0">
          <el-menu-item 
            v-for="(item, index) in templates" 
            :key="index" 
            :label="item.name"
            @click="handleClick(item)"
          >
            <span>{{ item.name }}</span>
          </el-menu-item>
        </el-menu>
      </div>

      <div class="column is-7">
        <div class="template-info" v-if="currentTemplate">
          <h2 class="title">{{ currentTemplate.name }}</h2>
          <p><b>Автор</b>: <a :href="currentTemplate.authorurl" target="_blank" rel="nofollow noreferrer">{{ currentTemplate.author }}</a></p>
          <p><b>Лицензия</b>: {{ currentTemplate.license }}</p>

          {{ currentTemplate.about }}
        </div>
      </div>

      <div class="column is-3">
        <div class="template-image" v-if="currentTemplate">
          <img class="responsive-image" :src="currentTemplateImage()" :alt="currentTemplate.name">
        </div>
      </div>
    </div>
  </el-tabs>
</template>

<script>
let templates = require('./templates.json').reverse()

export default {
  name: 'TemplateGallery',

  data() {
    return {
      templates,
      currentTemplate: templates[0],
    }
  },

  methods: {
    handleClick(item) {
      this.currentTemplate = item;
    },

    currentTemplateImage() {
      var images = require.context('@/assets/templates/', false)
      return this.currentTemplate && images(`./${this.currentTemplate.name}.jpg`)
    }
  }
}
</script>

<style lang="scss">
.el-menu {
  overflow-y: scroll;
  max-height: 77vh;
  min-width: 13em;
}
</style>