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

    <VueAceEditor
      v-model="form[location - 1].content"
      :options="aceOptions"
      class="mt-1"
    ></VueAceEditor>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import VueAceEditor from '@/components/Editor/ContentEditor/VueAceEditor.vue'

export default {
  components: {
    VueAceEditor
  },

  data: () => ({
    locations: [
      { location: 1, name: 'Head' },
      { location: 2, name: 'Before body' },
      { location: 3, name: 'After body' }
    ],

    location: 1,
    form: [],

    aceOptions: {
      mode: 'js',
      theme: 'clouds',
      fontSize: 16,
      fontFamily: 'monospace',
      highlightActiveLine: true,
      highlightGutterLine: true,
      maxLines: 25,
      wrap: true,
      autoScrollEditorIntoView: true,
      showPrintMargin: false
    }
  }),

  watch: {
    form: {
      handler(newValue) {
        this.handleData(newValue)
      },
      deep: true
    }
  },

  computed: {
    ...mapGetters('sites', ['site', 'loading'])
  },

  methods: {
    handleData: _.debounce(function(data) {
      let userScripts = data.map(x => ({ ...x, siteId: this.site.id }))
      this.$store.commit('sites/UPDATE_SITE', { userScripts })
    }, 300)
  },

  created() {
    this.form = _.values(
      _.merge(
        _.keyBy(this.site.userScripts, 'location'),
        _.keyBy(this.locations, 'location')
      )
    )
  }
}
</script>
