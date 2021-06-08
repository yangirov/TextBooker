<template>
  <div class="tab center-block">
    <CreateSiteButton size="large" />

    <el-button
      type="primary"
      size="large"
      @click="$router.push('/user/projects')"
    >
      <i class="el-icon-edit-outline"></i> {{ $t('tabs.start.openProject') }}
    </el-button>
  </div>
</template>

<script>
import CreateSiteButton from './CreateSiteButton.vue'

export default {
  name: 'StartWork',

  components: {
    CreateSiteButton
  }
}
</script>
