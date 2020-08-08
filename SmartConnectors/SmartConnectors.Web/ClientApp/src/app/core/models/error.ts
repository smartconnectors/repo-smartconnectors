export interface Pointer{
    pointer: string;
}
export interface Error {
    code: string;
    detail: string;
    id: string;
    source: Pointer;
    status: string;
    title: string;
}
