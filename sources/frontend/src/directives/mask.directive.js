import Vue from 'vue';
import Inputmask from 'inputmask';

export default Vue.directive('mask', {
    bind: function(el, binding) {
        if (binding.value) {
            let options =
                binding.arg === 'datetime'
                    ? {
                          alias: 'datetime',
                          inputFormat: binding.value,
                          placeholder: binding.value.replace(
                              /\w/gi,
                              char => ({ d: 'д', m: 'м', y: 'г', h: 'ч' }[char.toLocaleLowerCase()])
                          ),
                      }
                    : binding.value;

            const selectorList = el.querySelectorAll('input');

            selectorList.length
                ? Array.from(selectorList, inputNode => new Inputmask(options).mask(inputNode))
                : new Inputmask(options).mask(el);
        }
    },
});
