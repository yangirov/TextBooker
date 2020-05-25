<template>
  <div class="row">
    <div class="inner">
      <div class="columns">
        <div class="column is-full">
          <h1 class="title" v-html="$t('about.title')"></h1>

          <div class="buttons mt-2">
            <router-link class="button is-blue is-medium" to="editor">
              {{ $t('about.getStarted') }}
            </router-link>

            <a
              class="button is-light is-medium"
              href="help"
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
          <ul>
            <li v-for="item in $t('about.vantages')" :key="item.title">
              <h2 class="subtitle">{{ item.title }}</h2>
              <p>{{ item.desc }}</p>
            </li>
          </ul>
        </div>

        <div class="column is-half">
          <iframe
            class="iframe"
            src="https://www.youtube.com/embed/ezp60FUnjGg"
            frameborder="0"
            allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
            allowfullscreen
          ></iframe>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {}
</script>

<style lang="sass">
.row
  height: 70%
  display: flex
  align-items: center
  justify-content: center

.inner
  display: flex
  flex-direction: column
  align-items: flex-start
  justify-content: center

.iframe
  width: 100%
  height: 100%
  border: 1px solid #ccc
  margin-left: 2em

.subtitle
  padding-top: 1em
  margin-bottom: .5em !important
</style>
