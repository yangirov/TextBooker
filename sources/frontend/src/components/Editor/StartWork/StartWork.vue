<template>
  <div class="tab center-block">
    <el-button type="success" size="large">
      <i class="el-icon-document-add"></i> {{ $t('tabs.start.createProject') }}
    </el-button>
    
    <el-button type="primary" size="large">
      <i class="el-icon-edit-outline"></i> {{ $t('tabs.start.openProject') }}
    </el-button>
  </div>
</template>

<script>
export default {
  name: 'StartWork'
}
</script>