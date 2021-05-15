import ElGrid from './el-grid/';
import ElGridSearch from './el-grid-search/';

export default function install(Vue) {
    [ElGrid, ElGridSearch].map(component => {
        Vue.component(component.name, component);
    });
}
