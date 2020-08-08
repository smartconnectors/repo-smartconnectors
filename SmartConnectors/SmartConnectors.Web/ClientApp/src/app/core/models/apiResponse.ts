export class ApiResponse<T> {
    meta: Meta[];
    data: Array<T>;
    links: Link[];
    errors: Error[];
}

export class Meta {
    key: string;
    value: any;
}

export class Link {
    key: string;
    value: string;
}
