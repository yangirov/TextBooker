<template>
  <el-form
    ref="keysForm"
    :model="keysForm"
    label-width="250px"
    v-loading="loading"
  >
    <el-form-item
      :label="labelKey(item.id)"
      v-for="item in templateKeys"
      :key="item.templateKeyId"
    >
      <el-input
        v-model="keysForm[item.id]"
        :value="item.content"
        size="small"
        clearable
      ></el-input>
    </el-form-item>
  </el-form>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    keysForm: {}
  }),

  computed: {
    ...mapGetters('sites', ['site', 'templateKeys', 'loading']),

    form() {
      return Object.keys(keysForm).reduce((acc, cur) => {
        let item = {
          templateKeyId: cur,
          content: this.keysForm[cur],
          siteId: this.site.id
        }

        acc.push(item)
        return acc
      }, [])
    }
  },

  methods: {
    labelKey(id) {
      return this.$t('tabs.settings.templateFields')[id]
    }
  },

  created() {
    this.$store.dispatch('sites/fetchTemplateKeys')

    this.keysForm =
      this.site &&
      this.site.sectionNames.reduce((acc, { templateKeyId, content }) => {
        acc[templateKeyId] = content
        return acc
      }, {})
  }
}
</script>
