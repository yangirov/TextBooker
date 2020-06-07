<template>
  <el-form ref="pageForm" :model="pageForm" label-width="300px" size="small">
    <el-form-item :label="$t('tabs.pages.url')" prop="title">
      <el-input v-model="pageForm.url" placeholder="index.html"></el-input>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.title')" prop="title">
      <el-input v-model="pageForm.title"></el-input>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.description')" prop="title">
      <el-input v-model="pageForm.description"></el-input>
    </el-form-item>

    <el-form-item :label="$t('tabs.pages.keywords')" prop="title">
      <el-input v-model="pageForm.keywords"></el-input>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  data: () => ({
    pageForm: {}
  })
}
</script>
