<template>
  <section class="page">
    <PageHeader />
    <div class="page-content">
      <PageAside v-if="isInitAppState" />
      <router-view v-if="isInitAppState"></router-view>
    </div>
    <PageFooter />
  </section>
</template>

<script>
import { mapGetters } from 'vuex'

import PageHeader from './components/Layout/Header'
import PageAside from './components/Layout/Aside'
import PageFooter from './components/Layout/Footer'

export default {
  components: {
    PageHeader,
    PageAside,
    PageFooter
  },

  computed: {
    ...mapGetters('appState', ['isInitAppState', 'isAuth'])
  }
};
</script>

<style>
section.page {
  display: flex;
  flex-direction: column;
}
h1 {
  font-size: 22px;
  margin: 0;
}
main.page-main>header {
  padding: 15px;
  display: flex;
  align-items: center;
  min-height: 67px;
}
.search-icon {
  font-size: 25px;
  margin-right: 10px;
  opacity: 0.6;
}
</style>
