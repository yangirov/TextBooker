<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-half">
      <h1 class="title mb-2">{{ $t('user.projects') }}</h1>
      <ProjectList />
    </div>
  </div>
</template>

<script>
import ProjectList from '@/components/User/ProjectList.vue'

export default {
  components: {
    ProjectList
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 70%
  display: flex
</style>
