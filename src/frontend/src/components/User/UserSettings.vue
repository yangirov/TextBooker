<template>
  <el-form ref="form" @submit.native.prevent="onSubmit" v-loading="loading">
    <el-form-item>
      <el-input
        :placeholder="$t('user.username')"
        v-model="form.username"
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
          @onConfirm="deleteUser"
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
    form: {
      username: ''
    }
  }),

  computed: {
    ...mapGetters('user', ['user', 'loading'])
  },

  methods: {
    onSubmit() {
      this.$store.dispatch('user/update', this.form)
    },

    deleteUser() {
      this.$store.dispatch('user/delete')
    }
  },

  mounted() {
    this.form.username = this.user.username
  }
}
</script>
