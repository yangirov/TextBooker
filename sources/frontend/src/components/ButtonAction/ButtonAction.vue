<template>
    <el-tooltip
        effect="dark"
        :content="tooltipText"
        placement="top-end"
        :enterable="false"
        transition=""
        :open-delay="300"
        :disabled="!tooltipVisible"
    >
        <el-button
            style="padding: 5px; font-size: 12px; line-height: 1;"
            v-bind="[$props, $attrs]"
            @click.stop="handler"
        >
            {{ ' ' }}<slot />
        </el-button>
    </el-tooltip>
</template>

<script>
export default {
    name: 'button-action',

    props: {
        tooltipText: {
            type: String,
            default: 'Редактировать'
        },
        tooltipVisible: {
            type: Boolean,
            default: true
        },
        text: {
            type: String,
            default: ''
        },
        type: {
            type: String,
            default: 'primary'
        },
        plain: {
            type: Boolean,
            default: true
        },
        size: {
            type: String,
            default: 'small'
        },
        icon: {
            type: String,
            default: 'fal fa-pen'
        },
        disabled: {
            type: Boolean,
            default: false
        },
        loading: {
            type: Boolean,
            default: false
        }
    },

    methods: {
        handler() {
            this.$emit('handler');
        }
    }
};
</script>
