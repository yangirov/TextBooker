<template>
  <div class="columns">
    <div class="column is-3">
      <PagesList />
    </div>

    <div class="column">
      <el-tabs value="0">
        <el-tab-pane class="full-wh">
          <span slot="label">
            <i class="el-icon-edit"></i> {{ $t('tabs.pages.editor') }}
          </span>
          <PageEditor v-model="pageContent" />
        </el-tab-pane>

        <el-tab-pane class="full-wh">
          <span slot="label">
            <i class="el-icon-setting"></i> {{ $t('tabs.pages.settings') }}
          </span>
          <PageSettings v-model="pageSettings" />
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

import PagesList from './PagesList.vue'
import PageEditor from './PageEditor.vue'
import PageSettings from './PageSettings.vue'

export default {
  name: 'Pages',

  components: {
    PagesList,
    PageEditor,
    PageSettings
  },

  data: () => ({
    pageContent: {
      title: '',
      content: ''
    },

    pageSettings: {
      alias: '',
      description: '',
      keywords: '',
      inMainMenu: false,
      inAsideMenu: true
    }
  }),

  computed: {
    ...mapGetters('sites', ['site']),
    ...mapGetters('pages', ['page'])
  },

  watch: {
    page: {
      handler(newValue, oldValue) {
        this.pageHandler(newValue, oldValue)
      },
      deep: true
    },

    pageContent: {
      handler(newValue) {
        this.$store.commit('pages/SET_PAGE', newValue)
      },
      deep: true
    },

    pageSettings: {
      handler(newValue) {
        this.$store.commit('pages/SET_PAGE', newValue)
      },
      deep: true
    }
  },

  methods: {
    pageHandler: _.debounce(function(newValue, oldValue) {
      if (!_.isEmpty(newValue) && !_.isEqual(newValue, oldValue)) {
        let {
          id,
          title,
          content,
          alias,
          description,
          keywords,
          inMainMenu,
          inAsideMenu
        } = newValue

        this.pageContent = {
          id,
          title,
          content
        }

        this.pageSettings = {
          id,
          alias,
          description,
          keywords,
          inMainMenu,
          inAsideMenu
        }
      }
    }, 300)
  },

  created() {
    this.$store.dispatch('pages/fetchPages', this.site.id)
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 100%
  .column
    display: flex
    flex-direction: column
    justify-content: space-between
</style>
