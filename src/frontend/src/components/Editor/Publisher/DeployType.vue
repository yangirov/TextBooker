<template>
  <div>
    <div class="deploy_type_header">
      <p class="deploy_type_title">
        <i :class="'button-icon ' + icon"></i>
        {{ $t(`tabs.publish.ways.${deployType}.name`) }}
      </p>

      {{ $t(`tabs.publish.ways.${deployType}.description`) }}
    </div>

    <div class="deploy_type_content">
      <el-button
        type="success"
        @click="deploy()"
        class="mt-1 mr-hlf"
        size="small"
      >
        {{ $t(`tabs.publish.deploy`) }}
      </el-button>

      <el-divider direction="vertical"></el-divider>

      <a
        :href="$t(`tabs.publish.ways.${deployType}.link`)"
        target="_blank"
        rel="nofollow noreferrer"
      >
        {{ $t(`tabs.publish.ways.details`) }}
      </a>
    </div>

    <slot></slot>
  </div>
</template>

<script>
export default {
  props: {
    deployType: {
      type: String,
      required: true
    },
    icon: {
      type: String,
      required: true
    }
  },
  methods: {
    deploy() {
      this.$emit('deploy')
    }
  }
}
</script>

<style lang="sass">
.deploy_type
  &_title
    font-size: 1.1em
    font-weight: bold
    margin-bottom: 0.3em
  &_header
    display: block
  &_content
    display: block

.mr-hlf
  margin-right: 0.5em

.button-icon
  display: inline-block
  width: 1.2em
  height: 1.2em
  margin-right: .3em
  background-repeat: no-repeat
  background-position: center
  background-size: 100%
  &.ftp-icon
    font-size: 1.2em
    margin: 4px 0 0 2px
  &.vercel
    background-image: url('~@/assets/vercel.svg')
  &.github
    background-image: url('~@/assets/github.png')
</style>
