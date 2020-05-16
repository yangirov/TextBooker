<template>
  <footer role="contentinfo">
    <div class="container">
      <div class="columns is-centered">
        <div class="column">
          <div class="columns is-vcentered">
            <div class="column is-2">
              <a href="/">TextBooker</a>
            </div>
            <div class="column is-8 text-center">
              © 2020
            </div>
            <div class="column is-2 text-center">
              {{ version }} | Github
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name: 'PageFooter',
  computed: {
    ...mapGetters('appState', ['version']),
  }
};
</script>

<style lang="scss" scoped>
footer {
  margin-top: auto;
  height: 30px;
}

.columns {
  padding: 0 5px;
}
</style>