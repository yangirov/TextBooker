<template>
  <section class="page">
    <PageHeader />
    <PageMain />
    <PageFooter />
  </section>
</template>

<script>
import PageHeader from '@/components/Layout/Header'
import PageMain from '@/components/Layout/Main'
import PageFooter from '@/components/Layout/Footer'

export default {
  components: {
    PageHeader,
    PageMain,
    PageFooter
  },

  created() {
    this.$store.dispatch('appState/init')
    this.$store.dispatch('user/init')
  }
}
</script>

<style lang="sass" scoped>
.page
  width: 100%
  height: 100%
  margin: 0
  padding: 0
  display: flex
  flex-direction: column
  justify-content: flex-start
</style>
