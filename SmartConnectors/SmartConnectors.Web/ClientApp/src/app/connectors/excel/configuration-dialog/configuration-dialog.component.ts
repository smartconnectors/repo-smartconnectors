import { Component, OnInit, Inject, Input, Output, EventEmitter } from '@angular/core';
import { MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { environment } from 'src/environments/environment';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ExcelService } from './excel.service';

@Component({
  selector: 'app-excel-configuration',
  templateUrl: './configuration-dialog.component.html',
  styleUrls: ['./configuration-dialog.component.scss']
})
export class ConfigurationDialogComponent implements OnInit {

  @Input() set operationData(value) {
    if (value) {
      this.connectorType = value.connectorType;
      this.connectorPos = value.connectorPos;

      this.workflowId = value.workflowId;
      this.excelFileUpladUrl = environment.baseUrl + `/workflows/${value.workflowId}/upload`;
    }
  }

  @Input() set stepData(value) {
    if (value) {
      this.stepCount = value;
    }
  }

  @Output() operationChanged = new EventEmitter();
  @Output() operationStateChanged = new EventEmitter();
  @Output() actionSelected = new EventEmitter();

  never: "";
  stepCount: number;
  isLinear = false;
  workflowId: number;
  excelFileUpladUrl = '';
  sourceTypes = [];
  sourceExcelMetadata = [];
  connectorType: string;
  connectorPos: string;
  documents: any[];
  fileName: string;
  isLocalFileSystem: boolean;

  constructor(
    private fb: FormBuilder,
    private excelService: ExcelService,
    private snackBar: MatSnackBar) {

  }

  ngOnInit() {


    this.getUploadedDocumnet();

  }

  onFileUpload(event) {
    this.snackBar.open('File upload', 'Succesfull', {
      duration: 2000,
    });
    this.excelService.extractMetadata(event).subscribe((res) => {
      if (res) {
        this.sourceExcelMetadata = res.columns;
        this.operationChanged.emit(res);
      }
    });
  }

  onFileSelected(docId) {
    this.excelService.extractMetadata(docId).subscribe((res) => {
      if (res) {
        this.sourceExcelMetadata = res.columns;
        this.operationChanged.emit(res);

        this.operationStateChanged.emit({
          type: 'Excel File Selection',
          content: JSON.stringify({
            documentId: docId
          }),
          stepCount: this.stepCount
        });
      }
    });
  }

  onInputChange(event) {

    if (this.fileName) {
      this.operationStateChanged.emit({
        type: 'Excel Download',
        content: {
          fileName: this.fileName + '.xlsx',
          isLocalFileSystem: this.isLocalFileSystem
        },
        stepCount: this.stepCount
      });
    }
  }

  getUploadedDocumnet() {
    this.excelService.getDocuments().subscribe((res) => {
      this.documents = res;
    })
  }

  onConnectionDetailSubmit() {

  }

  onNext(event) {
    this.saveOperationState();
    this.actionSelected.emit({
      pos: 2
    });
  }

  saveOperationState() {
    if (this.fileName) {
      this.operationStateChanged.emit({
        type: 'Excel Download',
        content: {
          fileName: this.fileName,
          isLocalFileSystem: this.isLocalFileSystem
        },
        stepCount: this.stepCount
      });
    }
  }

}
