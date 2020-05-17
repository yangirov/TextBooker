<template>
  <el-tabs type="border-card" value="0">
    <el-tab-pane label="Start" class="full-wh">
      <span slot="label">
        <i class="el-icon-s-home"></i> {{ $t('tabs.start.name') }}
      </span>
      <StartWork />
    </el-tab-pane>

    <el-tab-pane label="Templates" v-if="isAuth" class="full-wh">
      <span slot="label">
        <i class="el-icon-picture"></i> {{ $t('tabs.template.name') }}
      </span>
      <TemplateGallery />
    </el-tab-pane>

    <el-tab-pane label="Settings" v-if="isAuth" class="full-wh">
      <span slot="label">
        <i class="el-icon-setting"></i> {{ $t('tabs.settings.name') }}
      </span>
      {{ $t('common.empty') }}
    </el-tab-pane>

    <el-tab-pane label="Pages" v-if="isAuth" class="full-wh">
      <span slot="label">
        <i class="el-icon-tickets"></i> {{ $t('tabs.pages.name') }}
      </span>
      {{ $t('common.empty') }}
    </el-tab-pane>

    <el-tab-pane label="Blocks" v-if="isAuth" class="full-wh">
      <span slot="label">
        <i class="el-icon-document-copy"></i>{{ $t('tabs.blocks.name') }}
      </span>
      <Blocks />
    </el-tab-pane>

    <el-tab-pane label="Publish" v-if="isAuth" class="full-wh">
      <span slot="label">
        <i class="el-icon-monitor"></i> {{ $t('tabs.publish.name') }}
      </span>
      {{ $t('common.empty') }}
    </el-tab-pane>
  </el-tabs>
</template>

<script>
import { mapGetters } from 'vuex'
import StartWork from '@/components/Editor/StartWork/StartWork.vue'
import TemplateGallery from '@/components/Editor/Templates/TemplateGallery.vue'
import Blocks from '@/components/Editor/Blocks/Blocks.vue'

export default {
  name: 'Editor',

  components: {
    StartWork,
    TemplateGallery,
    Blocks
  },

  computed: {
    ...mapGetters('appState', ['isAuth'])
  }
}
</script>

<style lang="sass">
.el-tabs__content
  overflow: hidden
  position: relative
  min-height: 80vh
  max-height: 80vh
</style>
