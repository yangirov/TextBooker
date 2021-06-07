<template>
  <div class="user-scripts">
    <el-select v-model="location" placeholder="Select">
      <el-option
        v-for="item in locations"
        :key="item.id"
        :label="item.name"
        :value="item.id"
      ></el-option>
    </el-select>

    <prism-editor
      class="mt-1"
      v-model="content"
      language="js"
      lineNumbers="true"
    ></prism-editor>

    <footer class="mt-2 text-right">
      <el-button @click="closeModal">
        Закрыть
      </el-button>

      <el-button
        :type="isEdit ? 'primary' : 'success'"
        native-type="submit"
        :loading="loading"
      >
        {{ isEdit ? 'Обновить' : 'Добавить' }}
      </el-button>
    </footer>
  </div>
</template>

<script>
import PrismEditor from 'vue-prism-editor'
import { USER_SCRIPTS_MODAL } from '@/store/modals'

export default {
  data: () => ({
    USER_SCRIPTS_MODAL,

    locations: [
      { id: 1, name: 'Head' },
      { id: 2, name: 'Before body' },
      { id: 3, name: 'After body' }
    ],

    location: 1,
    content: ''
  }),

  components: {
    PrismEditor
  },

  methods: {
    closeModal() {
      this.$modal.close(USER_SCRIPTS_MODAL)
    }
  }
}
</script>
