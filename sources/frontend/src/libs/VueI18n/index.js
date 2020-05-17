import Vue from 'vue'
import VueI18n from 'vue-i18n'

import elementEnLocale from 'element-ui/lib/locale/lang/en'
import elementRuLocale from 'element-ui/lib/locale/lang/ru-RU'

import { ENGLISH_TRANSLATIONS } from '@/translations/en'
import { RUSSIAN_TRANSLATIONS } from '@/translations/ru'

Vue.use(VueI18n)

const messages = {
  en: {
    ...ENGLISH_TRANSLATIONS,
    ...elementEnLocale
  },
  ru: {
    ...RUSSIAN_TRANSLATIONS,
    ...elementRuLocale
  }
}

export default new VueI18n({
  locale: process.env.VUE_APP_I18N_LOCALE || 'ru',
  fallbackLocale: process.env.VUE_APP_I18N_FALLBACK_LOCALE || 'en',
  silentTranslationWarn: true,
  messages
})
