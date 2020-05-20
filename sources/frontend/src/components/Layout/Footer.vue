<template>
  <footer role="contentinfo">
    <div class="container">
      <div class="columns is-centered">
        <div class="column">
          <div class="columns is-vcentered">
            <div class="column text-left">
              <router-link to="donate">{{ $t('common.donate') }}</router-link>
            </div>
            <div class="column text-right">
              © 2020
              <el-divider direction="vertical"></el-divider>
              {{ version }}
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

.github-icon
  width: 16px
  margin-bottom: -2px
  height: auto
</style>
