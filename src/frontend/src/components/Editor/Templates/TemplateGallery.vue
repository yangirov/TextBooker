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
        :default-active="currentTemplate.id"
        :items="templatesData"
        @handler="selectTemplate"
        :draggable="false"
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
import SelectList from '@/components/Editor/SelectList/SelectList.vue'

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
      return this.templates
        .map(x => {
          return { ...x, title: x.name }
        })
        .filter(item => {
          return (
            item.name.toLowerCase().indexOf(this.searchText.toLowerCase()) > -1
          )
        })
    }
  },

  methods: {
    selectTemplate(templateId) {
      this.currentTemplate = this.templates.find(x => x.id == templateId)
      this.$store.commit('sites/UPDATE_SITE', { templateId })
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
    this.selectTemplate(this.site?.templateId ?? 1)
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
