import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-script-editor',
  templateUrl: './script-editor.component.html',
  styleUrls: ['./script-editor.component.scss']
})
export class ScriptEditorComponent implements OnInit {
  queryForm: FormGroup;

  @Input() set objectListData(value: any) {
    this.objectList = value;
  }

  objectList = [];


  constructor(private formBuilder: FormBuilder, ) { }

  ngOnInit() {

  
  }

  onSubmit() {

  }
}
