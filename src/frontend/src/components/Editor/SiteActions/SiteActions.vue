<template>
  <div class="site-actions buttons" v-if="show">
    <el-button type="primary" size="small" @click="openSite">
      <i class="el-icon-view"></i> {{ $t('siteActions.view') }}
    </el-button>

    <el-button type="success" size="small" @click="updateSite">
      <i class="el-icon-video-play"></i> {{ $t('siteActions.generate') }}
    </el-button>
  </div>
</template>

<script>
import { lodash as _ } from '@/utils'
import { mapGetters } from 'vuex'

var siteWindow = null

export default {
  name: 'SiteActions',

  computed: {
    ...mapGetters('sites', ['site']),
    ...mapGetters('pages', ['page']),

    show() {
      return (
        !_.isEmpty(this.site) && this.site.id && this.$route.name == 'editor'
      )
    }
  },

  methods: {
    openSite() {
      let siteUrl = `/sites/${this.site.id}/${this.page.alias}.html`

      if (
        siteWindow &&
        !siteWindow.closed &&
        siteWindow.location.pathname === siteUrl
      ) {
        siteWindow.location.reload(true)
      } else {
        siteWindow = window.open(siteUrl, '_blank')
      }
    },

    async updateSite() {
      this.$store.commit('appState/SET_STATE', { loading: true })

      await Promise.all([
        this.$store.dispatch('sites/updateSite'),
        this.$store.dispatch('pages/updatePages'),
        this.$store.dispatch('blocks/updateBlocks')
      ]).then(data => {
        this.$store.dispatch('sites/generateSite', this.site.id)
      })

      this.$store.commit('appState/SET_STATE', { loading: false })
    }
  }
}
</script>

<style lang="sass">
.site-actions.buttons
  justify-content: flex-end !important
  margin: 3px 0
  margin-right: -15px
</style>
