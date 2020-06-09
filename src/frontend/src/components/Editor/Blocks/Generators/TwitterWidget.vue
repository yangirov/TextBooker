<template>
  <el-dialog
    :visible="$modal.isVisible(TWITTER_MODAL)"
    width="800px"
    top="5vh"
    :close-on-click-modal="true"
    :before-close="closeModal"
  >
    <div slot="title" class="twitter_title">
      <h3>{{ $t('twitter.feedTitle') }}</h3>
      <h3>{{ $t('common.preview') }}</h3>
    </div>

    <el-form
      ref="twitterForm"
      :model="twitterForm"
      :rules="rules"
      label-width="220px"
      @submit.native.prevent="onSubmit"
      size="small"
    >
      <div class="columns">
        <div class="column is-half">
          <el-form-item prop="url" :label="$t('twitter.url')">
            <el-input
              placeholder="https://twitter.com/twitter"
              v-model="twitterForm.url"
              clearable
            >
            </el-input>
          </el-form-item>

          <el-form-item prop="width" :label="$t('common.width')">
            <el-input v-model="twitterForm.width" type="number" clearable>
            </el-input>
          </el-form-item>

          <el-form-item prop="height" :label="$t('common.height')">
            <el-input
              v-model="twitterForm.height"
              type="number"
              :min="220"
              clearable
            >
            </el-input>
          </el-form-item>

          <el-form-item prop="lang" :label="$t('common.lang')">
            <el-select v-model="twitterForm.lang">
              <el-option
                v-for="item in langs"
                :key="item.short"
                :label="item.name"
                :value="item.short"
              >
              </el-option>
            </el-select>
          </el-form-item>

          <el-form-item prop="theme" :label="$t('common.theme')">
            <el-select v-model="twitterForm.theme">
              <el-option
                v-for="(item, index) in themes"
                :key="index"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
        </div>

        <div class="column is-half">
          <div v-html="preview"></div>
        </div>
      </div>

      <footer class="margin-top-30 text-right">
        <el-button @click="closeModal">
          {{ $t('common.close') }}
        </el-button>

        <el-button type="success" native-type="submit">
          {{ $t('common.add') }}
        </el-button>
      </footer>
    </el-form>
  </el-dialog>
</template>

<script>
import { lodash as _ } from '@/utils'
import langs from '@/api/langs'
import { TWITTER_MODAL } from '@/store/modals'

const twitterWidgetJs = 'https://platform.twitter.com/widgets.js'

export default {
  data: () => ({
    twitterForm: {
      url: 'https://twitter.com/twitter',
      width: 300,
      height: 400,
      theme: 'Light',
      lang: 'en'
    },

    TWITTER_MODAL,
    preview: '',
    themes: ['Light', 'Dark'],
    langs
  }),

  computed: {
    $form() {
      return this.$refs['twitterForm']
    },

    rules() {
      return {
        ...this.mapRules(
          ['url'],
          [
            {
              validator: this.validationTwitter,
              message: this.$t('twitter.invalidUrl')
            }
          ]
        )
      }
    }
  },

  watch: {
    twitterForm: {
      handler(newValue) {
        this.previewHandler(newValue)
      },
      deep: true
    }
  },

  methods: {
    closeModal() {
      this.$modal.close(TWITTER_MODAL)
    },

    validationTwitter(rules, url, callback) {
      if (!url.includes('https://twitter.com')) {
        return callback(new Error(this.$t('twitter.invalidUrl')))
      }
      return callback()
    },

    onSubmit() {
      const { $form } = this
      $form.validate(valid => {
        if (valid) {
          let embed = this.generateTwitterWidget()
          this.$emit('change', embed)
          this.closeModal()
        }
      })
    },

    previewHandler: _.debounce(function() {
      this.preview = this.generateTwitterWidget()
    }, 1000),

    generateTwitterWidget() {
      this.loadTwitterResources()

      let { lang, width, height, theme, url } = this.twitterForm

      let str = `<a
          class="twitter-timeline"
          data-lang="${lang}"
          data-width="${width}"
          data-height="${height}"
          data-theme="${theme.toLowerCase()}"
          href="${url}"
        >Tweets feed</a>
        <script async src="${twitterWidgetJs}" charset="utf-8"><\/script>`

      return str.replace(/(\r\n|\n|\t|\r)/gm, '').replace(/  +/g, ' ')
    },

    loadTwitterResources() {
      if (window.twttr) {
        window.twttr.widgets.load()
      } else if (!document.getElementById('twttr-widgets')) {
        const embed = document.createElement('script')
        embed.id = 'twttr-widgets'
        embed.src = twitterWidgetJs
        document.body.appendChild(embed)
      }
    }
  },

  mounted() {
    this.preview = this.generateTwitterWidget()
  }
}
</script>

<style lang="sass" scoped>
.twitter_title
  display: flex
  justify-content: space-between
  width: 64%
</style>
