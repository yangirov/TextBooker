<template>
  <div class="sign_form">
    <el-form
      ref="signForm"
      :model="signForm"
      :rules="rules"
      label-width="200px"
      @submit.native.prevent="onSubmit"
      v-loading="loading"
    >
      <el-form-item :label="$t('common.email')" prop="email" class="mb-2">
        <el-input
          type="email"
          v-model="signForm.email"
          placeholder="example@mail.com"
          v-mask="{ alias: 'email' }"
          clearable
          autocomplete
        ></el-input>
      </el-form-item>

      <el-form-item :label="$t('common.password')" prop="password" class="mb-2">
        <el-input
          :type="passwordType"
          v-model="signForm.password"
          placeholder="••••••••"
          clearable
          autocomplete
        >
          <el-button
            slot="append"
            icon="el-icon-view"
            @click="passwordVisible = !passwordVisible"
          ></el-button>
        </el-input>
      </el-form-item>

      <el-form-item>
        <el-button type="primary" native-type="submit">
          {{ $t('common.continue') }}
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'SignForm',

  props: {
    action: {
      type: String,
      required: true
    }
  },

  data: () => ({
    passwordVisible: false,

    signForm: {
      email: '',
      password: ''
    }
  }),

  computed: {
    ...mapGetters('user', ['loading']),

    passwordType() {
      return this.passwordVisible ? 'text' : 'password'
    },

    $form() {
      return this.$refs['signForm']
    },

    rules() {
      return {
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
    async login() {
      await this.$store.dispatch('user/login', this.signForm)
    },

    async register() {
      await this.$store.dispatch('user/register', this.signForm)
    },

    validationEmail(rule, value, callback) {
      if (!value) return callback()
      return window.validation.email(value)
        ? callback()
        : callback(new Error(rule.message))
    },

    onSubmit() {
      const { $form, action } = this

      $form.validate(async valid => {
        if (valid) {
          if (action === 'login') {
            await this.login()
          } else {
            await this.register()
          }
        } else {
          return false
        }
      })
    }
  }
}
</script>
