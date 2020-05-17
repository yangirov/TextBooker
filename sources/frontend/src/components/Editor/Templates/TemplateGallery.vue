<template>
  <InnerContainer>
    <template #right-aside>
      <SelectList
        :items="templates"
        @handler="handleClick"
        class-name="full-height-select"
      ></SelectList>
    </template>

    <template #content>
      <div class="template-info" v-if="currentTemplate">
        <h2 class="title">{{ currentTemplate.name }}</h2>
        <p><b>{{ $t('tabs.template.name') }}</b>: <a :href="currentTemplate.authorurl" target="_blank" rel="nofollow noreferrer">{{ currentTemplate.author }}</a></p>
        <p><b>{{ $t('tabs.template.licence') }}</b>: {{ currentTemplate.license }}</p>
        <p class="mt-1">{{ currentTemplate.about }}</p>
      </div>
    </template>

    <template #left-aside>
      <div class="column is-3">
        <div class="template-image" v-if="currentTemplate">
          <img class="responsive-image" :src="currentTemplateImage()" :alt="currentTemplate.name">
        </div>
      </div>
    </template>
  </InnerContainer>
</template>

<script>
let templates = require('./templates.json')
import InnerContainer from '@/components/Layout/InnerContainer.vue'

export default {
  name: 'TemplateGallery',

  components: {
    InnerContainer
  },

  data() {
    return {
      templates,
      currentTemplate: templates[0],
    }
  },

  methods: {
    handleClick(item) {
      this.currentTemplate = item
    },

    currentTemplateImage() {
      var images = require.context('@/assets/templates/', false)
      return this.currentTemplate && images(`./${this.currentTemplate.name}.jpg`)
    }
  }
}
</script>