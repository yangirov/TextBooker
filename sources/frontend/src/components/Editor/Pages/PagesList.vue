<template>
  <div class="pages_list">
    <SelectList
      :items="pages"
      @handler="selectPage"
      class-name="max-wh m-1"
      id="pages_list"
    ></SelectList>

    <div class="buttons mt-1 ml-1">
      <el-button type="primary" @click="addPage">
        <i class="el-icon-circle-plus-outline"></i>
        {{ $t('common.add') }}
      </el-button>

      <el-popconfirm
        :title="$t('common.deleteWarning')"
        :confirmButtonText="$t('common.yes')"
        :cancelButtonText="$t('common.no')"
        @onConfirm="deletePage"
        icon="el-icon-info"
        iconColor="red"
      >
        <el-button slot="reference" type="danger">
          <i class="el-icon-remove-outline"></i>
          {{ $t('common.delete') }}
        </el-button>
      </el-popconfirm>
    </div>
  </div>
</template>

<script>
export default {
  name: 'PagesList',

  data() {
    return {
      selectedPage: 0,
      pages: [
        {
          name: `${this.$t('tabs.pages.defaultPageName')} 1`,
          alias: 'page1'
        }
      ]
    }
  },

  methods: {
    addPage() {
      let index = this.pages.length + 1
      let page = {
        name: `${this.$t('tabs.pages.defaultPageName')} ${index}`,
        alias: `page${index}`
      }
      this.pages.push(page)
    },

    deletePage() {
      this.$delete(this.pages, this.selectedPage)
    },

    selectPage(index) {
      this.selectedPage = index
    }
  }
}
</script>

<style lang="sass" scoped>
.buttons
  display: flex
  flex-wrap: nowrap
  justify-content: flex-start

.pages_list
  display: flex
  flex-direction: column
  justify-content: space-between
  height: 100%
</style>
