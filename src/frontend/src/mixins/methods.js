import Vue from 'vue'

export default {
  mapRules(rulesName = [], option = []) {
    if (!rulesName.length) return
    return rulesName
      .map(nameField => ({
        [nameField]: [
          {
            required: true,
            message: this.$t('validation.required')
          },
          {
            validator: this.validation('isNotEmpty'),
            message: this.$t('validation.notEmpty')
          },
          ...option
        ]
      }))
      .reduce(
        (acc, item) => (
          (acc = {
            ...acc,
            ...item
          }),
          acc
        ),
        {}
      )
  },

  validation(type = '', success = () => {}, error = () => {}) {
    return (rule, value, callback) => {
      if (!type || !value) return callback()
      switch (type) {
        case 'name':
          return /^[A-Za-zА-Яа-яЁё]*$/.test(value)
            ? callback()
            : callback(new Error(rule.message))
        case 'isNotEmpty':
          return /^(?!\s)/.test(value)
            ? callback()
            : callback(new Error(rule.message))
        default:
          return window.validation[type](value)
            ? (success(), callback())
            : (error(), callback(new Error(rule.message)))
      }
    }
  },

  validationUniqueName(
    { data: dataGrid, index: indexEditRow, form, key = 'name' },
    success = () => {},
    error = () => {}
  ) {
    return (rule, value, callback) => {
      const data = [...dataGrid.filter((_, index) => index !== indexEditRow)]
      for (const item of data) {
        const isMatchName =
          this.formatString(form[key]) === this.formatString(item[key])
        if (isMatchName) return error(), callback(new Error(rule.message))
      }
      return success(), callback()
    }
  },

  formatString(str) {
    return (
      str &&
      String(str)
        .trim()
        .toLowerCase()
    )
  },

  resetForm(formName) {
    this.$refs[formName] && this.$refs[formName].resetFields()
  },

  get(from, selectors = '') {
    if (!selectors) return undefined
    return selectors
      .replace(/\[([^\[\]]*)\]/g, '.$1.')
      .split('.')
      .filter(Boolean)
      .reduce((prev, cur) => prev && prev[cur], from)
  },

  isNil(val) {
    return val === undefined || val === null
  },

  isNull(val) {
    return val === undefined || val === null
  }
}
