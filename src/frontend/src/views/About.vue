<template>
  <div class="row">
    <div class="inner">
      <div class="columns">
        <div class="column is-full">
          <h1 class="title" v-html="$t('about.title')"></h1>

          <div class="buttons mt-2">
            <router-link class="button is-blue is-medium" to="/editor">
              {{ $t('about.getStarted') }}
            </router-link>

            <a
              class="button is-light is-medium"
              href="https://github.com/yangirov/TextBooker/wiki"
              target="_blank"
              rel="nofollow noreferrer"
            >
              {{ $t('common.docs') }}
            </a>
          </div>
        </div>
      </div>

      <div class="columns mt-3">
        <div class="column is-half">
          <ul class="vantages">
            <li v-for="item in $t('about.vantages')" :key="item.title">
              <h2 class="subtitle">{{ item.title }}</h2>
              <p>{{ item.desc }}</p>
            </li>
          </ul>
        </div>

        <div class="column is-half">
          <video class="video-frame" controls muted autoplay loop>
            <source src="@/assets/demo.mp4" type="video/mp4" />
          </video>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {}
</script>

<style lang="sass">
.video-frame
  min-height: 320px
  max-width: 500px
  margin-top: -50px
  display: flex
  justify-content: center
  align-items: center
  &:focus, &:active
    outline: none

.row
  height: 80%
  display: flex
  align-items: center
  justify-content: center

.inner
  display: flex
  flex-direction: column
  align-items: flex-start
  justify-content: center

.subtitle
  padding-top: 1em
  margin-bottom: .5em !important

.vantages
  margin-top: -1em
</style>
