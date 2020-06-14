<template>
  <el-menu
    :default-active="defaultActive"
    :class="className"
    @select="selectHandler"
    @open="openHandler"
    @close="closeHandler"
  >
    <draggable :disabled="!draggable" v-model="items" @change="handleChange">
      <el-menu-item
        v-for="item in items"
        :index="item.id"
        :id="item.id"
        :key="item.order"
        :label="item.title"
      >
        <span class="noselect">{{ item.title }}</span>
      </el-menu-item>
    </draggable>
  </el-menu>
</template>

<script>
import draggable from 'vuedraggable'

export default {
  components: {
    draggable
  },

  props: {
    items: {
      type: Array,
      required: true
    },
    className: {
      type: String,
      required: false
    },
    defaultActive: {
      type: String,
      required: false
    },
    commitName: {
      type: String,
      default: ''
    },
    draggable: {
      type: Boolean,
      default: true
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
    },

    handleChange() {
      let { commitName, items, $store } = this

      commitName &&
        $store.commit(
          commitName,
          items.map((x, index) => ({ ...x, order: index }))
        )
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
