<template>
  <el-menu
    :default-active="defaultActive"
    :class="className"
    @select="selectHandler"
    @open="openHandler"
    @close="closeHandler"
  >
    <el-menu-item
      v-for="item in items"
      :index="item.id"
      :id="item.id"
      :key="item.id"
      :label="item.title"
    >
      <span class="noselect">{{ item.title }}</span>
    </el-menu-item>
  </el-menu>
</template>

<script>
export default {
  props: {
    items: {
      type: Array,
      required: true
    },
    className: {
      type: String,
      required: false
    }
  },

  computed: {
    defaultActive() {
      return this.items && this.items[0]?.id
    }
  },

  methods: {
    selectHandler(id) {
      this.$emit('handler', id)
    },

    openHandler(data) {
      this.$emit('open', data)
    },

    closeHandler(data) {
      this.$emit('close', data)
    }
  }
}
</script>

<style lang="sass" scoped>
.el-menu
  overflow-y: scroll
  width: 100%
  height: 100%

.el-menu-item
  height: auto
  line-height: 1.5
  padding: 10px 15px

.el-menu-item.is-active
  outline: 0
  background-color: #ecf5ff
</style>
