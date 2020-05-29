<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-half">
      <h1 class="title mb-2">{{ $t('user.settings') }}</h1>
      <UserSettings />
    </div>
  </div>
</template>

<script>
import UserSettings from '@/components/User/UserSettings.vue'

export default {
  components: {
    UserSettings
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 70%
  display: flex
</style>
