export function SET_STATE(context, data) {
  if (Array.isArray(data)) {
    data.map(({ state, payload }) => (context[state] = payload))
  } else if (!Object.keys(data).includes('state')) {
    Object.entries(data).forEach(([state, value]) => (context[state] = value))
  } else {
    const { state, payload } = data
    context[state] = payload
  }
}

export const setState = (commit, data) => commit('SET_STATE', data)

export function getInfoById(state) {
  return (id, stateName, key = 'id') =>
    state[stateName]?.find(item => item[key] === id) || {}
}

export const getSexType = (value, type = 'id') => {
  const [male, female] =
    type === 'id' ? [1, 2] : [this.$t('user.male'), this.$t('user.female')]

  return value === 1 ? male : value === 2 ? female : null
}
