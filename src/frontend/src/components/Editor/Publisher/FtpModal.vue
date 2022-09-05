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

    <p>{{ $t('tabs.publish.ways.ftp.description') }}</p>

    <div class="buttons">
      <el-button @click="downloadSite" :loading="loading" type="success">
        {{ $t('common.download') }}
      </el-button>
    </div>
  </el-dialog>
</template>

<script>
import { FTP_MODAL } from '@/store/modals'
import { mapGetters } from 'vuex'

export default {
  data: () => ({
    FTP_MODAL,
  }),

  computed: {
    ...mapGetters('sites', ['site']),
  },

  methods: {
    closeModal() {
      this.$modal.close(FTP_MODAL)
    },

    downloadSite() {
      const { site } = this

      this.$store.dispatch('deploy/downloadSite', site)
      this.closeModal()
    }
  }
}
</script>

<style>
.buttons {
  margin-top: 1em;
}
</style>