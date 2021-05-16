<template>
  <section class="page">
    <PageHeader />
    <PageContent />
    <PageFooter />
  </section>
</template>

<script>
import { mapGetters } from 'vuex'

import PageHeader from '@/components/Layout/Header'
import PageContent from '@/components/Layout/Content'
import PageFooter from '@/components/Layout/Footer'

export default {
  components: {
    PageHeader,
    PageContent,
    PageFooter
  },

  computed: {
    ...mapGetters('appState', ['isInitAppState', 'isAuth'])
  }
};
</script>

<style lang="scss">
.page {
  width: 100%;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  min-height: 98vh;
}
</style>