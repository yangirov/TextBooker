<template>
    <el-popover :placement="placement" trigger="click" v-model="visible">
        <el-tooltip
            slot="reference"
            effect="dark"
            content="Delete"
            placement="top-end"
            :enterable="false"
            transition=""
            :open-delay="300"
            :disabled="visible || !tooltipVisible"
        >
            <el-button
                style="padding: 5px; font-size: 12px; line-height: 1;"
                v-bind="Object.assign({}, $attrs, $props)"
                @click.stop="visible = false"
            >
                {{ ' ' }}<slot />
            </el-button>
        </el-tooltip>

        <p>{{ text }}</p>

        <div class="tooltip-button">
            <el-button
                type="danger"
                size="mini"
                :disabled="!visible"
                v-on:click.stop="handler"
            >
                Удалить
            </el-button>
            <el-button size="mini" @click="visible = false">
                Отмена
            </el-button>
        </div>
    </el-popover>
</template>

<script>
export default {
    name: 'button-delete',

    data: () => ({
        visible: false
    }),

    props: {
        text: {
            type: String,
            default: 'The action is irreversible! Delete it??'
        },
        placement: {
            type: String,
            default: 'top-end'
        },
        type: {
            type: String,
            default: 'danger'
        },
        size: {
            type: String,
            default: 'mini'
        },
        disabled: {
            type: Boolean,
            default: false
        },
        loading: {
            type: Boolean,
            default: false
        },
        plain: {
            type: Boolean,
            default: true
        },
        icon: {
            type: String,
            default: 'fal fa-trash-alt'
        },
        tooltipVisible: {
            type: Boolean,
            default: true
        }
    },

    methods: {
        handler() {
            this.visible = false;
            this.$emit('handler');
        }
    }
};
</script>

<style>
.tooltip-button {
    display: flex;
}
</style>
