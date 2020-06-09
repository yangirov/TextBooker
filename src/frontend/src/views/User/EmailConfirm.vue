<template>
  <div class="columns is-mobile is-centered" v-loading="loading">
    <div class="column is-half" v-if="!loading">
      {{ $t('status.success') }}
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  computed: {
    ...mapGetters('user', ['loading'])
  },

  created() {
    let { email, token } = this.$route.query
    this.$store.dispatch('user/confirmEmail', { email, token })
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 80%
  display: flex
  align-items: center
</style>
