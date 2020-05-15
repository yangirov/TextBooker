export default {
    lowercase(value) {
        if (!value && value !== 0) return '';
        return value && String(value).toLocaleLowerCase();
    },

    capitalize(value) {
        if (!value && value !== 0) return '';
        value = String(value).toLowerCase();
        return value.charAt(0).toUpperCase() + value.slice(1);
    }
}
