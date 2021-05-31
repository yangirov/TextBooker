<template>
  <el-form
    ref="feedbackForm"
    :model="feedbackForm"
    :rules="rules"
    label-width="180px"
    @submit.native.prevent="onSubmit"
    v-loading="loading"
  >
    <el-form-item :label="$t('feedback.name')" prop="name" class="mb-2">
      <el-input v-model="feedbackForm.name" clearable></el-input>
    </el-form-item>

    <el-form-item :label="$t('feedback.email')" prop="email" class="mb-2">
      <el-input
        type="email"
        v-model="feedbackForm.email"
        placeholder="example@mail.ru"
        v-mask="{ alias: 'email' }"
        clearable
      ></el-input>
    </el-form-item>

    <el-form-item :label="$t('feedback.message')" prop="message" class="mb-2">
      <el-input
        type="textarea"
        v-model="feedbackForm.message"
        clearable
      ></el-input>
    </el-form-item>

    <el-form-item>
      <el-button type="primary" native-type="submit">
        {{ $t('feedback.send') }}
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'FeedbackForm',

  data: () => ({
    feedbackForm: {
      name: '',
      email: '',
      message: ''
    }
  }),

  computed: {
    ...mapGetters('user', ['user', 'loading']),

    $form() {
      return this.$refs['feedbackForm']
    },

    rules() {
      return {
        ...this.mapRules(
          ['name'],
          [
            {
              max: 30,
              message: this.$t('validation.maxLength', { count: 30 })
            }
          ]
        ),
        ...this.mapRules(
          ['email'],
          [
            {
              validator: this.validationEmail,
              trigger: 'blur',
              message: this.$t('validation.email')
            }
          ]
        ),
        ...this.mapRules(
          ['message'],
          [
            {
              max: 500,
              message: this.$t('validation.maxLength', { count: 500 })
            }
          ]
        )
      }
    }
  },

  methods: {
    validationEmail(rule, value, callback) {
      if (!value) return callback()
      return window.validation.email(value)
        ? callback()
        : callback(new Error(rule.message))
    },

    onSubmit(formName) {
      const { $form, feedbackForm } = this

      $form.$refs[formName].validate(valid => {
        if (valid) {
          this.$store.dispatch('appState/sendFeedback', feedbackForm)
        } else {
          return false
        }
      })
    }
  },

  mounted() {
    if (this.user.email) this.feedbackForm.email = this.user.email
  }
}
</script>
