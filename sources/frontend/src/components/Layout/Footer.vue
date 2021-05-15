<template>
  <footer role="contentinfo" class="page-footer">
    <div class="overlay-fade">
      <span class="overlay-fade">{{ version }}</span>
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
