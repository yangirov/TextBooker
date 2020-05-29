<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-half">
      <h1 class="title mb-2">{{ $t('feedback.title') }}</h1>
      <FeedbackForm />
    </div>
  </div>
</template>

<script>
import FeedbackForm from '@/components/Information/FeedbackForm.vue'

export default {
  components: {
    FeedbackForm
  }
}
</script>

<style lang="sass" scoped>
.columns
  height: 70%
  display: flex
  align-items: center
</style>
