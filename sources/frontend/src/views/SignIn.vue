<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-4">
      <h1 class="title mb-2">{{ $t('user.login') }}</h1>
      <SignForm type="login" />
    </div>
  </div>
</template>

<script>
import SignForm from '@/components/User/SignForm.vue'

export default {
  name: 'SignIn',

  components: {
    SignForm
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 70%
  display: flex
  align-items: center
</style>
