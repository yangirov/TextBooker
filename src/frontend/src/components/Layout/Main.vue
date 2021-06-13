<template>
  <main v-loading="loading">
    <div class="container mt-1">
      <router-view></router-view>
    </div>
  </main>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'PageMain',

  computed: {
    ...mapGetters('appState', ['loading'])
  }
}
</script>

<style lang="sass">
main
  height: 100%
</style>
