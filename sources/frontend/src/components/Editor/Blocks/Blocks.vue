<template>
  <InnerContainer>
    <template #right-aside>
      <BlocksMenu />
    </template>

    <template #content>
      
    </template>
  </InnerContainer>
</template>

<script>
import BlocksMenu from './BlocksMenu.vue'
import InnerContainer from '@/components/Layout/InnerContainer.vue'

export default {
  name: 'Blocks',

  components: {
    InnerContainer,
    BlocksMenu
  }
}
</script>