export const formProps = {
    size: {
        type: String,
        default: 'small',
        validator: sizeValidator,
    },
    showResetBtn: {
        type: Boolean,
        default: true,
    },
    showIcon: {
        type: Boolean,
        default: true,
    },
    iconClassName: {
        type: String,
        default: 'el-icon-search search-icon',
    },
    inline: {
        type: Boolean,
        default: true,
    },
    fuzzy: {
        type: Boolean,
        default: true,
    },
    labelWidth: Number,
    itemWidth: Number,
    submitHandler: Function,
    submitLoading: {
        type: Boolean,
        default: false,
    },
    submitBtnText: {
        type: String,
        default: 'Найти',
    },
    resetBtnText: {
        type: String,
        default: 'Обновить таблицу',
    },
    forms: {
        type: Array,
        required: true,
        // label: String,
        // prop: {
        //   type: [String, Array],
        //   required: true
        // },
        // itemType: {
        //   type: String,
        //   default: 'input',
        //   validator: itemTypeValidator
        // },
        // size: {
        //   type: String,
        //   default: '',
        //   validator: sizeValidator
        // },
        // placeholder: {
        //   type: String,
        //   default: ''
        // },
        // editable: {
        //   type: Boolean,
        //   default: true
        // },
        // disabled: {
        //   type: Boolean,
        //   default: false
        // },
        // readonly: {
        //   type: Boolean,
        //   default: false
        // },
        // fuzzy: {
        //   type: Boolean,
        //   default: false
        // },
        // options: Array,
        // selectFetch: Function,
        // selectResultField: String,
        // selectResultHandler: Function,
        // selectUrl: String,
        // selectMethod: String,
        // valueKey: String,
        // labelKey: String,
        // format: Function,
        // selectParams: {
        //   type: Object,
        //   default: () => {
        //     return {}
        //   }
        // },
        // rules: Array,
        // pickerOptions: Object
    },
};

function sizeValidator(value) {
    const methodTypes = ['large', 'small', 'mini'];
    const valid = methodTypes.indexOf(value.toLowerCase()) !== -1 || value === '';
    if (!valid) {
        throw new Error(`Size must be one of ['large', 'small', 'mini']`);
    }
    return valid;
}

// function itemTypeValidator(value) {
//   const methodTypes = ['input', 'select', 'date', 'daterange'];
//   const valid = methodTypes.indexOf(value.toLowerCase()) !== -1
//   if (!valid) {
//     throw new Error(`ItemType must be one of ['input', 'select', 'date', 'daterange']`)
//   }
//   return valid
// }
