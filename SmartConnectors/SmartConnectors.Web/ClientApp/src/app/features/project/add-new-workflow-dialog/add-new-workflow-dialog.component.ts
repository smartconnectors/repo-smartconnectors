import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export interface DialogData {
  savedWorkFlows: any[];
  packageList: [];
}

@Component({
  selector: 'app-add-new-workflow-dialog',
  templateUrl: './add-new-workflow-dialog.component.html',
  styleUrls: ['./add-new-workflow-dialog.component.scss']
})
export class AddNewWorkflowDialogComponent implements OnInit {

  newWorkflowTitle: string;
  packageName: string;
  isDuplicateTitle: boolean;

  constructor(public dialogRef: MatDialogRef<AddNewWorkflowDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void {

  }

  create(): void {
    if (this.newWorkflowTitle && this.packageName) {
      this.dialogRef.close({
        newWorkflowTitle: this.newWorkflowTitle,
        packageName: this.packageName
      });
    }
  }

  close() {
    this.dialogRef.close();
  }


  onInputChange(event) {
    this.isDuplicateTitle = this.data.savedWorkFlows.filter(item => item.name == event.target.value).length !== 0;
  }
}