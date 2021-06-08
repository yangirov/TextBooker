<template>
  <div class="project_list">
    <div class="titles">
      <h1 class="title mb-2">{{ $t('user.projects') }}</h1>
      <CreateSiteButton />
    </div>

    <el-table :data="sites" class="project_list">
      <el-table-column
        prop="updatedOn"
        :label="$t('common.updatedOn')"
        width="140px"
        :formatter="
          ({ updatedOn }) => $moment(updatedOn).format('DD.MM.YYYY HH:mm')
        "
      ></el-table-column>

      <el-table-column
        prop="title"
        :label="$t('common.title')"
        width="200px"
      ></el-table-column>

      <el-table-column
        prop="description"
        :label="$t('common.description')"
        width="300px"
      ></el-table-column>

      <el-table-column align="right">
        <template slot-scope="scope">
          <el-button
            size="mini"
            @click="handleEdit(scope.row)"
            icon="el-icon-edit"
          ></el-button>

          <el-popconfirm
            :title="$t('common.deleteWarning')"
            :confirmButtonText="$t('common.yes')"
            :cancelButtonText="$t('common.no')"
            @onConfirm="handleDelete(scope.row)"
            icon="el-icon-info"
            iconColor="red"
          >
            <el-button
              slot="reference"
              size="mini"
              type="danger"
              icon="el-icon-delete"
            ></el-button>
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import CreateSiteButton from '@/components/Editor/StartWork/CreateSiteButton.vue'

export default {
  data: () => ({
    loading: false
  }),

  components: {
    CreateSiteButton
  },

  computed: {
    ...mapGetters('sites', ['sites'])
  },

  methods: {
    handleEdit({ id }) {
      this.$store.dispatch('sites/fetchSiteInfo', id)
    },

    handleDelete({ id }) {
      this.$store.dispatch('sites/deleteSite', id)
    }
  },

  created() {
    this.$store.dispatch('sites/fetchSites')
  }
}
</script>

<style lang="sass" scoped>
.project_list
  width: 100%
  overflow: auto
  max-height: 70vh
  .titles
    display: flex
    justify-content: space-between
    align-items: baseline
</style>
