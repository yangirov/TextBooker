<template>
  <div class="columns">
    <div class="column is-3">
      <slot name="right-aside"></slot>
    </div>

    <div class="column">
      <slot name="content"></slot>
    </div>
    
    <slot name="left-aside"></slot>
  </div>
</template>

<script>
export default {
  name: 'InnerContainer'
}
</script>

<style lang="scss" scoped>
.columns {
  height: 100%;
}
</style>