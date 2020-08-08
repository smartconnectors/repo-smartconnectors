import { Pipe, PipeTransform } from '@angular/core';
import { isFinite } from 'lodash';
import { LabelConstants, NUMERIC_TYPES, NUMERIC_SEPARATORS_MAP, NUMERIC_SEPARATORS } from '~/constants/component.constant';


@Pipe({
    name: 'numeric'
})
export class NumericPipe implements PipeTransform {
    transform(value: any, ...args: any[]): any {

        const noData = (args && args[2]) || LabelConstants.NO_DATA;
        if (!value && value !== 0) { return noData; }
        value = +value;

        const type = (args && args[0]) || NUMERIC_TYPES.DECIMAL;
        const decimals = +(args && args[1]) || 0;
        const isDifference = (args && args[3]) || false;

        if (!isFinite(value)) { return noData; }

        const transformFn = numericTransforms[type];
        return transformFn && typeof transformFn === 'function' ? transformFn(value, decimals) : value;
    }
}


// tslint:disable-next-line:max-line-length
const doTransform = (value: number = 0, decimals: number = 0, leftSymbol: string = '', rightSymbol: string = '', placeLeft: boolean = false, placeRight: boolean = false,
) => {
    let minusSign = '';
    // this code change is to show minus sign before the $
    // $-1,066 will be displayed as -$1,066
    if (leftSymbol === '$' && value && value < 0) {
        minusSign = '-';
        value = Math.abs(value);
    }

    if (rightSymbol === 'K' && value && value > 999) {
        value = value / 1000;
        rightSymbol = 'M';
    }

    // tslint:disable-next-line:max-line-length
    return `${placeLeft ? minusSign + leftSymbol : minusSign}${value.toFixed(decimals).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,')}${placeRight ? rightSymbol : ''}`;
};

type numericTransformFn = (number, decimals) => string;

const numericTransforms: { [key: string]: numericTransformFn } = {
    [NUMERIC_TYPES.DECIMAL]: (a: number, b: number) => doTransform(a, b, '', '', false, false),
    [NUMERIC_TYPES.PERCENTAGE]: (a: number, b: number) => doTransform(100 * a, b, '', '%', false, true),
    [NUMERIC_TYPES.CURRENCY]: (a: number, b: number) => doTransform(a, b, '$', '', true, false),
    [NUMERIC_TYPES.THOUSAND]: (a: number, b: number) => shorten(a, b),
    [NUMERIC_TYPES.COMMA_SEPARATED]: (a: number, b: number) => doTransform(a, b, '', '', true, false),
};

const shorten = (a: number, b: number) => {
    const separator = NUMERIC_SEPARATORS_MAP[Math.floor(Math.log10(a))] || NUMERIC_SEPARATORS.ONES;
    return doTransform(a / separator.divisor, b, '$', separator.symbol, true, true);
};
