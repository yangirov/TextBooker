<template>
  <el-menu
    :default-active="currentId"
    :class="className"
    @select="selectItem"
    @open="openHandler"
    @close="closeHandler"
  >
    <el-menu-item
      v-for="item in items"
      :key="item.id"
      :id="item.id"
      :label="item.name"
      :class="{ 'is-active': isActiveItem(item.id) }"
    >
      <span class="noselect">{{ item.name }}</span>
      <slot name="actions"></slot>
    </el-menu-item>
  </el-menu>
</template>

<script>
export default {
  name: 'SelectList',

  data: () => ({
    currentId: ''
  }),

  props: {
    items: {
      type: Array,
      required: true
    },
    className: {
      type: String,
      required: false
    },
    defaultId: {
      type: String
    }
  },

  methods: {
    openHandler(data) {
      this.$emit('open', data)
    },
    closeHandler(data) {
      this.$emit('close', data)
    },
    isActiveItem(id) {
      return this.currentId === id
    },
    selectItem(id) {
      this.currentId = id
      this.$emit('handler', id)
    }
  },

  created() {
    this.currentId = this.defaultId
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
