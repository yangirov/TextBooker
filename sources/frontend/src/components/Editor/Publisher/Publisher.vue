<template>
  <div class="columns p-1">
    <div class="column is-6">
      <h2 class="tab-title">{{ $t('tabs.publish.name') }}</h2>

      <el-collapse v-model="activeName" accordion>
        <el-collapse-item name="1">
          <template #title>
            <i class="button-icon github"></i>
            Github Pages
          </template>
          <div>
            {{ $t('tabs.publish.ways.github.description') }}
          </div>
          <el-button
            type="success"
            @click="netlifyDeploy()"
            class="mt-1 mr-hlf"
          >
            Deploy
          </el-button>
          <el-divider direction="vertical"></el-divider>
          <a
            :href="$t('tabs.publish.ways.netlify.link')"
            target="_blank"
            rel="nofollow noreferrer"
          >
            {{ $t('tabs.publish.ways.details') }}
          </a>
        </el-collapse-item>

        <el-collapse-item name="2">
          <template #title>
            <i class="button-icon netlify"></i>
            Netlify
          </template>
          <div>
            {{ $t('tabs.publish.ways.netlify.description') }}
          </div>
          <el-button
            class="mt-1 mr-hlf"
            type="success"
            @click="netlifyDeploy()"
          >
            Deploy
          </el-button>
          <el-divider direction="vertical"></el-divider>
          <a
            :href="$t('tabs.publish.ways.github.link')"
            target="_blank"
            rel="nofollow noreferrer"
          >
            {{ $t('tabs.publish.ways.details') }}
          </a>
        </el-collapse-item>
      </el-collapse>
    </div>

    <!-- <div class="column is-6">
      <h4>{{ $t('tabs.publish.log.name') }}</h4>
    </div> -->
  </div>
</template>

<script>
export default {
  data() {
    return {}
  },
  methods: {
    githubDeploy() {},

    netlifyDeploy() {}
  }
}
</script>

<style lang="sass">
.mr-hlf
  margin-right: 0.5em

.button-icon
  width: 1.5em
  height: 1.5em
  margin-right: .6em
  background-repeat: no-repeat
  background-position: center
  background-size: 100%
  &.netlify
    background-image: url('~@/assets/netlify.svg')
  &.github
    background-image: url('~@/assets/github.png')
</style>
