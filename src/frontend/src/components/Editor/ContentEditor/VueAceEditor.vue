<template>
  <div
    :id="id ? id : $options._componentTag + '-' + _uid"
    :class="$options._componentTag"
  >
    <slot></slot>
  </div>
</template>

<script>
import 'ace-builds/src-min-noconflict/ace'
import 'ace-builds/src-min-noconflict/theme-clouds'
import 'ace-builds/src-min-noconflict/mode-javascript'
import 'ace-builds/src-min-noconflict/mode-html'

export default {
  props: {
    value: {
      type: String,
      default: ''
    },

    id: {
      type: String,
      default: ''
    },

    options: {
      type: Function
    }
  },

  watch: {
    value() {
      this.$emit('input', this.value)

      if (this.oldValue !== this.value) {
        this.editor.setValue(this.value, 1)
      }
    }
  },

  mounted() {
    this.editor = window.ace.edit(this.$el.id)

    this.editor.$blockScrolling = Infinity

    const session = this.editor.getSession()
    session.on('changeAnnotation', () => {
      const a = session.getAnnotations()
      const b = a.slice(0).filter(item => item.text.indexOf('DOC') == -1)
      if (a.length > b.length) session.setAnnotations(b)
    })

    this.options = this.options || {}

    this.options.maxLines = this.options.maxLines || Infinity
    this.options.printMargin = this.options.printMargin || false
    this.options.highlightActiveLine = this.options.highlightActiveLine || false

    if (this.options.cursor === 'none' || this.options.cursor === false) {
      this.editor.renderer.$cursorLayer.element.style.display = 'none'
      delete this.options.cursor
    }

    if (this.options.mode && this.options.mode.indexOf('ace/mode/') === -1) {
      this.options.mode = `ace/mode/${this.options.mode}`
    }
    if (this.options.theme && this.options.theme.indexOf('ace/theme/') === -1) {
      this.options.theme = `ace/theme/${this.options.theme}`
    }
    this.editor.setOptions(this.options)

    if (!this.value || this.value === '') {
      this.$emit('input', this.editor.getValue())
    } else {
      this.editor.setValue(this.value, -1)
    }

    this.editor.on('change', () => {
      this.value = this.oldValue = this.editor.getValue()
    })
  },

  methods: {
    editor() {
      return this.editor
    }
  }
}
</script>

<style lang="sass">
.ace_editor
  margin: 2rem 0
  border: 1px solid #eee
</style>
