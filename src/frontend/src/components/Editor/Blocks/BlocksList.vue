<template>
  <div class="blocks_list">
    <SelectList
      class-name="max-wh m-1"
      :default-active="defaultActive"
      :items="blocks"
      @handler="selectBlock"
      commit-name="blocks/UPDATE_BLOCKS"
    ></SelectList>

    <div class="buttons mt-1">
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

      <el-dropdown
        trigger="click"
        placement="top-start"
        @command="insertBlockTemplate"
      >
        <span class="el-dropdown-link">
          <el-button class="ml-1" type="info" icon="el-icon-dish"></el-button>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item command="twitter">
            <div class="dropdown_item">
              <img class="twitter_icon" src="@/assets/twitter.svg" />
              Twitter
            </div>
          </el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>

      <TwitterWidget @change="handleTwitterWidget" />
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import { TWITTER_MODAL } from '@/store/modals'
import TwitterWidget from './Generators/TwitterWidget.vue'
import SelectList from '@/components/Editor/SelectList/SelectList.vue'

export default {
  components: {
    SelectList,
    TwitterWidget
  },

  data: () => ({
    defaultActive: '',
    TWITTER_MODAL
  }),

  computed: {
    ...mapGetters('sites', ['site']),
    ...mapGetters('blocks', ['blocks', 'block'])
  },

  methods: {
    addBlock() {
      let index = this.blocks.length + 1 ?? 1

      let data = {
        title: `${this.$t('tabs.blocks.defaultBlockName')} ${index}`,
        alias: `block${index}`,
        siteId: this.site.id
      }

      this.$store.dispatch('blocks/addBlock', data)
    },

    deleteBlock() {
      this.$store.dispatch('blocks/deleteBlock', {
        id: this.block.id,
        siteId: this.site.id
      })
    },

    selectBlock(id) {
      this.defaultActive = id
      let block = this.blocks.find(x => x.id === id) ?? {}
      this.$store.commit('blocks/SET_BLOCK', block)
    },

    handleTwitterWidget(content) {
      this.$store.commit('blocks/SET_BLOCK', { ...this.block, content })
    },

    insertBlockTemplate(template) {
      switch (template) {
        case 'twitter':
          this.$modal.open(TWITTER_MODAL)
          break
        default:
          break
      }
    }
  },

  mounted() {
    setTimeout(() => {
      this.selectBlock(this.blocks[0]?.id)
    }, 100)
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

.dropdown_item
  display: flex
  .twitter_icon
    height: 2em
    width: 2em
    margin-right: .3em
</style>
