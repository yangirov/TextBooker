<template>
  <div class="sign_form">
    <el-form
      ref="signForm"
      :model="signForm"
      :rules="rules"
      label-width="200px"
    >
      <el-form-item :label="$t('common.email')" prop="email" class="mb-2">
        <el-input
          type="email"
          v-model="signForm.email"
          placeholder="example@mail.ru"
          v-mask="{ alias: 'email' }"
          clearable
        ></el-input>
      </el-form-item>

      <el-form-item :label="$t('common.password')" prop="password" class="mb-2">
        <el-input :type="passwordType" v-model="signForm.password" clearable>
          <el-button
            slot="append"
            icon="el-icon-view"
            @click="passwordVisible = !passwordVisible"
          ></el-button>
        </el-input>
      </el-form-item>

      <el-form-item>
        <el-button type="primary" @click="submitForm('signForm')">
          {{ $t('common.continue') }}
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'SignForm',

  computed: {
    passwordType() {
      return this.passwordVisible ? 'text' : 'password'
    }
  },

  data() {
    return {
      passwordVisible: false,

      signForm: {
        email: '',
        password: ''
      },

      rules: {
        ...this.mapRules(
          ['password'],
          [
            {
              max: 16,
              message: this.$t('validation.maxLength', { count: 16 })
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
