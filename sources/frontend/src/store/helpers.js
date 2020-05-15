export function SET_STATE(context, data) {
    if (Array.isArray(data)) {
        data.map(({ state, payload }) => (context[state] = payload));
    } else if (!Object.keys(data).includes('state')) {
        Object.entries(data).forEach(([state, value]) => (context[state] = value));
    } else {
        const { state, payload } = data;
        context[state] = payload;
    }
}

export const setState = (commit, data) => commit('SET_STATE', data);

/**
 * Getters-функция для получение данных из массива по выбранному id - поиск по выбранному ключу
 * @param state
 * @returns {function(*, *, *=): *|{}}
 */
export function getInfoById(state) {
    /**
     * @param {number|string} id - какой идетификатор найти
     * @param {string} stateName - строка выбранного списка из getters
     * @param {key=} key - с каким ключем произвести сравнение
     *
     * @example getInfoById(this.form.userId, 'usersList') => { name, id, role }
     */
    return (id, stateName, key = 'id') => state[stateName]?.find(item => item[key] === id) || {};
}

/**
 * Преобразовать тип данных «Пол пацинета» к формату строки или id (1 или «мужской»)
 * @param {number|null} value - получаемое значение
 * @param {'id'|'name'} type - тип вывода
 * @returns {string|number}
 */
export const getSexType = (value, type = 'id') => {
    const [male, female] = type === 'id' ? [1, 2] : ['мужской', 'женский'];
    /**
     * ... получаемый формат данных из справочников разный
     * 1|4|7 => мужской
     * 2|5|8 => женский
     */
    return [1, 4, 7].includes(value) ? male : [2, 5, 8].includes(value) ? female : null;
};
