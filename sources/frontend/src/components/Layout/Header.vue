<template>
  <header class="navbar header" role="navigation" aria-label="main navigation" id="header">
    <div class="container">
      <div class="navbar-brand">
        <router-link to="/" class="navbar-item logo" exact>
          <div class="logo">
            <img class= "logo__img" src="@/assets/logo.svg" alt="Logo">
            <span class="logo__text">TextBooker</span>
          </div>
        </router-link>
      </div>

      <div id="nav-menu" class="navbar-menu">
        <div class="navbar-end">
          <div class="navbar-item">
            <div class="buttons">
              <router-link class="navbar-item" to="help"><i class="el-icon-question"></i> Руководство</router-link>
                <!-- <span>{{ getName(user.lastName, user.firstName, user.middleName).short }}</span> -->
              <router-link class="button is-light" id="login-button" to="login"> Войти </router-link>
              <router-link class="button bold is-blue is-outlined" id="get-started" to="signup"> Начать </router-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name: 'PageHeader',
  computed: {
    ...mapGetters('appState', ['isAuth', 'systemName', 'user']),
  },
  methods: {
    async logout() {
      await this.$store.dispatch('appState/logout')
        .then(() => this.$router.push('/'))
        .catch(err => console.log(err))
    }
  }
};
</script>

<style lang="scss">
.logo {
  display: flex;
  align-items: center;

  &__text {
    margin-left: 0.3em;
    font-size: 1.3em;
  }
}
.navbar-menu {
  display: block !important;
}
</style>