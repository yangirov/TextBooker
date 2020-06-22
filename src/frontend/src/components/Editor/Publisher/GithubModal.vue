<template>
  <el-dialog
    :visible="$modal.isVisible(GITHUB_MODAL)"
    width="900px"
    top="5vh"
    :close-on-click-modal="true"
    :before-close="closeModal"
  >
    <div slot="title">
      <h3>{{ $t(`tabs.publish.ways.github.name`) }}</h3>
    </div>

    <p>Coming soon...</p>
  </el-dialog>
</template>

<script>
import { GITHUB_MODAL } from '@/store/modals'

export default {
  data: () => ({
    GITHUB_MODAL
  }),

  methods: {
    closeModal() {
      this.$modal.close(GITHUB_MODAL)
    }
  }
}
</script>
