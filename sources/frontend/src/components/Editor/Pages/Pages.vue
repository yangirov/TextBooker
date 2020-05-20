<template>
  <div class="columns">
    <div class="column is-3">
      <PagesList />
    </div>

    <div class="column">
      <el-tabs value="1">
        <el-tab-pane class="full-wh">
          <span slot="label">
            <i class="el-icon-edit"></i> {{ $t('tabs.pages.editor') }}
          </span>
        </el-tab-pane>

        <el-tab-pane class="full-wh">
          <span slot="label">
            <i class="el-icon-setting"></i> {{ $t('tabs.pages.settings') }}
          </span>
        </el-tab-pane>
      </el-tabs>

      <SiteActions />
    </div>
  </div>
</template>

<script>
import SiteActions from '@/components/Editor/SiteActions/SiteActions.vue'
import PagesList from './PagesList.vue'

export default {
  name: 'Pages',

  components: {
    SiteActions,
    PagesList
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
