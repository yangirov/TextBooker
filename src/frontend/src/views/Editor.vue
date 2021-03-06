<template>
  <section>
    <el-tabs type="card" :value="activeTab">
      <el-tab-pane class="full-wh" :lazy="true" name="start">
        <span slot="label">
          <i class="el-icon-s-home"></i> {{ $t('tabs.start.name') }}
        </span>
        <StartWork />
      </el-tab-pane>

      <el-tab-pane
        v-if="siteExists"
        class="full-wh"
        :lazy="true"
        name="templates"
      >
        <span slot="label">
          <i class="el-icon-picture-outline"></i> {{ $t('tabs.template.name') }}
        </span>
        <TemplateGallery />
      </el-tab-pane>

      <el-tab-pane
        v-if="siteExists"
        class="full-wh"
        :lazy="true"
        name="settings"
      >
        <span slot="label">
          <i class="el-icon-setting"></i> {{ $t('tabs.settings.name') }}
        </span>
        <SiteSettings />
      </el-tab-pane>

      <el-tab-pane v-if="siteExists" class="full-wh" :lazy="true" name="pages">
        <span slot="label">
          <i class="el-icon-tickets"></i> {{ $t('tabs.pages.name') }}
        </span>
        <Pages />
      </el-tab-pane>

      <el-tab-pane v-if="siteExists" class="full-wh" :lazy="true" name="blocks">
        <span slot="label">
          <i class="el-icon-document-copy"></i> {{ $t('tabs.blocks.name') }}
        </span>
        <Blocks />
      </el-tab-pane>

      <el-tab-pane v-if="siteExists" class="full-wh" :lazy="true" name="deploy">
        <span slot="label">
          <i class="el-icon-monitor"></i> {{ $t('tabs.publish.name') }}
        </span>
        <Publisher />
      </el-tab-pane>

      <el-tab-pane
        v-if="siteExists && $route.name == 'editor'"
        :disabled="true"
        :lazy="true"
        name="actions"
      >
        <span slot="label">
          <SiteActions />
        </span>
      </el-tab-pane>
    </el-tabs>
  </section>
</template>

<script>
import { mapGetters } from 'vuex'
import { lodash as _ } from '@/utils'

import StartWork from '@/components/Editor/StartWork/StartWork.vue'
import TemplateGallery from '@/components/Editor/Templates/TemplateGallery.vue'
import SiteSettings from '@/components/Editor/SiteSettings/SiteSettings.vue'
import Pages from '@/components/Editor/Pages/Pages.vue'
import Blocks from '@/components/Editor/Blocks/Blocks.vue'
import Publisher from '@/components/Editor/Publisher/Publisher.vue'
import SiteActions from '@/components/Editor/SiteActions/SiteActions.vue'

export default {
  name: 'Editor',

  components: {
    StartWork,
    TemplateGallery,
    SiteSettings,
    Pages,
    Blocks,
    Publisher,
    SiteActions
  },

  computed: {
    ...mapGetters('sites', ['site']),

    activeTab() {
      if (this.siteExists) return 'settings'
      return 'start'
    },

    siteExists() {
      return !_.isEmpty(this.site) && this.site.id
    }
  }
}
</script>

<style lang="sass">
.el-tabs__content
  overflow: hidden
  position: relative
  min-height: 80vh
  max-height: 80vh

.el-tabs__nav
  display: flex !important
  width: 100% !important

#tab-actions
  flex-grow: 1
</style>
