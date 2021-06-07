<template>
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
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    loading: false
  }),

  computed: {
    ...mapGetters('sites', ['sites'])
  },

  methods: {
    handleEdit({ id }) {
      this.$router.push({ name: 'editor', params: { id } })
    },
    handleDelete({ id }) {
      this.$store.dispatch('sites/deleteUserSite', id)
    }
  },

  created() {
    this.$store.dispatch('sites/fetchUserSites')
  }
}
</script>

<style lang="sass" scoped>
.project_list
  width: 100%
  overflow: auto
  max-height: 70vh
</style>
