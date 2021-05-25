<template>
  <el-form
    ref="feedbackForm"
    :model="feedbackForm"
    :rules="rules"
    label-width="180px"
  >
    <el-form-item :label="$t('feedback.name')" prop="name" class="mb-2">
      <el-input v-model="feedbackForm.name" clearable></el-input>
    </el-form-item>

    <el-form-item :label="$t('feedback.email')" prop="email" class="mb-2">
      <el-input
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
      <el-button type="primary" @click="submitForm('feedbackForm')">
        {{ $t('feedback.send') }}
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'FeedbackForm',

  data() {
    return {
      feedbackForm: {
        name: '',
        email: '',
        message: ''
      },

      rules: {
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
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          alert('Submit!')
        } else {
          console.log('Error submit!')
          return false
        }
      })
    }
  }
}
</script>
