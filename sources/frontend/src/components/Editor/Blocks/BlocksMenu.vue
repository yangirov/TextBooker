<template>
  <div class="blocks_menu">
    <SelectList
      :items="blocks"
      @handler="selectBlock"
      class-name="max-wh m-1"
    ></SelectList>
    <div class="buttons mt-1 ml-1">
      <el-button type="primary" @click="addBlock">➕ {{ $t('common.add') }}</el-button>
      
      <el-popconfirm 
        :title="$t('common.deleteWarning')" 
        :confirmButtonText="$t('common.yes')"
        :cancelButtonText="$t('common.no')"
        :onConfirm="deleteBlock"
        icon="el-icon-info"
        iconColor="red"
      >
        <el-button slot="reference" type="danger">➖ {{ $t('common.delete') }}</el-button>
      </el-popconfirm>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlocksMenu',

  data() {
    return {
      selectedBlock: 0,
      blocks: [
        {
          name: `${this.$t('tabs.blocks.defaultBlockName')} 1`,
          alias: "block1"
        }
      ]
    }
  },

  methods: {
    addBlock() {
      let index = this.blocks.length + 1
      let block = { 
        name: `${this.$t('tabs.blocks.defaultBlockName')} ${index}`, 
        alias: `block${index}` 
      }
      this.blocks.push(block)
    },
    
    deleteBlock() {
      this.blocks.splice(this.selectedBlock, 1)
    },

    selectBlock(index) {
      this.selectedBlock = index
    }
  },
}
</script>

<style lang="scss" scoped>
.buttons {
  flex-wrap: nowrap;
  justify-content: space-evenly;
}
.blocks_menu {
  height: 94%;
}
</style>