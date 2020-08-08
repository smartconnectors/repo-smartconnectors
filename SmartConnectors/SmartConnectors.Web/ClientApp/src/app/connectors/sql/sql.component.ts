import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-sql',
  templateUrl: './sql.component.html',
  styleUrls: ['./sql.component.scss']
})
export class SqlComponent implements OnInit {

  @Input() set outputOperationData(value) {
    if (value) {
      this.outputOperations = value;

    } else {
      this.outputOperations = [];
    }
  }

  @Input() set inputOperationData(value) {
    if (value) {
      this.inputOperations = value;
    }
  }

  @Input() set stepData(value) {
    if (value) {
      this.stepCount = value;
    }
  }

  @Output() scriptChanged = new EventEmitter();
  @Output() operationStateChanged = new EventEmitter();

  connectionForm: FormGroup;
  public data: any = [];
  sourceTypes: [];
  inputOperations: any[] = [];
  outputOperations: any[] = [];
  payloads: any[] = [];
  stepCount: number;

  constructor(private fb: FormBuilder) {
    this.connectionForm = this.fb.group({
      sourceName: [''],
      sourceType: [''],
      serverName: [''],
      databaseName: [''],
      severLogin: [''],
      serverPassword: ['']
    });
  }

  ngOnInit() {
  }

  onConnectionDetailSubmit() {

  }
}
