<template>
  <div class="site-actions buttons" v-if="show">
    <el-button type="primary">
      <i class="el-icon-view"></i> {{ $t('siteActions.view') }}
    </el-button>

    <el-button type="success">
      <i class="el-icon-video-play"></i> {{ $t('siteActions.generate') }}
    </el-button>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'SiteActions',

  computed: {
    ...mapGetters('sites', ['site']),

    show() {
      return this.site && this.$route.name == 'editor'
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
