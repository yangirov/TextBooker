import cloneDeep from 'lodash/cloneDeep'
import merge from 'lodash/merge'
import sortBy from 'lodash/sortBy'
import orderBy from 'lodash/orderBy'
import get from 'lodash/get'
import setWith from 'lodash/setWith'
import keyBy from 'lodash/keyBy'
import difference from 'lodash/difference'
import debounce from 'lodash/debounce'
import isEmpty from 'lodash/isEmpty'
import union from 'lodash/union'
import isEqual from 'lodash/isEqual'
import values from 'lodash/values'

const lodash = {
  cloneDeep,
  merge,
  sortBy,
  orderBy,
  get,
  setWith,
  keyBy,
  difference,
  debounce,
  isEmpty,
  union,
  isEqual,
  values
}

window.lodash = lodash
window._ = lodash

export { lodash }
