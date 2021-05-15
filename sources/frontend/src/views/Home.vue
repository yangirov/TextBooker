<template>
  <div class="flex width-full">
    <p>Hello, World!</p>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'Auth',
  
  computed: {
    ...mapGetters('appState', ['isAuth'])
  }
}
</script>