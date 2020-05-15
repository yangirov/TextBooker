<template>
    <div class="grid-table">
        <slot name="grid-header" :loading="loading" :search="searchHandler"></slot>

        <search-form
            class="grid-table__search-form"
            v-if="formOptions"
            ref="searchForm"
            :showIcon="formOptions.showIcon"
            :iconClassName="formOptions.iconClassName"
            :forms="formOptions.forms"
            :size="formOptions.size"
            :fuzzy="formOptions.fuzzy"
            :inline="formOptions.inline"
            :label-width="formOptions.labelWidth"
            :item-width="formOptions.itemWidth"
            :submit-handler="searchHandler"
            :submit-loading="loading"
            :showResetBtn="formOptions.showResetBtn"
            :submitBtnText="formOptions.submitBtnText"
            :resetBtnText="formOptions.resetBtnText"
        >
        </search-form>

        <el-table
            v-loading.lock="loading"
            ref="table"
            :data="tableData"
            :border="border"
            :size="size"
            :stripe="stripe"
            :height="height"
            :max-height="maxHeight"
            :fit="true"
            :show-header="showHeader"
            :highlight-current-row="highlightCurrentRow"
            :current-row-key="currentRowKey"
            :row-class-name="rowClassName"
            :row-style="rowStyle"
            :row-key="rowKey"
            :empty-text="emptyText"
            :default-expand-all="defaultExpandAll"
            :expand-row-keys="expandRowKeys"
            :default-sort="defaultSort"
            :tooltip-effect="tooltipEffect"
            :show-summary="showSummary"
            :sum-text="sumText"
            :summary-method="summaryMethod"
            :style="tableStyle"
            @select="(selection, row) => emitEventHandler('select', selection, row)"
            @select-all="selection => emitEventHandler('select-all', selection)"
            @selection-change="selection => emitEventHandler('selection-change', selection)"
            @cell-mouse-enter="
                (row, column, cell, event) => emitEventHandler('cell-mouse-enter', row, column, cell, event)
            "
            @cell-mouse-leave="
                (row, column, cell, event) => emitEventHandler('cell-mouse-leave', row, column, cell, event)
            "
            @cell-click="(row, column, cell, event) => emitEventHandler('cell-click', row, column, cell, event)"
            @cell-dblclick="(row, column, cell, event) => emitEventHandler('cell-dblclick', row, column, cell, event)"
            @row-click="(row, event, column) => emitEventHandler('row-click', row, event, column)"
            @row-dblclick="(row, event) => emitEventHandler('row-dblclick', row, event)"
            @row-contextmenu="(row, event) => emitEventHandler('row-contextmenu', row, event)"
            @header-click="(column, event) => emitEventHandler('header-click', column, event)"
            @sort-change="sortChange"
            @filter-change="filters => emitEventHandler('filter-change', filters)"
            @current-change="
                (currentRow, oldCurrentRow) => emitEventHandler('current-change', currentRow, oldCurrentRow)
            "
            @header-dragend="
                (newWidth, oldWidth, column, event) =>
                    emitEventHandler('header-dragend', newWidth, oldWidth, column, event)
            "
            @expand-change="(row, expanded) => emitEventHandler('expand-change', row, expanded)"
        >
            <slot name="prepend"></slot>

            <el-table-column
                v-for="(column, columnIndex) in columnsVisible"
                :key="columnIndex"
                :column-key="column.columnKey"
                :type="column.type"
                :prop="column.prop"
                :label="column.label"
                :width="column.minWidth ? '-' : column.width"
                :minWidth="column.minWidth || column.width"
                :fixed="column.fixed"
                :render-header="column.renderHeader"
                :sortable="typeof column.sortable === 'undefined' ? true : column.sortable"
                :sort-method="column.method || sortMethod"
                :resizable="column.resizable || false"
                :formatter="column.formatter"
                :show-overflow-tooltip="column.showOverflowTooltip"
                :align="column.align"
                :header-align="column.headerAlign || column.align"
                :class-name="column.className"
                :label-class-name="column.labelClassName"
                :selectable="column.selectable"
                :reserve-selection="column.reserveSelection"
                :filters="column.filters"
                :filter-placement="column.filterPlacement"
                :filter-multiple="column.filterMultiple"
                :filter-method="column.filterMethod"
                :filtered-value="column.filteredValue"
            >
                <template v-if="!column.type">
                  <template v-if="column.slotHeader" slot-scope="scope" :slot="column.slotHeader ? 'header' : null">
                    <slot :name="column.slotHeader" :row="scope.row" :$index="scope.$index"></slot>
                  </template>
                  <template slot-scope="scope" :scope="newSlotScope ? 'scope' : false">
                      <span v-if="column.filter">
                          {{ Vue.filter(column['filter'])(scope.row[column.prop]) }}
                      </span>
                      <span v-else-if="column.slotName">
                          <slot :name="column.slotName" :row="scope.row" :$index="scope.$index"></slot>
                      </span>
                      <span v-else>
                          {{ column.render ? column.render(scope.row) : scope.row[column.prop] }}
                      </span>
                  </template>
                </template>
            </el-table-column>

            <slot name="append"></slot>
        </el-table>

        <div v-if="showPagination" class="grid-table__pagination">
            <el-pagination
                @size-change="handleSizeChange"
                @current-change="handleCurrentChange"
                background
                :current-page="pagination.pageIndex"
                :page-sizes="pageSizes"
                :page-size="pagination.pageSize"
                :layout="paginationLayout"
                :total="total"
            >
            </el-pagination>
        </div>
    </div>
</template>

<script>
import Vue from 'vue';
import props from './props';
import searchForm from '../../el-grid-search/src/main.vue';
import { orderBy, compare } from 'natural-orderby';

export default {
    name: 'ElGrid',
    components: {
        searchForm
    },
    props,
    data() {
        const _this = this;
        return {
            Vue,
            // loading: this.loading || false,
            pagination: {
                pageIndex: 1,
                pageSize: (() => {
                    const { pageSizes } = _this;
                    if (pageSizes.length > 0) {
                        return pageSizes[0];
                    }
                    return 20;
                })()
            },
            total: 0,
            tableData: [],
            cacheLocalData: [],
            sortLocalData: [],
            sortParams: {
                sortField: null,
                sortOrder: null
            }
        };
    },
    computed: {
        newSlotScope() {
            return Number(Vue.version.replace(/\./g, '')) >= 250;
        },
        columnsVisible() {
            return this.columns.filter(column => (column.visible !== undefined ? column.visible : true));
        }
    },
    methods: {
        handleSizeChange(size) {
            this.pagination.pageSize = size;
            this.dataChangeHandler();
        },
        handleCurrentChange(pageIndex) {
            this.pagination.pageIndex = pageIndex;
            this.dataChangeHandler();
        },
        searchHandler(resetPageIndex = true) {
            if (resetPageIndex) {
                this.pagination.pageIndex = 1;
            }
            this.dataChangeHandler(arguments[0]);
        },
        dataChangeHandler() {
            const { type } = this;
            if (type === 'local') {
                this.dataFilterHandler(arguments[0]);
            } else if (type === 'remote') {
                this.fetchHandler(arguments[0]);
            }
        },
        dataFilter(data) {
            const { pageIndex, pageSize } = this.pagination;
            return data.filter((_v, i) => i >= (pageIndex - 1) * pageSize && i < pageIndex * pageSize);
        },
        sortChange({ prop: sortField, order: sortOrder }) {
            const { type, localOrderBy, cacheLocalData, dataFilter } = this;
            this.sortParams = { sortField, sortOrder };

            if (type === 'local') {
                if (sortOrder) {
                    this.sortLocalData = localOrderBy(cacheLocalData, sortField, sortOrder);
                    this.tableData = dataFilter(this.sortLocalData);
                } else {
                    this.tableData = dataFilter(this.cacheLocalData);
                }
            } else if (type === 'remote') {
                this.fetchHandler();
            }
        },
        localOrderBy(data, field, order) {
            if (!field) return data;
            order = order === 'ascending' ? 'asc' : order === 'descending' ? 'desc' : null;
            let [sorted, unsorted] = [data.filter(v => v[field] !== null), data.filter(v => v[field] === null)];
            sorted = orderBy(sorted, [v => v[field]], [order]);
            return [...sorted, ...unsorted];
        },
        sortMethod(a, b) {
            const { sortOrder } = this.sortParams;
            return [a, b].sort(compare({ order: sortOrder }));
        },
        getParamsSearch() {
            const searchForm = this.$refs['searchForm'];
            let paramFuzzy;

            if (searchForm) {
                paramFuzzy = searchForm.getParamFuzzy();
            }

            return paramFuzzy;
        },
        dataFilterHandler(formParams) {
            this.loading = true;
            const { cacheLocalData, sortLocalData, params, pagination } = this;
            const mergeParams = Object.assign(params, formParams);
            const validParamKeys = Object.keys(mergeParams).filter(v => {
                return mergeParams[v] !== undefined && mergeParams[v] !== '';
            });
            const searchForm = this.$refs['searchForm'];
            let paramFuzzy;

            if (searchForm) {
                paramFuzzy = searchForm.getParamFuzzy();
            }

            if (validParamKeys.length > 0) {
                const validData = cacheLocalData.filter(v => {
                    let valids = [];
                    validParamKeys.forEach(vv => {
                        // if (typeof v[vv] === 'number') {
                        valids.push(
                            paramFuzzy && paramFuzzy[vv]
                                ? String(v[vv])
                                      .toLowerCase()
                                      .trim()
                                      .indexOf(
                                          String(mergeParams[vv])
                                              .toLowerCase()
                                              .trim()
                                      ) !== -1
                                : String(v[vv])
                                      .toLowerCase()
                                      .trim() ===
                                      String(mergeParams[vv])
                                          .toLowerCase()
                                          .trim()
                        );
                        // } else {
                        // valids.push(
                        //   paramFuzzy && paramFuzzy[vv] ? ((v[vv]).indexOf(mergeParams[vv]) !== -1) : ((v[vv]) === mergeParams[vv])
                        // )
                        // }
                    });
                    return valids.every(vvv => {
                        return vvv;
                    });
                });

                this.tableData = this.dataFilter(validData);
                this.total = validData.length;
                this.loading = false;
            } else {
                const { sortOrder } = this.sortParams;
                this.total = cacheLocalData.length;
                this.tableData = this.dataFilter(sortOrder ? sortLocalData : cacheLocalData);
                this.loading = false;
            }
        },

        getSortParams() {
            const { sortFieldKey, orderSortKey, orderAscKey, orderDescKey, sortParams } = this;

            const orderKey =
                sortParams.sortOrder === 'ascending'
                    ? orderAscKey
                    : 'descending';

            return {
                [sortFieldKey]: sortParams.sortField,
                [orderSortKey]: orderKey
            };
        },

        fetchHandler(formParams = {}) {
            this.loading = true;
            let {
                fetch,
                method,
                url,
                axios,
                headers,
                listField,
                pageIndexKey,
                pageSizeKey,
                totalField,
                params,
                payload,
                showPagination,
                pagination
            } = this;

            const sortParams = this.getSortParams();

            params = JSON.parse(JSON.stringify(Object.assign(params, formParams, sortParams, payload)));
            let pageIndexInfo = {
                [pageIndexKey]: pagination.pageIndex,
                [pageSizeKey]: pagination.pageSize
            };

            if (showPagination) {
                params = Object.assign(params, pageIndexInfo);
            }

            let requestObject = null;

            if (fetch) {
                requestObject = fetch(params);
            } else {
                method = method.toLowerCase();
                url = url.replace(/\{([^\}]+)\}/g, (v, c) => pageIndexInfo[c]);
                if (method === 'get') {
                    requestObject = axios[method](url, { params });
                } else {
                    requestObject = axios[method](url, params);
                }
            }

            requestObject
                .then(response => {
                    let result = response;

                    if (response && !(response instanceof Array)) {
                        if (listField && listField.indexOf('.') !== -1) {
                            listField.split('.').forEach(vv => {
                                result = result[vv];
                            });
                        } else {
                            result = response[listField];
                        }
                    }

                    result = result || [];

                    if (!result || !(result instanceof Array)) {
                        this.loading = false;
                        throw new Error(`The result of key:${listField} is not Array.`);
                    }

                    if (this.dataHandler) {
                        this.tableData = result.map(this.dataHandler);
                    } else {
                        this.tableData = result;
                    }

                    let totalValue = response;
                    if (Object.prototype.toString.call(response) === '[object Array]') {
                        totalValue = response.length;
                    } else if (typeof response === 'object') {
                        if (totalField && totalField.indexOf('.') !== -1) {
                            totalField.split('.').forEach(vv => {
                                totalValue = totalValue[vv];
                            });
                        } else {
                            totalValue = response[totalField];
                        }
                    } else {
                        totalValue = 0;
                    }
                    this.total = totalValue;

                    this.loading = false;
                })
                .catch(error => {
                    // console.error('Get remote data failed. ', error)
                    this.loading = false;
                });
        },
        emitEventHandler(event) {
            if (event === 'row-click') {
                const row = arguments[1];
                this.expandRowByClick && this.$emit('row-click', this.$refs['table'].toggleRowExpansion(row));
            }
            this.$emit(event, ...Array.from(arguments).slice(1));
        },
        loadLocalData(data) {
            const { autoload } = this;
            if (!data) {
                this.showPagination = false;
                throw new Error(`When the type is 'local', you must set attribute 'data' and 'data' must be a array.`);
            }
            const cacheData = JSON.parse(JSON.stringify(data));
            this.cacheLocalData = cacheData;
            this.sortLocalData = cacheData;
            if (autoload) {
                this.tableData = this.dataFilter(cacheData);
                this.total = cacheData.length;
            }
        },
        refresh() {
            this.searchHandler(arguments[0]);
        },
        clearForm() {
            this.$refs['searchForm'].resetForm();
        },
        resetFields() {
            this.$refs['searchForm'].resetFields();
        },
        getDataSearchForm() {
            return this.$refs['searchForm'].params;
        }
    },
    mounted() {
        this.$refs['table'].$on('expand', (row, expanded) => this.emitEventHandler('expand', row, expanded));
        const { type, autoload, data, formOptions, params, payload } = this;

        if (type === 'remote' && autoload) {
            if (formOptions) {
                this.$refs['searchForm'].getParams((error, formParams) => {
                    if (!error) {
                        this.fetchHandler(Object.assign(formParams, params));
                    }
                });
            } else {
                this.fetchHandler(Object.assign(payload, params));
            }
        } else if (type === 'local') {
            this.loadLocalData(data || []);
        }
    },
    watch: {
        data: function(value) {
            this.loadLocalData(value);
        }
    }
};
</script>
