<template>
  <el-form
    ref="userForm"
    :model="userForm"
    :rules="rules"
    @submit.native.prevent="onSubmit"
    v-loading="loading"
  >
    <el-form-item prop="username">
      <el-input
        :placeholder="$t('user.username')"
        v-model="userForm.username"
        clearable
      ></el-input>
    </el-form-item>

    <el-form-item>
      <div class="buttons mt-1">
        <el-button type="primary" native-type="submit">
          {{ $t('common.save') }}
        </el-button>

        <el-popconfirm
          :title="$t('user.deleteWarning')"
          :confirmButtonText="$t('common.yes')"
          :cancelButtonText="$t('common.no')"
          @confirm="deleteUser"
        >
          <el-button slot="reference" type="danger">
            {{ $t('user.delete') }}
          </el-button>
        </el-popconfirm>
      </div>
    </el-form-item>
  </el-form>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    userForm: {
      username: ''
    }
  }),

  computed: {
    ...mapGetters('user', ['user', 'loading']),

    rules() {
      return {
        username: [
          {
            max: 20,
            message: this.$t('validation.maxLength', { count: 20 })
          }
        ]
      }
    },

    $form() {
      return this.$refs['userForm']
    }
  },

  methods: {
    onSubmit() {
      const { $form, userForm } = this

      $form.validate(async valid => {
        if (valid) {
          this.$store.dispatch('user/update', userForm)
        } else {
          return false
        }
      })
    },

    deleteUser() {
      this.$store.dispatch('user/delete').then(() => {
        localStorage.clear()
        this.$store.dispatch('user/reset')
        this.$store.dispatch('sites/reset')
        this.$router.push('/')
      })
    }
  },

  mounted() {
    this.userForm.username = this.user.username
  }
}
</script>
