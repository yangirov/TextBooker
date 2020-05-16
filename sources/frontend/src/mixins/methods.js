import Vue from 'vue';

export default {
    mapRules(rulesName = [], option = []) {
        if (!rulesName.length) return;
        return rulesName
            .map(nameField => ({
                [nameField]: [
                    { required: true, message: 'Required field' },
                    {
                        validator: this.validation('isNotEmpty'),
                        message: 'Field cannot be empty'
                    },
                    ...option
                ]
            }))
            .reduce((acc, item) => ((acc = { ...acc, ...item }), acc), {});
    },

    validation(type = '', success = () => { }, error = () => { }) {
        return (rule, value, callback) => {
            if (!type || !value) return callback();
            switch (type) {
                case 'name':
                    return /^[A-Za-zА-Яа-яЁё]*$/.test(value) ? callback() : callback(new Error(rule.message));
                case 'isNotEmpty':
                    return /^(?!\s)/.test(value) ? callback() : callback(new Error(rule.message));
                default:
                    return window.validation[type](value)
                        ? (success(), callback())
                        : (error(), callback(new Error(rule.message)));
            }
        };
    },

    validationUniqueName(
        { data: dataGrid, index: indexEditRow, form, key = 'name' },
        success = () => { },
        error = () => { }
    ) {
        return (rule, value, callback) => {
            const data = [...dataGrid.filter((_, index) => index !== indexEditRow)];
            for (const item of data) {
                const isMatchName = this.formatString(form[key]) === this.formatString(item[key]);
                if (isMatchName) return error(), callback(new Error(rule.message));
            }
            return success(), callback();
        };
    },

    formatString(str) {
        return str && String(str).trim().toLowerCase();
    },

    resetForm(formName) {
        this.$refs[formName] && this.$refs[formName].resetFields();
    },

    debounce(callback, limit = 500) {
        let wait = false;
        return function () {
            if (!wait) {
                callback.call();
                wait = true;
                setTimeout(function () {
                    wait = false;
                }, limit);
            }
        };
    },

    getName(fisrtName = '', middleName = '', lastName = '') {
        let isString = val => val && typeof val === 'string';
        let _upper = value => (isString(value) ? value[0].toUpperCase() + value.slice(1).toLowerCase() : '');
        let _first = value => (isString(value) ? `${value[0].toUpperCase()}.` : '');

        return {
            short: `${_upper(fisrtName)} ${_first(middleName)} ${_first(lastName)}`,
            full: `${_upper(fisrtName)} ${_upper(middleName)} ${_upper(lastName)}`
        };
    },

    get(from, selectors = '') {
        if (!selectors) return undefined;
        return selectors
            .replace(/\[([^\[\]]*)\]/g, '.$1.')
            .split('.')
            .filter(Boolean)
            .reduce((prev, cur) => prev && prev[cur], from);
    },

    isNil(val) {
        return val === undefined || val === null;
    },

    isNull(val) {
        return val === undefined || val === null;
    },

    showSuccessNotify(text, options = {}) {
        Vue.prototype.$notify({
            title: 'Успешно',
            message: text,
            type: 'success',
            customClass: 'notify-success',
            ...options
        });
    },

    showErrorNotify(text, options = {}) {
        Vue.prototype.$notify({
            title: 'Ошибка',
            message: text,
            type: 'error',
            customClass: 'notify-error',
            ...options
        });
    },

    showInfoNotify(text, options = {}) {
        Vue.prototype.$notify({
            message: text,
            type: 'info',
            customClass: 'notify-info',
            ...options
        });
    },

    showWarningNotify(text, options = {}) {
        Vue.prototype.$notify({
            message: text,
            type: 'warning',
            customClass: 'notify-warning',
            ...options
        });
    }
};
