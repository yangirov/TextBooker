<template>
  <div class="columns">
    <div class="column is-3">
      <el-input
        :placeholder="$t('common.search')"
        class="mb-1"
        size="small"
        v-model="searchText"
        clearable
      >
      </el-input>

      <SelectList
        class="template-list"
        :items="templatesData"
        :default-index="templateIndex"
        @handler="handleClick"
      ></SelectList>
    </div>

    <div class="column">
      <div class="template-info" v-if="currentTemplate">
        <h2 class="tab-title">{{ currentTemplate.name }}</h2>
        <p>
          <b>{{ $t('tabs.template.author') }}: </b>
          <a
            :href="currentTemplate.authorUrl"
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
import { mapGetters } from 'vuex'
import SelectList from '@/components/SelectList/SelectList.vue'

export default {
  name: 'TemplateGallery',

  components: {
    SelectList
  },

  data: () => ({
    searchText: '',
    currentTemplate: {}
  }),

  computed: {
    ...mapGetters('sites', ['site', 'templates']),

    templatesData() {
      return this.templates.filter(item => {
        return (
          item.name.toLowerCase().indexOf(this.searchText.toLowerCase()) > -1
        )
      })
    },

    templateIndex() {
      return (
        this.currentTemplate &&
        this.templates.findIndex(x => x.id == this.currentTemplate.id)
      )
    }
  },

  methods: {
    handleClick(templateId) {
      this.currentTemplate = this.templates[templateId]

      this.$store.commit('sites/UPDATE_SITE', {
        templateId: this.currentTemplate.id
      })
    },

    currentTemplateImage() {
      var images = require.context('@/assets/templates/', false)
      return (
        this.currentTemplate && images(`./${this.currentTemplate.name}.jpg`)
      )
    }
  },

  created() {
    this.$store.dispatch('sites/fetchTemplates')

    let templateIndex =
      (this.site &&
        this.templates.findIndex(x => x.id == this.site.templateId)) ??
      0

    this.handleClick(templateIndex)
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 100%

.template-list
  height: 95% !important

.template-image
  text-align: right
  img
    max-width: 350px
</style>
