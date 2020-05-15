export function flatObject(dto, fields = []) {
    let outer = {};
    let inner = {};

    Object.keys(dto).forEach(key => {
        if (dto[key] == null) return;

        let item = dto[key];
        fields && fields.includes(key) ? (inner = flatObject(item)) : (outer[key] = item);
    });

    return { ...outer, ...inner };
}

/**
 * Множественное значение
 * @example pluralize(['год','года','лет'], 'Меньше года')(0) => "Меньше года"
 * @example pluralize(['год','года','лет'])(4) => "4 года"
 *
 * @param {array} titles
 * @param {string} empty
 * @returns {Function}
 */
export function pluralize(titles, empty) {
    const cases = [2, 0, 1, 1, 1, 2];
    return count => {
        count = Math.abs(count); // для отрицательных чисел
        if (count === 0 && empty) return empty;
        const title = titles[count % 100 > 4 && count % 100 < 20 ? 2 : cases[count % 10 < 5 ? count % 10 : 5]];
        return `${count} ${title}`;
    };
}

export const getYearPluralize = pluralize(['год', 'года', 'лет'], 'Меньше года');
