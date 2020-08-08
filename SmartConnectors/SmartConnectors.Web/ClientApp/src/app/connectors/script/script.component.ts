import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { JsonEditorComponent, JsonEditorOptions } from 'ang-jsoneditor';

@Component({
  selector: 'app-script',
  templateUrl: './script.component.html',
  styleUrls: ['./script.component.scss']
})
export class ScriptComponent implements OnInit {

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

  @ViewChild(JsonEditorComponent, { static: true }) editor: JsonEditorComponent;

  public editorOptions: JsonEditorOptions;
  public data: any = [];
  inputOperations: any[] = [];
  outputOperations: any[] = [];
  payloads: any[] = [];
  stepCount: number;

  constructor() {

    this.editorOptions = new JsonEditorOptions()
    this.editorOptions.modes = ['code']; // set all allowed modes
    this.editorOptions.mode = 'code';
  }

  ngOnInit() {
  }

  onScriptChange(event) {
    this.scriptChanged.emit(event);

    this.operationStateChanged.emit({
      type: 'Script',
      content: this.outputOperations,
      stepCount: this.stepCount
    });
  }

}
