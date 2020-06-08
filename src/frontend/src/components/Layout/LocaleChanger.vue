<template>
  <el-dropdown @command="changeLocale" class="pl-1">
    <span class="el-dropdown-link">
      {{ currentLocale.name }} <i class="el-icon-arrow-down el-icon--right"></i>
    </span>
    <el-dropdown-menu slot="dropdown">
      <el-dropdown-item
        v-for="(lang, i) in langs"
        :key="i"
        :command="lang.short"
        :disabled="lang.short === $i18n.locale"
      >
        {{ lang.flag }} {{ lang.name }}
      </el-dropdown-item>
    </el-dropdown-menu>
  </el-dropdown>
</template>

<script>
import langs from './langs'

export default {
  name: 'LocaleChanger',
  data: () => ({
    langs
  }),

  computed: {
    currentLocale() {
      return this.langs.find(x => x.short === this.$i18n.locale)
    }
  },

  methods: {
    changeLocale(lang) {
      this.$i18n.locale = lang
      this.$root.$emit('locale')
    }
  }
}
</script>
