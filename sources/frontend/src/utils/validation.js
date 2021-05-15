const validation = {
    email(email) {
        let regExp = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regExp.test(String(email).toLowerCase());
    },

    phone(value) {
        let regExp = /^\+7\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}$/;
        return regExp.test(String(value).toLowerCase());
    },

    url(value) {
        let regExp = /^(http|https):\/\/(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]):[0-9]{1,5}$/;
        return regExp.test(String(value).toLowerCase());
    }
};

window.validation = validation;
export { validation };
