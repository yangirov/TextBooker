import ElGridSearch from './src/main';

ElGridSearch.install = function(Vue) {
    Vue.component(ElGridSearch.name, ElGridSearch);
};

export default ElGridSearch;
