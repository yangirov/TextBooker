<template>
  <el-upload
    action=""
    :http-request="upload"
    :on-change="uploadChanged"
    :before-upload="beforeUpload"
    :multiple="false"
    :auto-upload="true"
    :show-file-list="false"
    :accept="accept"
  >
    <slot />
  </el-upload>
</template>

<script>
export default {
  props: {
    upload: {
      type: Function,
      required: true
    },

    beforeUpload: {
      type: Function,
      required: true
    },

    accept: {
      type: String,
      default: 'image/png, image/jpg, image/jpeg',
      required: false
    }
  },

  methods: {
    uploadChanged(file, fileList) {
      this.$emit('input', file)
    }
  }
}
</script>
