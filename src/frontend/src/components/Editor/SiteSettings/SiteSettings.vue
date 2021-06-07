<template>
  <div class="columns">
    <div class="column is-half">
      <SiteOptions />
    </div>

    <div class="column is-half">
      <h3 class="tab-title">{{ $t('tabs.settings.templateSection') }}</h3>
      <SectionNames />
    </div>
  </div>
</template>

<script>
import SiteOptions from './SiteOptions.vue'
import SectionNames from './SectionNames.vue'

export default {
  components: {
    SiteOptions,
    SectionNames
  }
}
</script>
