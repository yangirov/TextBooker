<template>
  <div class="columns">
    <div class="column is-3">
      <BlocksList />
    </div>

    <div class="column">
      <BlockEditor />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

import BlocksList from './BlocksList.vue'
import BlockEditor from './BlockEditor'

export default {
  name: 'Blocks',

  components: {
    BlocksList,
    BlockEditor
  },

  computed: {
    ...mapGetters('sites', ['site'])
  },

  created() {
    this.$store.dispatch('blocks/fetchBlocks', this.site.id)
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 100%
  .column
    display: flex
    flex-direction: column
    justify-content: space-between
</style>
