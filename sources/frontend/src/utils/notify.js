import Vue from 'vue';

export function showSuccessNotify(text, options = {}) {
    Vue.prototype.$notify({
        title: 'Успешно',
        message: text,
        type: 'success',
        customClass: 'notify-success',
        ...options,
    });
}

export function showErrorNotify(text, options = {}) {
    Vue.prototype.$notify({
        title: 'Ошибка',
        message: text,
        type: 'error',
        customClass: 'notify-error',
        ...options,
    });
}

export function showInfoNotify(text, options = {}) {
    Vue.prototype.$notify({
        message: text,
        type: 'info',
        customClass: 'notify-info',
        ...options,
    });
}

export function showWarningNotify(text, options = {}) {
    Vue.prototype.$notify({
        message: text,
        type: 'warning',
        customClass: 'notify-warning',
        ...options,
    });
}
