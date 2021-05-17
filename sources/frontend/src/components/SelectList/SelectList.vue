<template>
  <el-menu
    :default-active="selectedIndex"
    :class="className"
    @select="selectItem"
    @open="openHandler"
    @close="closeHandler"
  >
    <el-menu-item
      v-for="(item, index) in items"
      :key="index"
      :index="index"
      :label="item.name"
      :class="{ 'is-active': isActiveItem(index) }"
    >
      <span>{{ item.name }}</span>
    </el-menu-item>
  </el-menu>
</template>

<script>
export default {
  name: 'SelectList',

  data() {
    return {
      selectedIndex: 0
    }
  },

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

  methods: {
    openHandler(data) {
      this.$emit('open', data)
    },
    closeHandler(data) {
      this.$emit('close', data)
    },
    isActiveItem(index) {
      return this.selectedIndex === index
    },
    selectItem(index) {
      this.selectedIndex = index
      this.$emit('handler', index)
    }
  }
}
</script>

<style lang="sass" scoped>
.el-menu
  overflow-y: scroll
  width: 100%
  height: 100%
  min-width: 13em

.el-menu-item
  height: auto
  line-height: 1.5
  padding: 10px 15px


.el-menu-item.is-active
  outline: 0
  background-color: #ecf5ff
</style>
