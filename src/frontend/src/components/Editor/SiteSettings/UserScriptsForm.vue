<template>
  <div class="user-scripts">
    <el-select
      v-model="location"
      :placeholder="$t('common.select')"
      size="small"
    >
      <el-option
        v-for="item in locations"
        :key="item.location"
        :label="item.name"
        :value="item.location"
      ></el-option>
    </el-select>

    <prism-editor
      class="mt-1"
      style="max-width: 850px; max-height: 600px; overflow-y: scroll;"
      v-model="form[location - 1].content"
      language="js"
      lineNumbers="true"
    ></prism-editor>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import PrismEditor from 'vue-prism-editor'

export default {
  components: {
    PrismEditor
  },

  data: () => ({
    locations: [
      { location: 1, name: 'Head' },
      { location: 2, name: 'Before body' },
      { location: 3, name: 'After body' }
    ],

    location: 1,
    form: []
  }),

  computed: {
    ...mapGetters('sites', ['site', 'loading'])
  },

  created() {
    this.form = _.merge(this.site.userScripts, this.locations)
  }
}
</script>
