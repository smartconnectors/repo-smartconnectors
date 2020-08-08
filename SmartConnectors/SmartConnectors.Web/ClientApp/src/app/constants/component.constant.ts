
export const DefaultPagination = {
    pageSize: 25,
    pageNumber: 1,
    pageSizes: [25, 50, 75, 100, 250]
};


export const LabelConstants = {
    NO_DATA: '-'
};

export const COLUMN_TYPE = {
    CURRENCY: 'currency',
    DATE: 'date',
    PERCENTAGE: 'percentage',
    DECIMAL: 'decimal',
    STRING: 'string',
    THOUSAND: 'thousand',
    NUMBER: 'number',
    PROPERTY_ORDER: 'order',
    POINTS: 'points'
};

export const GRID_HEIGHT = {
    AUTO: 'auto',
    FULL: 'full'
};

export const NUMERIC_TYPES = {
    DECIMAL: 'decimal',
    PERCENTAGE: 'percentage',
    CURRENCY: 'currency',
    THOUSAND: 'thousand',
    COMMA_SEPARATED: 'commaSeparated'
};

export const NUMERIC_SEPARATORS = {
    ONES: { divisor: 1e0, symbol: '' },
    THOUSANDS: { divisor: 1e3, symbol: 'K' },
    MILLIONS: { divisor: 1e6, symbol: 'M' },
    BILLIONS: { divisor: 1e9, symbol: 'B' }
};

export const NUMERIC_SEPARATORS_MAP = {
    0: NUMERIC_SEPARATORS.ONES,
    1: NUMERIC_SEPARATORS.ONES,
    2: NUMERIC_SEPARATORS.ONES,
    3: NUMERIC_SEPARATORS.THOUSANDS,
    4: NUMERIC_SEPARATORS.THOUSANDS,
    5: NUMERIC_SEPARATORS.THOUSANDS,
    6: NUMERIC_SEPARATORS.MILLIONS,
    7: NUMERIC_SEPARATORS.MILLIONS,
    8: NUMERIC_SEPARATORS.MILLIONS,
    9: NUMERIC_SEPARATORS.BILLIONS,
    10: NUMERIC_SEPARATORS.BILLIONS,
    11: NUMERIC_SEPARATORS.BILLIONS,
    12: NUMERIC_SEPARATORS.BILLIONS
};

export const ORGANIZATION_TYPES = [
    'Registered Vendor',
    'Client'
];

export const EMAIL_PATTERN = new RegExp('^[^\s@]+@[^\s@]+\.[^\s@]{2,}$');

export const PHONE_PATTERN = new RegExp('^(1\s?)?((\([0-9]{3}\))|[0-9]{3})[\s\-]?[\0-9]{3}[\s\-]?[0-9]{4}$');

export const AUTHORIZED_MENU = [{
    id: 'home',
    url: '/home',
    title: 'Home',
    isSelected: true
}, {
    id: 'dashboard',
    url: '/dashboard',
    title: 'mDocs',
    isSelected: false
}, {
    id: 'search',
    url: '/search',
    title: 'Search',
    isSelected: false
}, {
    id: 'analyzers',
    url: '/analyzers',
    title: 'Analyzer',
    isSelected: false
}, {
    id: 'reports',
    url: '/reports',
    title: 'Reports',
    isSelected: false
}, {
    id: 'contactus',
    url: '/contactus',
    title: 'Contact US',
    isSelected: false
}];

export const UNAUTHORIZED_MENU = [{
    id: 'home',
    url: '/home',
    title: 'Home',
    isSelected: true
}, {
    id: 'contactus',
    url: '/contactus',
    title: 'Contact US',
    isSelected: false
}];

export enum OperationStep{
    SalesforceObjectSelection,
    SaleforceQuery,
    TransformationInput,
    TransformationOutput,
    TransformationPayload,
    ExcelDownload
}