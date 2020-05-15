<template>
  <header role="banner" class="page-header">
    <router-link to="/" class="logo" exact>
      <div>{{ systemName }}</div>
    </router-link>

    <ul role="navigation" class="navigation" v-if="isAuth">
      <li class="navigation__item">
        <a class="navigation__link">
          <i class="fas fa-user-circle" aria-hidden="true"></i>
          <span>{{ getName(user.lastName, user.firstName, user.middleName).short }}</span>
        </a>
      </li>

      <li class="navigation__item">
        <a class="navigation__link" href="#" @click="logout()" role="menuitem">
          <i class="fas fa-power-off" aria-hidden="true"></i> Выйти
        </a>
      </li>
    </ul>
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

<style>
.el-badge__content.is-fixed {
  right: -5px;
}
</style>