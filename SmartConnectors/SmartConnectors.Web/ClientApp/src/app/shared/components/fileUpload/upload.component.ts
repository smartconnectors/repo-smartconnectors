import { Component, ViewChild, Output, EventEmitter, Input } from '@angular/core';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http';
import { HttpService } from '~/core/http';

@Component({
    selector: 'app-file-upload-component',
    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.scss']
})

export class UploadComponent {
    @Input() set fileUploadUrlData(value) {
        if (value) {
            this.fileUploadUrl = value;
        }
    }

    @Output() fileUploaded = new EventEmitter();

    fileUploadUrl = '';
    public progress: number;
    public message: string;

    constructor(private http: HttpClient, private httpService: HttpService) { }

    upload(files) {
        if (files.length === 0) {
            return;
        }

        const formData = new FormData();

        for (const file of files) {
            formData.append(file.name, file);
        }

        const uploadReq = new HttpRequest('POST', this.fileUploadUrl, formData, {
            reportProgress: true,
        });

        this.http.request(uploadReq).subscribe(event => {
            if (event.type === HttpEventType.UploadProgress) {
                this.progress = Math.round(100 * event.loaded / event.total);
            } else if (event.type === HttpEventType.Response) {
                this.fileUploaded.emit(event.body.toString());
            }
        });
    }
}
