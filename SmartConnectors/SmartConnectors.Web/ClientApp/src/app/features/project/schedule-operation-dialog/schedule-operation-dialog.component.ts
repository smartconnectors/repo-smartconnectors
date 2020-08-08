import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ProjectService } from '../project.service';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';

export interface DialogData {
  workflowId: number;
}

@Component({
  selector: 'app-schedule-operation-dialog',
  templateUrl: './schedule-operation-dialog.component.html',
  styleUrls: ['./schedule-operation-dialog.component.scss']
})
export class ScheduleOperationDialogComponent implements OnInit {
  schedulerForm: FormGroup;
  schedulerRequest: any;
  schedulerRepeatOptions: [];
  selectedWorkflowId: number;
  showEditForm: boolean;
  selectedSchedule: any;
  savedSchedules: any[] = [];
  name = 'Angular';
  private exportTime = { hour: 7, minute: 15, meriden: 'PM', format: 12 };

  constructor(private fb: FormBuilder, private projectService: ProjectService,
    public dialogRef: MatDialogRef<ScheduleOperationDialogComponent>,
    private _snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {

    this.schedulerRequest = {
      isRepeated: false
    };

    if (data && data.workflowId) {
      this.selectedWorkflowId = data.workflowId;

      this.getSavedSchedules();
    }


    this.buildForm();
  }

  ngOnInit() {


    this.getRepeatOptions();
  }

  buildForm() {
    this.schedulerForm = this.fb.group({
      id: [this.schedulerRequest.id ? this.schedulerRequest.id : 0],
      name: [this.schedulerRequest.name],
      startDate: [this.schedulerRequest.startDate],
      endDate: [this.schedulerRequest.endDate],
      startTime: [this.schedulerRequest.startTime],
      endTime: [this.schedulerRequest.endTime],
      isRepeated: [this.schedulerRequest.isRepeated],
      repeatOptionId: [this.schedulerRequest.repeatOptionId]
    });

    this.schedulerForm.valueChanges.subscribe(res => {
      this.schedulerRequest = res;
    });
  }

  getRepeatOptions() {
    this.projectService.getRepeatOptions().subscribe((res) => {
      this.schedulerRepeatOptions = res;
    });
  }

  getSavedSchedules() {
    this.projectService.getSavedSchedules(this.selectedWorkflowId).subscribe(res => {
      this.savedSchedules = res;
    })
  }

  editSchedule(event, item) {
    this.showEditForm = true;
    this.schedulerRequest = item;
    this.schedulerForm.patchValue(item);
  }

  deleteSchedule(event, item) {
    if (item.id) {
      this.projectService.removeSchedule(item.id, this.selectedWorkflowId).subscribe(res => {
        this.getSavedSchedules();
      });
    }
  }

  saveSchedule() {

    if (this.schedulerRequest && this.schedulerRequest.id) {
      this.projectService.updateScheduleOperation(this.schedulerRequest.id, this.selectedWorkflowId, this.schedulerRequest).subscribe(res => {
        this.openSnackBar("Schedule", "Updated");
        this.getSavedSchedules();
      });
    } else {
      this.projectService.createScheduleOperation(this.selectedWorkflowId, this.schedulerRequest).subscribe(res => {
        this.openSnackBar("Schedule", "Created");
        this.getSavedSchedules();
      });
    }
  }

  newSchedule(event) {
    this.schedulerRequest = {};
    this.showEditForm = true;
  }

  onClose() {
    this.dialogRef.close();

  }

  onReset() {

  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
