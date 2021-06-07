<template>
  <el-tooltip
    effect="dark"
    :content="tooltipText"
    placement="top-end"
    :enterable="false"
    transition=""
    :open-delay="300"
    :disabled="!tooltipVisible"
  >
    <el-button
      class="button-action"
      v-bind="[$props, $attrs]"
      @click.stop="handler"
    >
      {{ ' ' }}<slot />
    </el-button>
  </el-tooltip>
</template>

<script>
export default {
  name: 'button-action',

  props: {
    tooltipText: {
      type: String,
      default: 'Edit'
    },
    tooltipVisible: {
      type: Boolean,
      default: true
    },
    text: {
      type: String,
      default: ''
    },
    type: {
      type: String,
      default: 'primary'
    },
    plain: {
      type: Boolean,
      default: true
    },
    size: {
      type: String,
      default: 'small'
    },
    icon: {
      type: String,
      default: 'el-icon-edit'
    },
    disabled: {
      type: Boolean,
      default: false
    },
    loading: {
      type: Boolean,
      default: false
    }
  },

  methods: {
    handler() {
      this.$emit('handler')
    }
  }
}
</script>

<style lang="sass" scoped>
.buton-action
  padding: 5px
  font-size: 12px
  line-height: 1
</style>
