<template>
  <el-dropdown @command="handleCommand">
    <span class="avatar el-dropdown-link">{{ shortUsername }}</span>

    <el-dropdown-menu slot="dropdown">
      <el-dropdown-item command="projects">
        <i class="el-icon-s-order"></i>
        <span>{{ $t('user.projects') }}</span>
      </el-dropdown-item>

      <el-dropdown-item command="settings">
        <i class="el-icon-s-tools"></i>
        <span>{{ $t('user.settings') }}</span>
      </el-dropdown-item>

      <el-divider></el-divider>

      <el-dropdown-item command="logout">
        <i class="el-icon-right"></i>
        <span>{{ $t('user.logout') }}</span>
      </el-dropdown-item>
    </el-dropdown-menu>
  </el-dropdown>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  computed: {
    ...mapGetters('appState', ['user']),

    shortUsername() {
      return 'TU'
    }
  },

  methods: {
    async handleCommand(command) {
      switch (command) {
        case 'projects':
          this.$router.push('/user/projects')
          break
        case 'settings':
          this.$router.push('/user/settings')
          break
        case 'logout':
          await this.logout()
          break
      }
    },

    async logout() {
      await this.$store
        .dispatch('appState/logout')
        .then(() => this.$router.push('/'))
        .catch(err => console.log(err))
    }
  }
}
</script>

<style lang="sass" scoped>
.el-divider--horizontal
  margin: .5em 0 .5em 0

.avatar
  display: flex
  width: 2em
  height: 2em
  border-radius: 50%
  align-items: center
  justify-content: center
  text-align: center
  user-select: none
  background-color: #007bff
  color: #ffffff
  min-width: 2em
  min-height: 2em
</style>
