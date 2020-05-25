<template>
  <section>
    <el-tabs type="card" value="0">
      <el-tab-pane class="full-wh" :lazy="true">
        <span slot="label">
          <i class="el-icon-s-home"></i> {{ $t('tabs.start.name') }}
        </span>
        <StartWork />
      </el-tab-pane>

      <el-tab-pane v-if="isAuth" class="full-wh" :lazy="true">
        <span slot="label">
          <i class="el-icon-picture-outline"></i> {{ $t('tabs.template.name') }}
        </span>
        <TemplateGallery />
      </el-tab-pane>

      <el-tab-pane v-if="isAuth" class="full-wh" :lazy="true">
        <span slot="label">
          <i class="el-icon-setting"></i> {{ $t('tabs.settings.name') }}
        </span>
        {{ $t('common.empty') }}
      </el-tab-pane>

      <el-tab-pane v-if="isAuth" class="full-wh" :lazy="true">
        <span slot="label">
          <i class="el-icon-tickets"></i> {{ $t('tabs.pages.name') }}
        </span>
        <Pages />
      </el-tab-pane>

      <el-tab-pane v-if="isAuth" class="full-wh" :lazy="true">
        <span slot="label">
          <i class="el-icon-document-copy"></i>{{ $t('tabs.blocks.name') }}
        </span>
        <Blocks />
      </el-tab-pane>

      <el-tab-pane v-if="isAuth" class="full-wh" :lazy="true">
        <span slot="label">
          <i class="el-icon-monitor"></i> {{ $t('tabs.publish.name') }}
        </span>
        <Publisher />
      </el-tab-pane>
    </el-tabs>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'

import StartWork from '@/components/Editor/StartWork/StartWork.vue'
import TemplateGallery from '@/components/Editor/Templates/TemplateGallery.vue'
import Pages from '@/components/Editor/Pages/Pages.vue'
import Blocks from '@/components/Editor/Blocks/Blocks.vue'
import Publisher from '@/components/Editor/Publisher/Publisher.vue'

export default {
  name: 'Editor',

  components: {
    StartWork,
    TemplateGallery,
    Pages,
    Blocks,
    Publisher
  },

  computed: {
    ...mapGetters('appState', ['isAuth'])
  },

  created() {
    if (!this.isAuth) this.$router.push('/signin')
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
