import { HttpService } from '~/core/http';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'app-dashboard-component',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements OnInit {
    public reportDefectForm: FormGroup;

    public constructor(private httpService: HttpService) {

    }

    public ngOnInit(): void {
        this.checkFileStatus();
    }

    public checkFileStatus() {
        // this.httpService.get('/api/document/check-file-status').subscribe((res) => {
        //     debugger;
        // });
    }
}
