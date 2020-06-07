<template>
  <div class="columns">
    <div class="column is-3">
      <PagesList />
    </div>

    <div class="column">
      <el-tabs value="0">
        <el-tab-pane class="full-wh">
          <span slot="label">
            <i class="el-icon-edit"></i> {{ $t('tabs.pages.editor') }}
          </span>
          <PageEditor />
        </el-tab-pane>

        <el-tab-pane class="full-wh">
          <span slot="label">
            <i class="el-icon-setting"></i> {{ $t('tabs.pages.settings') }}
          </span>
          <PageSettings />
        </el-tab-pane>
      </el-tabs>
    </div>
  </div>
</template>

<script>
import PagesList from './PagesList.vue'
import PageEditor from './PageEditor.vue'
import PageSettings from './PageSettings.vue'

export default {
  name: 'Pages',

  components: {
    PagesList,
    PageEditor,
    PageSettings
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
