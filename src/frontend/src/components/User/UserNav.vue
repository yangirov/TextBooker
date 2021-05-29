<template>
  <el-dropdown @command="handleCommand">
    <span class="el-dropdown-link">
      {{ shortUsername }} <i class="el-icon-arrow-down el-icon--right"></i>
    </span>

    <el-dropdown-menu slot="dropdown">
      <el-dropdown-item command="projects">
        <i class="el-icon-s-order"></i>
        <span>{{ $t('user.projects') }}</span>
      </el-dropdown-item>

      <el-divider></el-divider>

      <el-dropdown-item command="settings">
        <i class="el-icon-s-tools"></i>
        <span>{{ $t('user.settings') }}</span>
      </el-dropdown-item>

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
    ...mapGetters('user', ['user']),

    shortUsername() {
      return this.user.email
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
        .dispatch('user/logout')
        .then(() => this.$router.push('/'))
        .catch(err => console.log(err))
    }
  }
}
</script>

<style lang="sass" scoped>
.el-divider--horizontal
  margin: .5em 0 .5em 0

.el-dropdown-link
  cursor: pointer
  color: #409EFF
</style>
