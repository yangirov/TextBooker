<template>
  <el-dialog
    :visible="$modal.isVisible(VERCEL_MODAL)"
    width="900px"
    top="5vh"
    :close-on-click-modal="true"
    :before-close="closeModal"
  >
    <div slot="title">
      <h3>{{ $t(`tabs.publish.ways.vercel.name`) }}</h3>
    </div>

    <p>Coming soon...</p>
  </el-dialog>
</template>

<script>
import { VERCEL_MODAL } from '@/store/modals'

export default {
  data: () => ({
    VERCEL_MODAL
  }),

  methods: {
    closeModal() {
      this.$modal.close(VERCEL_MODAL)
    }
  }
}
</script>
