<template>
  <header class="navbar header" role="navigation" aria-label="main navigation">
    <div class="container">
      <div class="navbar-brand">
        <router-link to="/" class="navbar-item logo" exact>
          <div class="logo">
            <img class="logo__img" src="@/assets/logo.svg" alt="Logo" />
            <span class="logo__text">{{ systemName }}</span>
          </div>
        </router-link>
      </div>

      <div class="navbar-menu">
        <div class="navbar-end">
          <div class="navbar-item">
            <div class="buttons">
              <UserNav v-if="isAuth" />

              <router-link class="button is-light" to="/signin" v-if="!isAuth">
                {{ $t('user.login') }}
              </router-link>

              <router-link
                class="button bold is-blue is-outlined"
                to="/signup"
                v-if="!isAuth"
              >
                {{ $t('user.register') }}
              </router-link>

              <LocaleChanger />
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { mapGetters } from 'vuex'
import LocaleChanger from './LocaleChanger.vue'
import UserNav from '@/components/User/UserNav.vue'

export default {
  name: 'PageHeader',

  components: {
    LocaleChanger,
    UserNav
  },

  computed: {
    ...mapGetters('appState', ['systemName']),
    ...mapGetters('user', ['isAuth'])
  }
}
</script>

<style lang="sass" scoped>
.logo
  display: flex
  align-items: center
  &__text
    margin-left: 0.3em
    font-size: 1.3em
</style>
