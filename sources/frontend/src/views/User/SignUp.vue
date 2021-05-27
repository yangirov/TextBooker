<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-4">
      <h1 class="title mb-2">{{ $t('user.register') }}</h1>
      <SignForm action="register" />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import SignForm from '@/components/User/SignForm.vue'

export default {
  name: 'SignUp',

  components: {
    SignForm
  },

  computed: {
    ...mapGetters('user', ['isAuth'])
  },

  created() {
    if (this.isAuth) this.$router.push('/editor')
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 70%
  display: flex
  align-items: center
</style>
