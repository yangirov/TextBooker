<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-half">
      <p class="text-center" v-html="$t('user.confirm', { email: email })"></p>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    email() {
      return this.$route?.params?.email
    }
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 80%
  display: flex
  align-items: center
</style>
