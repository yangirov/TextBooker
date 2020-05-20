const path = require('path')

module.exports = {
  productionSourceMap: false,

  devServer: {
    port: 8081,
    historyApiFallback: true,
    hot: true,

    overlay: {
      warnings: true,
      errors: true
    },

    headers: {
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, PATCH, OPTIONS',
      'Access-Control-Allow-Headers': '*'
    },

    proxy: {
      '^/': {
        target: process.env.VUE_APP_HOST,
        changeOrigin: true
      }
    }
  },

  pluginOptions: {
    i18n: {
      locale: 'ru',
      fallbackLocale: 'en',
      localeDir: 'translations',
      enableInSFC: false
    }
  }
}
