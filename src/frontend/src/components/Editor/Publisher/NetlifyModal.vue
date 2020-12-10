<template>
  <el-dialog
    :visible="$modal.isVisible(NETLIFY_MODAL)"
    width="900px"
    top="5vh"
    :close-on-click-modal="true"
    :before-close="closeModal"
  >
    <div slot="title">
      <h3>{{ $t(`tabs.publish.ways.netlify.name`) }}</h3>
    </div>

    <p>Coming soon...</p>
  </el-dialog>
</template>

<script>
import { NETLIFY_MODAL } from '@/store/modals'

export default {
  data: () => ({
    NETLIFY_MODAL
  }),

  methods: {
    closeModal() {
      this.$modal.close(NETLIFY_MODAL)
    }
  }
}
</script>
