<template>
  <div>
    Hello!
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'Home',

  computed: {
    ...mapGetters('appState', ['isAuth'])
  },

  created() {
    this.$router.push('/editor')
  }
}
</script>