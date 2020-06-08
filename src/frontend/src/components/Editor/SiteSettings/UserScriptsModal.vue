<template>
  <el-dialog
    :visible="$modal.isVisible(USER_SCRIPTS_MODAL)"
    width="900px"
    top="5vh"
    :close-on-click-modal="true"
    :before-close="closeModal"
  >
    <div slot="title">
      <h3>{{ $t('tabs.settings.userScripts') }}</h3>
    </div>

    <UserScriptsForm @close="closeModal" />
  </el-dialog>
</template>

<script>
import UserScriptsForm from './UserScriptsForm'
import { USER_SCRIPTS_MODAL } from '@/store/modals'

export default {
  data: () => ({
    USER_SCRIPTS_MODAL
  }),

  components: {
    UserScriptsForm
  },

  methods: {
    closeModal() {
      this.$modal.close(USER_SCRIPTS_MODAL)
    }
  }
}
</script>
