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

export default {
  name: 'SiteActions',

  computed: {
    ...mapGetters('sites', ['site']),

    show() {
      return (
        !_.isEmpty(this.site) && this.site.id && this.$route.name == 'editor'
      )
    }
  },

  methods: {
    async openSite() {
      await Promise.all([this.updateSite()]).then(res => {
        window.open(`/static/${this.site.id}/index.html`, '_blank')
      })
    },

    async updateSite() {
      this.$store.commit('appState/SET_STATE', { loading: true })

      await Promise.all([
        this.$store.dispatch('sites/updateSite'),
        this.$store.dispatch('pages/updatePages'),
        this.$store.dispatch('blocks/updateBlocks')
      ])
        .then(data =>
          this.$store.commit('appState/SET_STATE', { loading: false })
        )
        .catch(err =>
          this.$store.commit('appState/SET_STATE', { loading: false })
        )
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
