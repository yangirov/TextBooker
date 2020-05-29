<template>
  <div class="columns">
    <div class="column is-3">
      <BlocksList />
    </div>

    <div class="column">
      <BlockEditor />
      <SiteActions />
    </div>
  </div>
</template>

<script>
import SiteActions from '@/components/Editor/SiteActions/SiteActions.vue'
import BlocksList from './BlocksList.vue'
import BlockEditor from './BlockEditor'

export default {
  name: 'Blocks',

  components: {
    SiteActions,
    BlocksList,
    BlockEditor
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
