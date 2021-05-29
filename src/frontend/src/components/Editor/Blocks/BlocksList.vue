<template>
  <div class="blocks_list">
    <SelectList
      :items="blocks"
      @handler="selectBlock"
      class-name="max-wh m-1"
      id="blocks_list"
    ></SelectList>

    <div class="buttons mt-1 ml-1">
      <el-button type="primary" @click="addBlock">
        <i class="el-icon-circle-plus-outline"></i>
        {{ $t('common.add') }}
      </el-button>

      <el-popconfirm
        :title="$t('common.deleteWarning')"
        :confirmButtonText="$t('common.yes')"
        :cancelButtonText="$t('common.no')"
        @onConfirm="deleteBlock"
        icon="el-icon-info"
        iconColor="red"
      >
        <el-button slot="reference" type="danger">
          <i class="el-icon-remove-outline"></i>
          {{ $t('common.delete') }}
        </el-button>
      </el-popconfirm>

      <el-button class="ml-1" type="info" icon="el-icon-dish"></el-button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlocksList',

  data: () => ({
    selectedBlock: 0,
    blocks: [
      {
        name: `${this.$t('tabs.blocks.defaultBlockName')} 1`,
        alias: 'block1'
      }
    ]
  }),

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
      this.$delete(this.blocks, this.selectedBlock)
    },

    selectBlock(index) {
      this.selectedBlock = index
    }
  }
}
</script>

<style lang="sass" scoped>
.buttons
  display: flex
  flex-wrap: nowrap
  justify-content: flex-start

.blocks_list
  display: flex
  flex-direction: column
  justify-content: space-between
  height: 100%
</style>
