<template>
  <div class="columns">
    <div class="column is-3">
      <BlocksMenu />
    </div>

    <div class="column"></div>
  </div>
</template>

<script>
import BlocksMenu from './BlocksMenu.vue'

export default {
  name: 'Blocks',

  components: {
    BlocksMenu
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 100%
</style>
