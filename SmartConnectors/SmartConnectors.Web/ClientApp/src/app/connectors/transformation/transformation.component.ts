import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { JsonEditorOptions, JsonEditorComponent } from 'ang-jsoneditor';
import { debug } from 'util';

declare var LeaderLine: any;

@Component({
  selector: 'app-transformation',
  templateUrl: './transformation.component.html',
  styleUrls: ['./transformation.component.scss']
})
export class TransformationComponent implements OnInit {
  @Input() set outputOperationData(value) {
    if (value) {
      this.outputOperations = value;

      this.autoGeneratePayload();
    } else {
      this.outputOperations = [];
    }
  }

  @Input() set inputOperationData(value) {
    if (value) {
      this.inputOperations = value;
      this.autoGeneratePayload();
    } else {
      this.inputOperations = [];
    }
  }

  @Input() set transformationData(value) {
    if (value) {
      this.transformationDetails = value;
      this.autoGeneratePayload();
    }
  }

  @Input() set savedOperationsData(value) {
    if (value) {
      setTimeout(() => {
        this.savedOperations = value;
        this.setPageData();
      }, 1000);
    }
  }

  @Input() set stepData(value) {
    if (value) {
      this.stepCount = value;
    }
  }

  @Output() transformationChanged = new EventEmitter();
  @Output() operationStateChanged = new EventEmitter();
  @Output() actionSelected = new EventEmitter();

  @ViewChild(JsonEditorComponent, { static: true }) editor: JsonEditorComponent;
  @ViewChild(JsonEditorComponent, { static: true }) payloadEditor: JsonEditorComponent;

  public editorOptions: JsonEditorOptions;
  public payloadEditorOptions: JsonEditorOptions;
  public payloadData: any;
  public scriptData: any;
  public transformationDetails: any;

  inputOperations: any[] = [];
  outputOperations: any[] = [];
  payloads: any[] = [];
  savedOperations: any[];
  stepCount: number;

  constructor() {
    this.editorOptions = new JsonEditorOptions()
    this.editorOptions.modes = ['code']; // set all allowed modes
    this.editorOptions.mode = 'code';

    this.payloadEditorOptions = new JsonEditorOptions()
    this.payloadEditorOptions.modes = ['code']; // set all allowed modes
    this.payloadEditorOptions.mode = 'code';

  }

  ngOnInit() {


  }

  setPageData() {
    this.savedOperations.forEach(item => {
      if (item.operationTypeId == 15) {
        this.inputOperations = JSON.parse(item.content);
      } else if (item.operationTypeId == 16) {
        this.outputOperations = JSON.parse(item.content);
      } else if (item.operationTypeId == 17) {
        this.payloads = JSON.parse(item.content);
      }
    });
    this.autoGeneratePayload();
  }

  autoGeneratePayload() {
    this.payloads = [];
    this.inputOperations.forEach(input => {
      this.outputOperations.forEach(output => {
        const objName = output.name.toLowerCase();
        const obj = {};
        obj[objName] = objName;
        if (input.name.toLowerCase() == output.name.toLowerCase()) {
          this.payloads.push(obj);
        }
      })
    });

    this.payloadData = this.payloads
  }

  drop(event) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex);
    }
  }

  copy() {
    this.inputOperations.forEach((item) => {
      this.outputOperations.push({
        item: 'input.' + item
      });
    });

    this.saveOperationState();

    this.payloadData = this.outputOperations
  }

  onScriptJsonEditorChange(event) {
    this.transformationChanged.emit({
      input: this.inputOperations,
      output: this.outputOperations,
      payload: this.payloadData,
      script: this.scriptData
    });

    this.saveOperationState();
  }

  onPayloadJsonEditorChange(event) {
    this.transformationChanged.emit({
      input: this.inputOperations,
      output: this.outputOperations,
      payload: this.payloadData,
      script: this.scriptData
    });

    this.saveOperationState();
  }

  onMappingChanged(event) {
    this.payloads = [];

    let payloadObj = {};
    if (event && event.length) {

      event.forEach(element => {
        let inputIndex = element.inputMapping.split('-')[1];
        let outputIndex = element.outputMapping.split('-')[2];

        payloadObj[this.inputOperations[inputIndex].name] = this.outputOperations[outputIndex].name;
      });

      this.payloads.push(payloadObj);
      this.payloadData = { "payload": payloadObj }
      this.buildSqlScriptQuery();

      this.transformationChanged.emit({
        input: this.inputOperations,
        output: this.outputOperations,
        payload: this.payloadData,
        script: this.scriptData
      });
    }
  }

  buildSqlScriptQuery() {
    var sql = 'SELECT * FROM ' + this.transformationDetails.tableName;
    var columns = [];

    this.payloads.forEach(element => {
      Object.keys(element).forEach((item) => {
        if (item) {
          columns.push(element[item]);
        }
      });
    });

    if (columns.length) {
      sql = sql.replace('*', columns.join(','));
    }

    this.scriptData = sql;
  }

  onNext(event) {
    this.saveOperationState();
    this.actionSelected.emit({
      pos: 2
    });
  }

  saveOperationState() {
    this.operationStateChanged.emit({
      type: 'Transformation Input',
      content: this.inputOperations,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Transformation Output',
      content: this.outputOperations,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Transformation Payload',
      content: this.payloadData,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Transformation Script',
      content: this.scriptData,
      stepCount: this.stepCount
    });
  }
}
