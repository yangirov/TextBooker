import ElGrid from './src/main';

ElGrid.install = function(Vue) {
    Vue.component(ElGrid.name, ElGrid);
};

export default ElGrid;
