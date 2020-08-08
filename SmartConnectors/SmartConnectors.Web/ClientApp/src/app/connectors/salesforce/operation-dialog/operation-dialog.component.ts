import { Component, OnInit, Inject, ViewChild, Input, Output, EventEmitter, ViewChildren } from '@angular/core';
import { MAT_DIALOG_DATA, MatStepper } from '@angular/material';
import { SalesforceService } from '../salesforce.service';
import { forkJoin, Observable } from 'rxjs';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

export interface INameLabelPair {
  name: string;
  label: string;
}

@Component({
  selector: 'app-salesforce-operation',
  templateUrl: './operation-dialog.component.html',
  styleUrls: ['./operation-dialog.component.scss']
})
export class OperationDialogComponent implements OnInit {
  @ViewChildren("stepper") myStepper: MatStepper;

  @Input() set operationData(value) {
    if (value) {
      this.token = value.token;
      this.workflowId = value.workflowId;
      this.connectorId = value.connectorId;
      this.connectorType = value.connectorType;
      this.connectorPos = value.connectorPos;
    }
  }

  @Input() set savedOperationsData(value) {
    if (value) {
      this.savedOperations = value;
      this.setPageData();
    }
  }

  @Input() set tokenData(value) {
    if (value) {
      this.token = value;
      this.getObjectList();
    }
  }


  @Input() set operationTypesData(value) {
    if (value) {
      if (this.connectorPos == 'source') {
        this.operationTypes = value.filter(item => (item.name == 'Salesforce Org' || item.name == 'Salesforce Query'));
      } else {
        this.operationTypes = value.filter(item => (
          item.name == 'Salesforce Update' ||
          item.name == 'Salesforce Insert' ||
          item.name == 'Salesforce Upsert'));
      }
    }
  }

  @Input() set stepData(value) {
    if (value) {
      this.stepCount = value;
    }
  }

  @Output() inputOperationChanged = new EventEmitter();
  @Output() outputOperationChanged = new EventEmitter();
  @Output() actionSelected = new EventEmitter();
  @Output() operationStateChanged = new EventEmitter();

  filteredOptions: Observable<string[]>;
  parentOrderClause: any = { sortBy: '', sortKey: '' };
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  filteredParentObjectList: any[];
  filteredChildObjectList: any[];
  stepCount: number;
  workflowId: number;
  connectorId: number;
  token: string;
  objectTypeName: string;
  selectedSalesforceQuery: string;
  selectedOperationState: any;
  operationTypes: [];
  savedOperations: any[];
  selectedSaleforceObject: string;
  connectorType: string;
  objectList: any[];
  objectListViewModel: any[];
  selectedParentObjectType: INameLabelPair = { name: '', label: '' };
  selectedChildObjectType: INameLabelPair = { name: '', label: '' };
  selectedTargetObjectType: INameLabelPair = { name: '', label: '' };
  selectedColumns: any;
  parentObjectDescription: any = {};
  childObjectDescription: any = {};
  salesforceQuery: string;
  salesforceSubQuery: string;
  showDeveloperConsole: boolean = false;
  sqlQueryTestResult: any;
  salesforceTestQuery: string;
  salesforceParentQuery: string;
  salesforceChildQuery: string;
  connectorPos: string;
  selectedTabIndex: number;
  summaryList: any[];
  targetOperationType: any;
  stepperSelectedIndex: number = 0;
  parentWhereConditions: any[] = [{}];
  childWhereConditions: any[] = [{}];
  operatorList: any[] = [
    {
      type: 'equal',
      value: '=',
      label: 'Equal'
    },
    {
      type: 'StartWith',
      value: 'Start with',
      label: 'Start With'
    },
    {
      type: 'IsNotNUll',
      value: 'Is not null'
    },
    {
      type: 'Contains',
      value: 'Contains',
      label: 'Contains'
    },
    {
      type: 'IsNull',
      value: 'Is Null',
      label: 'Is Null'
    }
  ]

  constructor(
    private salesforceService: SalesforceService) {

  }

  ngOnInit() {

    this.salesforceService.getSalesforceObjects(this.token).subscribe((res) => {
      if (res && res.sobjects) {
        this.objectList = res.sobjects;
        this.filteredParentObjectList = Object.assign([], this.objectList);
      }
    });
  }

  setPageData() {
    this.savedOperations.forEach(item => {
      if (item.operationTypeId == 6) {
        this.selectedParentObjectType.label = JSON.parse(item.content);
        this.selectedParentObjectType.name = JSON.parse(item.content);
        this.getObjectDesctiption(this.selectedParentObjectType.name, true);
      } else if (item.operationTypeId == 7) {
        this.selectedChildObjectType.label = JSON.parse(item.content);
        this.selectedChildObjectType.name = this.selectedChildObjectType.label + 's';

        this.getObjectDesctiption(this.selectedChildObjectType.label, false);
      } else if (item.operationTypeId == 10) {
        this.salesforceQuery = JSON.parse(item.content);
      } else if (item.operationTypeId == 18) {
        this.salesforceQuery = JSON.parse(item.content);
      } else if (item.operationTypeId == 19) {
        this.salesforceSubQuery = JSON.parse(item.content);
      } else if (item.operationTypeId == 20) {
        this.selectedTargetObjectType.name = JSON.parse(item.content);
        this.selectedTargetObjectType.label = JSON.parse(item.content);
      } else if (item.operationTypeId == 21) {
        const operation = this.operationTypes.filter((element: any) => element.name == JSON.parse(item.content))[0];
        this.targetOperationType = operation;
      }
    });
  }

  getObjectList() {
    this.salesforceService.getSalesforceObjects(this.token).subscribe((res) => {
      if (res && res.sobjects) {
        this.objectList = res.sobjects;
        this.objectListViewModel = Object.assign([], res.sobjects);
        this.filteredParentObjectList = Object.assign([], res.sobjects);
      }
    });
  }

  getObjectDesctiption(objectTypeName, isParent: boolean) {

    if (!objectTypeName)
      return;

    this.salesforceService.getSalesforceObjectDescription(this.token, objectTypeName).subscribe((res) => {
      if (res) {
        if (isParent) {
          this.parentObjectDescription = res;
          this.filteredChildObjectList = Object.assign([], this.parentObjectDescription.childRelationships);
          const outputList = this.parentObjectDescription.fields.map(item => {
            return {
              name: item.name,
              label: item.label,
              type: item.type
            }
          });

          this.outputOperationChanged.emit(outputList);
        } else {
          this.childObjectDescription = res;
        }

        this.setDefaultfieldSelection(isParent);
      }
    }, (err) => {
      this.salesforceService.refreshToken(this.workflowId, 1);
    });
  }

  setDefaultfieldSelection(isParent) {
    if (isParent) {
      this.parentObjectDescription.fields.map((item) => {
        item.selected = false;
        if (this.salesforceQuery && this.salesforceQuery.indexOf(item.name) > 0) {
          item.selected = true;
        }
      });

    } else {
      this.childObjectDescription.fields.map((item) => {
        item.selected = false;
        if (this.salesforceSubQuery && this.salesforceSubQuery.indexOf(item.name) > 0) {
          item.selected = true;
        }
      });
    }

  }

  onApplyParentFilters(event, conditions) {
    this.salesforceQuery = this.salesforceParentQuery + this.buildWhereClause(conditions);

    if (this.parentOrderClause && this.parentOrderClause.sortBy) {
      this.salesforceQuery = this.salesforceQuery + ' ORDER By '
        + this.parentOrderClause.sortKey + ' ' + this.parentOrderClause.sortBy;
    }
  }

  onApplyChildFilters(event, conditions) {
    this.salesforceSubQuery = this.salesforceChildQuery + this.buildWhereClause(conditions);

  }

  onAddCondition(event, conditions) {
    conditions.push({
      condition: "",
      operator: '',
      value: ''
    });
  }

  onSortBySelection(event, selection) {
    //selection.sortBy = event.target.value;
  }

  onRemoveCondition(event, conditions, index) {
    conditions.splice(index, 1);
  }

  onParentObjectSelection(event) {
    if (event) {
      this.selectedParentObjectType.name = event.option.value.name;
      this.selectedParentObjectType.label = event.option.value.label;

      this.onParentObjectTypeNameSelection(this.selectedParentObjectType.name);
    }
  }

  onParentObjectTypeInput(event) {

    if (this.objectList && this.objectList.length == 0) {
      return;
    }

    this.filteredParentObjectList = this.objectList;

    if (event.target.value) {
      this.filteredParentObjectList = this.objectList.filter(item => item.label.toLowerCase().startsWith(event.target.value.toLowerCase()));
    }
  }

  onParentObjectTypeNameSelection(item) {
    this.salesforceQuery = '';
    this.salesforceSubQuery = '';
    this.selectedChildObjectType = { name: '', label: '' };
    this.parentObjectDescription = {};
    this.childObjectDescription = {};

    this.getObjectDesctiption(item, true);

  }

  onParentSelectionChange(event, selectedOptions) {
    let selectedColumns = selectedOptions.selected.map(i => i.value);

    this.salesforceQuery = this.buildSqlQuery(this.selectedParentObjectType.name, selectedColumns);

    this.salesforceParentQuery = this.salesforceQuery;

    this.inputOperationChanged.emit(selectedColumns.map(i => i.label));
  }

  onChildObjectTypeInput(event) {

    if (this.parentObjectDescription.childRelationships.length == 0) {
      return;
    }

    this.filteredChildObjectList = Object.assign(this.parentObjectDescription.childRelationships);

    if (event.target.value) {
      this.filteredChildObjectList = this.parentObjectDescription.childRelationships.filter(item => item.childSObject.toLowerCase().startsWith(event.target.value.toLowerCase()));
    }
  }

  onChildObjectTypeNameSelection(item) {
    this.salesforceSubQuery = '';
    this.childObjectDescription = {};

    this.selectedChildObjectType.name = item.relationshipName;
    this.selectedChildObjectType.label = item.childSObject

    this.getObjectDesctiption(item.childSObject, false);
  }

  onChildObjectSelection(event) {
    //childSObject
    this.onChildObjectTypeNameSelection(event.option.value) //relationshipName
  }

  onChildSelectionChange(event, selectedOptions) {
    let selectedColumns = selectedOptions.selected.map(i => i.value);

    this.salesforceSubQuery = this.buildSqlQuery(this.selectedChildObjectType.name, selectedColumns);

    this.salesforceChildQuery = this.salesforceSubQuery;
  }

  onTargetObjectSelection(event) {
    if (event) {
      this.selectedTargetObjectType.name = event.option.value.name;
      this.selectedTargetObjectType.label = event.option.value.label;

      this.operationStateChanged.emit({
        type: 'Salesforce Target Object Selection',
        content: this.selectedTargetObjectType.name,
        stepCount: this.stepCount
      });
    }
  }

  onTargetObjectTypeInput(event) {

    if (this.objectList && this.objectList.length == 0) {
      return;
    }

    this.filteredParentObjectList = this.objectList;

    if (event.target.value) {
      this.filteredParentObjectList = this.objectList.filter(item => item.label.toLowerCase().startsWith(event.target.value.toLowerCase()));
    }
  }

  onQueryChange(value) {
    this.selectedSalesforceQuery = value;
  }

  onObjectClick(item) {
    this.selectedParentObjectType.label = item.label;
    this.selectedParentObjectType.name = item.name;
    this.selectedTabIndex = 1;

    this.onParentObjectSelection(item.label);
  }

  onNext(event) {
    this.stepperSelectedIndex = this.stepperSelectedIndex + 1;
  }

  onExecute(event) {
    this.salesforceSubQuery = "";

    this.stepperSelectedIndex = 2;
    this.buildTestSqlQuery();
  }

  onTransform(event, fromParent) {
    if (fromParent) {
      this.salesforceSubQuery = "";
      this.selectedChildObjectType = { name: '', label: '' }
    }
    this.buildTestSqlQuery();
    this.executeSalesforceQuery(false);
  }

  setOperationData() {

    this.operationStateChanged.emit({
      type: 'Salesforce Parent Query',
      content: this.salesforceQuery,
      stepCount: this.stepCount
    });

    this.operationTypes.forEach((type: any) => {
      this.savedOperations.forEach((ops: any) => {
        if (type.name == 'Salesforce Execute Query') {
          this.selectedSalesforceQuery = ops.content;
        }

        if (type.name == 'Salesforce Parent Object Selection') {
          this.selectedParentObjectType.name = ops.content;
        }
      });
    });
  }

  onOptionSelected(item) {
    this.savedOperations.forEach((ele) => {
      if (ele.operationTypeId == item.id) {
        this.selectedOperationState = {
          id: ele.id,
          operationTypeId: item.id,
          workflowId: this.workflowId
        };
      }
    });
  }

  onTargetOperationTypeChange(event) {
    this.operationStateChanged.emit({
      type: 'Salesforce Target Operation Selection',
      content: event.value.name,
      stepCount: this.stepCount
    });
  }

  executeSalesforceQuery(fromConsole) {

    this.salesforceService.executeSalesforceQuery(this.token, this.selectedSalesforceQuery).subscribe((res) => {
      if (res) {
        this.sqlQueryTestResult = res;
        this.buildInputOperationList(res);
        if (!fromConsole) {
          this.actionSelected.emit({
            pos: 1
          });
        }
      }
    });
  }

  onNavigateToTransformation(event) {
    setTimeout(() => {
      this.actionSelected.emit({
        pos: 1
      });
    }, 1000);

    this.getObjectDesctiption(this.selectedTargetObjectType.label, true);
  }

  buildInputOperationList(data) {
    let inputList = [];

    if (data.length) {
      var parentObj = data[0];

      Object.keys(parentObj).forEach((parentKey) => {
        inputList.push(
          {
            name: parentKey, type: 'string'
          });

        if (typeof (parentObj[parentKey]) == "object") {
          let childObj = parentObj[parentKey];
          let childObjList = [];

          if (childObj && childObj.records && childObj.records.length) {
            Object.keys(childObj.records[0]).forEach(childKey => {
              childObjList.push({
                name: parentKey + '.' + childKey,
                type: 'string'
              });
            });
          }

          inputList = inputList.concat(childObjList);
        }
      });
    }

    this.inputOperationChanged.emit(inputList);
  }

  saveOperationState() {
    this.operationStateChanged.emit({
      type: 'Salesforce Child Object Selection',
      content: this.selectedChildObjectType.label,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Salesforce Parent Object Selection',
      content: this.selectedParentObjectType.name,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Salesforce Parent Query',
      content: this.salesforceQuery,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Salesforce Child Query',
      content: this.salesforceSubQuery,
      stepCount: this.stepCount
    });

    this.operationStateChanged.emit({
      type: 'Salesforce Execute Query',
      content: this.selectedSalesforceQuery,
      stepCount: this.stepCount
    });
  }

  buildWhereClause(conditions) {
    let sqlCondition = ' WHERE ';

    conditions.forEach((item, index) => {

      switch (item.operator) {
        case "=":
          sqlCondition = sqlCondition + (item.condition + " " + item.operator + " ");
          break;
        case ">":
          sqlCondition = sqlCondition + (item.condition + " " + item.operator + " ");
          break;
        case "<":
          sqlCondition = sqlCondition + (item.condition + " " + item.operator + " ");
          break;
        default:
          break;
      }

      sqlCondition = sqlCondition + " '" + item.value + "' ";

      if (conditions.length > 1 && index < conditions.length - 1) {
        sqlCondition = sqlCondition + ' AND ';
      }
    });

    return sqlCondition;
  }


  buildSqlQuery(objectTypeName, selectedColumns) {
    var sql = 'SELECT * FROM ' + objectTypeName;
    var columns = [];

    selectedColumns.forEach(element => {
      if (element && element.name) {
        columns.push(element.name);
      }
    });

    if (columns.length) {
      sql = sql.replace('*', columns.join(','));
    }

    return sql;
  }

  buildTestSqlQuery() {

    if (this.salesforceSubQuery) {

      let whereClauseIndex = this.salesforceQuery.indexOf("FROM");

      this.salesforceTestQuery = this.salesforceQuery.slice(0, whereClauseIndex) + ', (' +
        this.salesforceSubQuery + ') ' +
        this.salesforceQuery.slice(whereClauseIndex, this.salesforceQuery.length);

    } else {
      this.salesforceTestQuery = this.salesforceQuery;
    }

    this.selectedSalesforceQuery = this.salesforceTestQuery;

    this.saveOperationState();
  }

  buildSummary() {
    this.summaryList = [];

    this.savedOperations.forEach((ops: any) => {
      this.operationTypes.forEach((type: any) => {
        if (ops.operationTypeId == type.id) {
          this.summaryList.push({
            title: type.name,
            content: ops.content
          });
        }
      });
    });
  }
}
