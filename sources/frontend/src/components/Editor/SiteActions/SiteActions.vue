<template>
  <div class="buttons">
    <el-button type="buttons">
      <i class="el-icon-view"></i> {{ $t('siteActions.view') }}
    </el-button>

    <el-button type="success">
      <i class="el-icon-video-play"></i> {{ $t('siteActions.generate') }}
    </el-button>
  </div>
</template>

<script>
export default {
  name: 'SiteActions',

  data() {
    return {}
  }
}
</script>

<style lang="sass" scoped>
.buttons
  margin-left: auto
  margin-right: 1em
</style>
