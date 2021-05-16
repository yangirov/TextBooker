<template>
  <el-tabs type="border-card">
    <el-tab-pane label="Start">
      <span slot="label"><i class="el-icon-s-home"></i> Начало</span>
      <div class="tab center-block">
        <div class="">
          <el-button type="success" size="large"><i class="el-icon-document-add"></i> Создать проект</el-button>
          <el-button type="primary" size="large"><i class="el-icon-edit-outline"></i> Открыть проект</el-button>
        </div>
      </div>
    </el-tab-pane>

    <el-tab-pane label="Config">
      <span slot="label"><i class="el-icon-picture"></i> Шаблон</span>
      <div class="tab"><TemplateGallery /></div>
    </el-tab-pane>

    <el-tab-pane label="Settings">
      <span slot="label"><i class="el-icon-setting"></i> Параметры сайта</span>
      <div class="tab">Параметры сайта</div>
    </el-tab-pane>

    <el-tab-pane label="Pages">
      <span slot="label"><i class="el-icon-tickets"></i> Страницы</span>
      <div class="tab">Страницы</div>
    </el-tab-pane>

    <el-tab-pane label="Blocks">
      <span slot="label"><i class="el-icon-document-copy"></i> Блоки</span>
      <div class="tab">Блоки</div>
    </el-tab-pane>

    <el-tab-pane label="Publish">
      <span slot="label"><i class="el-icon-monitor"></i> Публикация</span>
      <div class="tab">Публикация</div>
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import {mapGetters} from 'vuex'
import TemplateGallery from '@/components/TemplateGallery/TemplateGallery.vue'

export default {
  name: 'Home',

  components: {
    TemplateGallery
  },

  computed: {
    ...mapGetters('appState', ['isAuth'])
  }
}
</script>

<style lang="scss">
// .el-tabs {
//   min-height: 80vh;
//   height: 100%;
//   max-height: 800px;
// }
.el-tabs__content {
  overflow: hidden;
  position: relative;
  min-height: 80vh;
  max-height: 80vh;
}
</style>