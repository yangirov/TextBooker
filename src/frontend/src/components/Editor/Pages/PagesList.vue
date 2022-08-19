<template>
  <div class="pages_list">
    <SelectList
      class-name="max-wh m-1"
      :default-active="defaultActive"
      :items="pages"
      @handler="selectPage"
      commit-name="pages/UPDATE_PAGES"
    ></SelectList>

    <div class="buttons mt-1">
      <el-button type="primary" @click="addPage">
        <i class="el-icon-circle-plus-outline"></i>
        {{ $t('common.add') }}
      </el-button>

      <el-popconfirm
        :title="$t('common.deleteWarning')"
        :confirmButtonText="$t('common.yes')"
        :cancelButtonText="$t('common.no')"
        @confirm="deletePage"
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
import { mapGetters } from 'vuex'
import SelectList from '@/components/Editor/SelectList/SelectList.vue'

export default {
  name: 'PagesList',

  components: {
    SelectList
  },

  data: () => ({
    defaultActive: ''
  }),

  computed: {
    ...mapGetters('sites', ['site']),
    ...mapGetters('pages', ['pages', 'page'])
  },

  methods: {
    addPage() {
      let index = this.pages.length + 1 ?? 1

      let data = {
        title: `${this.$t('tabs.pages.defaultPageName')} ${index}`,
        alias: `page${index}`,
        siteId: this.site.id
      }

      this.$store.dispatch('pages/addPage', data)
    },

    deletePage() {
      this.$store.dispatch('pages/deletePage', {
        id: this.page.id,
        siteId: this.site.id
      })
    },

    selectPage(id) {
      this.defaultActive = id
      let page = this.pages.find(x => x.id === id) ?? {}
      this.$store.commit('pages/SET_PAGE', page)
    }
  },

  mounted() {
    setTimeout(() => {
      this.selectPage(this.pages[0]?.id)
    }, 100)
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
