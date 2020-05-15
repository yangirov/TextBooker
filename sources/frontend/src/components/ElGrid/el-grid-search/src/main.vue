<template>
    <div class="d-flex-row">
        <el-form
            :model="params"
            :inline="inline"
            ref="form"
            label-position="top"
            @submit.native.prevent="searchHandler()"
            :label-width="labelWidth ? labelWidth + 'px' : ''"
            class="d-flex-row align-items-end"
        >
            <i :class="iconClassName || 'search-icon'" v-if="showIcon"></i>
            <el-form-item
                v-for="(form, index) in forms"
                :key="index"
                :prop="form.itemType != 'daterange' ? form.prop : datePrefix + index"
                :label="form.label"
                :rules="form.rules || []"
                :label-width="form.labelWidth ? form.labelWidth + 'px' : ''"
            >
                <template v-if="form.visible !== undefined ? form.visible : true">
                    <el-input
                        v-if="form.itemType === 'input' || form.itemType === undefined"
                        v-model="params[form.modelValue]"
                        :size="form.size ? form.size : size"
                        v-mask="form.mask"
                        :clearable="form.clearable"
                        :readonly="form.readonly"
                        :disabled="form.disabled"
                        :placeholder="form.placeholder"
                        :style="itemStyle + (form.itemWidth ? `width: ${form.itemWidth}px;` : '')"
                        :rules="form.rules"
                        :class="form.class"
                    >
                    </el-input>

                    <el-select
                        v-else-if="form.itemType === 'select'"
                        v-model="params[form.modelValue]"
                        :clearable="form.clearable"
                        :size="form.size ? form.size : size"
                        :disabled="form.disabled"
                        :placeholder="form.placeholder"
                        :filterable="form.filterable"
                        :style="itemStyle + (form.itemWidth ? `width: ${form.itemWidth}px;` : '')"
                        :rules="form.rules"
                        :class="form.class"
                    >
                        <el-option
                            v-for="(option, optionIndex) in form.options"
                            :key="optionIndex + '_local'"
                            :value="typeof option === 'object' ? option[form.valueKey || 'value'] : option"
                            :label="typeof option === 'object' ? option[form.labelKey || 'label'] : option"
                        />
                        <el-option
                            v-for="(op, opIndex) in selectOptions[selectOptionPrefix + index]"
                            :key="opIndex + '_remote'"
                            :value="typeof op === 'object' ? op[form.valueKey || 'value'] : op"
                            :label="typeof op === 'object' ? op[form.labelKey || 'label'] : op"
                        />
                    </el-select>

                    <el-date-picker
                        v-else-if="form.itemType === 'date'"
                        v-model="params[form.modelValue]"
                        type="date"
                        :placeholder="form.placeholder"
                        format="dd.MM.yyyy"
                        v-mask:datetime="'dd.mm.yyyy'"
                        :value-format="form.valueFormat"
                        :size="form.size ? form.size : size"
                        :disabled="form.disabled"
                        :readonly="form.readonly"
                        :editable="form.editable"
                        :style="itemStyle + (form.itemWidth ? `width: ${form.itemWidth}px;` : '')"
                        :picker-options="form.pickerOptions || {}"
                        :rules="form.rules"
                        :class="form.class"
                    >
                    </el-date-picker>

                    <el-date-picker
                        v-else-if="form.itemType === 'daterange'"
                        v-model="params[form.modelValue]"
                        format="dd.MM.yyyy"
                        v-mask:datetime="'dd.mm.yyyy'"
                        :value-format="form.valueFormat"
                        :size="form.size ? form.size : size"
                        type="daterange"
                        @change="date => changeDate(date, form.startDateName, form.endDateName)"
                        :disabled="form.disabled"
                        :readonly="form.readonly"
                        :editable="form.editable"
                        :placeholder="form.placeholder"
                        :range-separator="form.separator || '-'"
                        :start-placeholder="form.startPlaceholder || 'Дата начала'"
                        :end-placeholder="form.endPlaceholder || 'Дата окончания'"
                        :style="itemStyle + (form.itemWidth ? `width: ${form.itemWidth}px;` : '')"
                        :picker-options="form.pickerOptions || {}"
                        :rules="form.rules"
                        :class="form.class"
                    >
                    </el-date-picker>

                    <el-checkbox
                        v-else-if="form.itemType === 'checkbox' || form.itemType === undefined"
                        v-model="params[form.modelValue]"
                        :size="form.size ? form.size : size"
                        :readonly="form.readonly"
                        :disabled="form.disabled"
                        :style="itemStyle + (form.itemWidth ? `width: ${form.itemWidth}px;` : '')"
                        :rules="form.rules"
                        :class="form.class"
                        :label="form.checkboxLabel"
                    >
                    </el-checkbox>
                </template>
            </el-form-item>
            <el-form-item label="">
                <div class="d-flex-row">
                    <el-button plain type="primary" :size="size" native-type="submit" :loading="submitLoading">
                        {{ submitBtnText }}
                    </el-button>

                    <el-tooltip effect="dark" :content="resetBtnText" placement="top-end">
                        <el-button
                            plain="true"
                            :size="size"
                            v-if="showResetBtn"
                            @click="resetForm"
                            :loading="submitLoading"
                            icon="el-icon-refresh"
                        ></el-button>
                    </el-tooltip>
                </div>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
import { formProps } from './props';

export default {
    name: 'ElGridSearch',
    props: formProps,
    data() {
        const { forms, fuzzy } = this.$props;

        const datePrefix = 'daterange-prefix';
        const selectOptionPrefix = 'select-option-prefix';
        let dataObj = {
            selectOptions: {}
        };

        let params = {};
        let format = {};
        let fuzzyOps = {};

        forms.forEach((v, i) => {
            const propType = typeof v.prop;
            if (propType === 'string') {
                v.modelValue = v.prop;
                params[v.prop] = v.value ?? '';
                fuzzyOps[v.prop] = v.fuzzy ? v.fuzzy : fuzzy;
                if (v.format) {
                    format[v.prop] = v.format;
                }
            } else if (propType === 'object' && Object.prototype.toString.call(v.prop) === '[object Array]') {
                v.prop.forEach(vv => {
                    params[vv] = '';
                    if (v.format) {
                        format[vv] = v.format;
                    }

                    fuzzyOps[vv] = v.fuzzy ? v.fuzzy : fuzzy;
                });
            }
            if (v.itemType === 'daterange') {
                params[datePrefix + i] = '';
                v.modelValue = datePrefix + i;
                params[v.startDateName] = '';
                params[v.endDateName] = '';
            }
            if (v.itemType === 'select' && (v.selectFetch || v.selectUrl)) {
                const dataKey = selectOptionPrefix + i;
                dataObj.selectOptions[dataKey] = [];
                const { axios } = this;
                if (!v.selectMethod) {
                    v.selectMethod = 'get';
                }
                this.getRemoteData({
                    fetch: v.selectFetch
                        ? v.selectFetch
                        : () => {
                              return axios[v.selectMethod](
                                  v.selectUrl,
                                  v.selectMethod.toLowerCase() === 'get' ? { params: v.selectParams } : v.selectParams
                              );
                          },
                    dataKey,
                    resultField: v.selectResultField || 'result',
                    resultHandler: v.selectResultHandler
                });
            }
        });

        return {
            params,
            datePrefix,
            selectOptionPrefix,
            ...dataObj,
            format,
            fuzzyOps
        };
    },
    computed: {
        itemStyle() {
            const { itemWidth } = this;
            if (itemWidth) {
                return `width: ${itemWidth}px;`;
            }
            return '';
        }
    },
    methods: {
        isArray(value) {
            return typeof value === 'object' && Object.prototype.toString.call(value) === '[object Array]';
        },
        searchHandler() {
            this.getParams((error, params) => {
                if (!error) {
                    const { submitHandler } = this;
                    if (submitHandler) {
                        submitHandler(params);
                    } else {
                        throw new Error('Need to set attribute: submitHandler!');
                    }
                }
            });
        },
        getParamFuzzy() {
            return this.fuzzyOps;
        },
        getParams(callback) {
            this.$refs['form'].validate(valid => {
                if (valid) {
                    const { params, datePrefix, format } = this;
                    let formattedForm = {};
                    Object.keys(params).forEach(v => {
                        if (v.indexOf(datePrefix) === -1) {
                            formattedForm[v] = format[v] ? format[v](params[v], v) : params[v];
                        }
                    });
                    if (callback) callback(null, formattedForm);
                } else {
                    if (callback) callback(new Error());
                }
            });
        },
        resetForm() {
            Object.keys(this.params).forEach(key => this.params[key] = '');
            this.$refs['form'].resetFields();
            this.searchHandler();
        },
        resetFields() {
            this.$refs['form'].resetFields();
        },
        changeDate(date, startDateName, endDateName) {
            let dates;
            if (date === null) {
                this.params[startDateName] = '';
                this.params[endDateName] = '';
                return;
            }
            if (typeof date === 'string') {
                dates = date.split(' - ');
            } else if (date && date.hasOwnProperty('length')) {
                const firstDate = date[0];
                const secondDate = date[1];
                dates = [
                    `${firstDate.getFullYear()}-${('0' + (firstDate.getMonth() + 1)).substr(-2)}-${(
                        '0' + firstDate.getDate()
                    ).substr(-2)}`,
                    `${secondDate.getFullYear()}-${('0' + (secondDate.getMonth() + 1)).substr(-2)}-${(
                        '0' + secondDate.getDate()
                    ).substr(-2)}`
                ];
            }

            this.params[startDateName] = dates[0];
            this.params[endDateName] = dates[1];
        },
        getRemoteData({ fetch, dataKey, resultField, resultHandler }) {
            fetch().then(response => {
                let result = response;
                if (typeof response === 'object' && !this.isArray(response)) {
                    if (resultField.indexOf('.') !== -1) {
                        resultField.split('.').forEach(vv => {
                            result = result[vv];
                        });
                    } else {
                        result = response[resultField];
                    }
                }

                if (!result || !(result instanceof Array)) {
                    console.warn(`The result of key:${resultField} is not Array.`);
                }

                if (this.resultHandler) {
                    this.selectOptions[dataKey] = result.map(this.resultHandler);
                } else {
                    this.selectOptions[dataKey] = result;
                }
            });
        }
    }
};
</script>

<style scoped>
.d-flex-row {
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;

    -webkit-box-orient: horizontal;
    -webkit-box-direction: normal;
    -ms-flex-direction: row;
    flex-direction: row;

    -webkit-box-align: center;
    -ms-flex-align: center;
    align-items: center;
}

.search-icon {
    font-size: 25px;
    margin-right: 10px;
    margin-bottom: 3px;
    opacity: 0.6;
}

.el-form-item {
    margin-bottom: 0 !important;
}
</style>
