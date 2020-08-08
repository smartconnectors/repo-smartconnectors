import { Component, OnInit, Inject } from '@angular/core';
import { ProjectService } from '../project.service';
import { MAT_DIALOG_DATA } from '@angular/material';
import { DialogData } from '../schedule-operation-dialog/schedule-operation-dialog.component';

export interface IOperationLog {
  message: string;
  content: number;
  status: string;
  createdDate: number;
  createdBy: string;
}

@Component({
  selector: 'app-show-operation-summary-dialog',
  templateUrl: './show-operation-summary-dialog.component.html',
  styleUrls: ['./show-operation-summary-dialog.component.scss']
})
export class ShowOperationSummaryDialogComponent implements OnInit {

  workflowId: number;
  displayedColumns: string[] = ['message', 'content', 'status', 'createdDate'];
  dataSource: IOperationLog[];

  constructor(private projectService: ProjectService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {

    this.workflowId = data.workflowId;
  }

  ngOnInit() {
    this.getSummary();
  }

  getSummary() {
    this.projectService.getOperationLogs(this.workflowId).subscribe((res) => {
      this.dataSource = res;
    });
  }
}
