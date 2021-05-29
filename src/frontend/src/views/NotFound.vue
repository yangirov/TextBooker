<template>
  <div class="columns is-mobile is-centered">
    <div class="column is-4 text-center">
      <h1 class="title mb-2">{{ $t('notfound.title') }}</h1>
      <img class="notfound_image" src="@/assets/404.gif" alt="404" />
    </div>
  </div>
</template>

<style lang="sass" scoped>
.columns
  height: 90%
  display: flex
  align-items: center

.notfound_image
  max-width: 500px
</style>
