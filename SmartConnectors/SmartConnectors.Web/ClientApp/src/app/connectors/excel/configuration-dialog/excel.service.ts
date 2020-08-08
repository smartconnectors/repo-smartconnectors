import { Injectable, EventEmitter } from '@angular/core';
import { HttpService } from '~/core/http';
import { environment } from 'src/environments/environment';
import { tap } from 'rxjs/operators';

@Injectable()
export class ExcelService {
    objectList = [];


    constructor(private http: HttpService) {

    }

    extractMetadata(docId) {
        let url = environment.baseUrl + `/documents/${docId}/extract-excel-metadata`;
        return this.http.get(url, true);
    }

    getDocuments() {
        let url = environment.baseUrl + `/documents`;
        return this.http.get(url, true);
    }
}