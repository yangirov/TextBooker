<template>
  <el-form
    ref="pageSettingsForm"
    :model="value"
    :rules="rules"
    label-width="300px"
    size="small"
  >
    <el-form-item :label="$t('tabs.pages.url')" prop="alias">
      <el-input v-model="value.alias" placeholder="index"></el-input>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.description')" prop="description">
      <el-input v-model="value.description"></el-input>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.keywords')" prop="keywords">
      <el-input v-model="value.keywords"></el-input>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.inMainMenu')" prop="inMainMenu">
      <el-checkbox v-model="value.inMainMenu"></el-checkbox>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.inAsideMenu')" prop="inAsideMenu">
      <el-checkbox v-model="value.inAsideMenu"></el-checkbox>
    </el-form-item>
  </el-form>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props: {
    value: {
      type: Object
    }
  },

  watch: {
    value() {
      this.$emit('input', this.value)
    }
  },

  computed: {
    ...mapGetters('pages', ['pages', 'page']),

    $form() {
      return this.$refs['pageSettingsForm']
    },

    rules() {
      let { pages: data, value: form } = this

      return {
        ...this.mapRules(
          ['alias'],
          [
            {
              validator: this.validationUniqueName({
                data,
                index: data.findIndex(x => x.id === this.page.id),
                form,
                key: 'alias'
              }),
              message: this.$t('tabs.pages.existsError')
            },
            {
              required: true,
              min: 5,
              max: 100,
              message: this.$t('tabs.pages.errorLength', { from: 5, to: 100 })
            }
          ]
        )
      }
    }
  }
}
</script>
