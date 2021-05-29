<template>
  <div class="columns">
    <div class="column is-3">
      <SelectList
        :items="templates"
        @handler="handleClick"
        class-name="full-height-select"
      ></SelectList>
    </div>

    <div class="column">
      <div class="template-info mt-2" v-if="currentTemplate">
        <h2 class="title">{{ currentTemplate.name }}</h2>
        <p>
          <b>{{ $t('tabs.template.name') }}: </b>
          <a
            :href="currentTemplate.authorurl"
            target="_blank"
            rel="nofollow noreferrer"
          >
            {{ currentTemplate.author }}
          </a>
        </p>
        <p>
          <b>{{ $t('tabs.template.licence') }}:</b>
          {{ currentTemplate.license }}
        </p>
        <p class="mt-1">{{ currentTemplate.about }}</p>
      </div>
    </div>

    <div class="column is-3 mt-2 mr-1">
      <div class="template-image" v-if="currentTemplate">
        <img
          class="responsive-image"
          :src="currentTemplateImage()"
          :alt="currentTemplate.name"
        />
      </div>
    </div>
  </div>
</template>

<script>
let templates = require('./templates.json')

export default {
  name: 'TemplateGallery',

  data: () => ({
    templates,
    currentTemplate: templates[0]
  }),

  methods: {
    handleClick(index) {
      this.currentTemplate = this.templates[index]
    },

    currentTemplateImage() {
      var images = require.context('@/assets/templates/', false)
      return (
        this.currentTemplate && images(`./${this.currentTemplate.name}.jpg`)
      )
    }
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 100%
</style>
