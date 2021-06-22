<template>
  <el-dialog
    :visible="$modal.isVisible(FTP_MODAL)"
    width="600px"
    top="5vh"
    :close-on-click-modal="true"
    :before-close="closeModal"
  >
    <div slot="title">
      <h3>{{ $t(`tabs.publish.ways.ftp.name`) }}</h3>
    </div>

    <div v-if="deployed">
      {{ $t(`tabs.publish.deployed`) }}
    </div>

    <el-form
      v-if="!deployed"
      ref="ftpForm"
      :model="ftpForm"
      :rules="rules"
      label-width="300px"
      @submit.native.prevent="onSubmit"
      size="small"
    >
      <el-form-item
        :label="$t('tabs.publish.ways.ftp.host')"
        prop="host"
        autocomplete
      >
        <el-input v-model="ftpForm.host"></el-input>
      </el-form-item>

      <el-form-item
        :label="$t('tabs.publish.ways.ftp.port')"
        prop="port"
        type="number"
        autocomplete
      >
        <el-input v-model="ftpForm.port"></el-input>
      </el-form-item>

      <el-form-item
        :label="$t('tabs.publish.ways.ftp.login')"
        prop="login"
        autocomplete
      >
        <el-input v-model="ftpForm.login"></el-input>
      </el-form-item>

      <el-form-item
        :label="$t('tabs.publish.ways.ftp.password')"
        prop="password"
        type="password"
        autocomplete
      >
        <el-input v-model="ftpForm.password"></el-input>
      </el-form-item>

      <el-form-item
        :label="$t('tabs.publish.ways.ftp.folder')"
        prop="folder"
        autocomplete
      >
        <el-input v-model="ftpForm.folder"></el-input>
      </el-form-item>

      <footer class="mt-2 text-right">
        <el-button @click="closeModal">
          {{ $t('common.close') }}
        </el-button>

        <el-button native-type="submit" :loading="loading" type="success">
          {{ $t('tabs.publish.deploy') }}
        </el-button>
      </footer>
    </el-form>
  </el-dialog>
</template>

<script>
import { FTP_MODAL } from '@/store/modals'
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    FTP_MODAL,
    ftpForm: {
      host: '',
      port: '',
      login: '',
      password: '',
      folder: ''
    }
  }),

  computed: {
    ...mapGetters('sites', ['site']),

    ...mapGetters('deploy', ['deployed']),

    $form() {
      return this.$refs['ftpForm']
    },

    rules() {
      return {
        ...this.mapRules(['host']),
        ...this.mapRules(['port']),
        ...this.mapRules(['login']),
        ...this.mapRules(['password']),
        ...this.mapRules(['folder'])
      }
    }
  },

  methods: {
    closeModal() {
      this.$modal.close(FTP_MODAL)
    },

    onSubmit() {
      const { $form, ftpForm, site } = this

      $form.validate(valid => {
        if (valid) {
          this.$store('deploy/ftpDeploy', { ...ftpForm, siteId: site.id })
          this.closeModal()
        }
      })
    }
  }
}
</script>
