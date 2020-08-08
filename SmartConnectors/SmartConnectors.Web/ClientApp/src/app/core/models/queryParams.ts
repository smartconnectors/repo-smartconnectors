import { DefaultPagination } from "~/constants/component.constant";
import { HttpParams } from "@angular/common/http";

export class Page{
    size: number;
    number: number;

    constructor(){
        this.size = DefaultPagination.pageSize;
        this.number = 1;
    }
}

export class Sort{
    asc: string;
    desc: string;

    constructor(){
        this.asc = '';
        this.desc = '';
    }
}

export class QueryParams {
    public clientId: string;
    public statusId: string;
    public applicationId: string;
    public propertyId : string;
    public sort: Sort;
    public page: Page;    

    constructor() {
        this.statusId = '';
        this.clientId = '';

        this.page = new Page();
        this.sort = new Sort();
    }
}