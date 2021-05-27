<template>
  <footer role="contentinfo">
    <div class="container">
      <div class="columns is-centered">
        <div class="column">
          <div class="columns is-vcentered">
            <div class="column text-left footer-left">
              © 2020
              <el-divider direction="vertical"></el-divider>
              <span>{{ version }}</span>
              <el-divider direction="vertical"></el-divider>
              <span>{{ $t('common.metrics') }}</span>
            </div>

            <div class="column text-center">
              <nav class="footer-menu">
                <router-link to="about">
                  {{ $t('about.shortTitle') }}
                </router-link>
                <el-divider direction="vertical"></el-divider>
                <a href="help" target="_blank" rel="nofollow noreferrer">
                  {{ $t('common.help') }}
                </a>
                <el-divider direction="vertical"></el-divider>
                <router-link to="feedback">
                  {{ $t('feedback.title') }}
                </router-link>
              </nav>
            </div>

            <div class="column text-right">
              <span>Coded with ❤️</span>
              <el-divider direction="vertical"></el-divider>
              <a
                href="https://github.com/yangirov/TextBooker"
                target="_blank"
                rel="nofollow noreferrer"
              >
                <img class="github-icon" src="@/assets/github.png" alt="Icon" />
                Github
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'PageFooter',
  computed: {
    ...mapGetters('appState', ['version'])
  }
}
</script>

<style lang="sass" scoped>
footer
  margin-top: auto
  margin-bottom: 10px
  height: 30px
  font-size: .9em

.footer-menu
  a
    color: rgba(0, 0, 0, 0.7)

.github-icon
  width: 16px
  margin-bottom: -2px
  height: auto

.footer-left
  color: #7e8184
</style>
