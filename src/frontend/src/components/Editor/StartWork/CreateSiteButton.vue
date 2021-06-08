<template>
  <el-button
    type="success"
    icon="el-icon-document-add"
    @click="createSite"
    :size="size"
  >
    {{ $t('tabs.start.createProject') }}
  </el-button>
</template>

<script>
export default {
  props: {
    size: {
      type: String,
      default: 'medium'
    }
  },

  methods: {
    createSite() {
      let initState = {
        title: 'Site name',
        templateId: 1
      }
      this.$store.dispatch('sites/addSite', initState)
    }
  }
}
</script>
