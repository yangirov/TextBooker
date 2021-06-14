<template>
  <div>
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
          v-model="keysForm[item.id].content"
          :value="item.content"
          size="small"
          clearable
        ></el-input>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { lodash as _ } from '@/utils'
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    keysForm: {}
  }),

  watch: {
    keysForm: {
      handler(newValue) {
        this.handleData()
      },
      deep: true
    }
  },

  computed: {
    ...mapGetters('sites', ['site', 'templateKeys', 'loading']),

    form() {
      return Object.values(this.keysForm).reduce((acc, item) => {
        acc.push(item)
        return acc
      }, [])
    }
  },

  methods: {
    handleData: _.debounce(function() {
      this.$store.commit('sites/UPDATE_SITE', { sectionNames: this.form })
    }, 1000),

    labelKey(id) {
      return this.$t('tabs.settings.templateFields')[id]
    }
  },

  created() {
    this.$store.dispatch('sites/fetchTemplateKeys')

    this.keysForm = this.templateKeys.reduce((acc, { id }) => {
      acc[id] = this.site.sectionNames.find(x => x.templateKeyId === id) ?? {
        templateKeyId: id,
        content: '',
        siteId: this.site.id
      }
      return acc
    }, {})
  }
}
</script>
