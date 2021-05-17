<template>
  <div class="columns">
    <div class="column is-3">
      <BlocksMenu />
    </div>

    <div class="column">
      <BlockEditor />
    </div>
  </div>
</template>

<script>
import BlocksMenu from './BlocksMenu.vue'
import BlockEditor from './BlockEditor'

export default {
  name: 'Blocks',

  components: {
    BlocksMenu,
    BlockEditor
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 100%
</style>
