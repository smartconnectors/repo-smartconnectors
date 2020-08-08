import { Component, OnInit } from '@angular/core';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { faFileExcel } from '@fortawesome/free-solid-svg-icons';
import { MatDialogRef, MatDialog, MatTreeFlattener, MatTreeFlatDataSource, MatSnackBar } from '@angular/material';
import { OperationDialogComponent } from '~/connectors/salesforce/operation-dialog/operation-dialog.component';
import { LocalStorageService } from '~/core/services';
import { ConfigurationDialogComponent } from '~/connectors/excel/configuration-dialog';
import { HttpService } from '~/core/http';
import { environment } from 'src/environments/environment';
import { ActivatedRoute } from '@angular/router';
import { forkJoin } from 'rxjs';
import { SalesforceService } from '~/connectors/salesforce/salesforce.service';
import { ProjectService } from './project.service';
import { debug } from 'util';
import { FlatTreeControl } from '@angular/cdk/tree';
import { ScheduleOperationDialogComponent } from './schedule-operation-dialog/schedule-operation-dialog.component';
import { AddNewWorkflowDialogComponent } from './add-new-workflow-dialog/add-new-workflow-dialog.component';
import { SalesforceLoginModalComponent } from '~/connectors/salesforce';
import { ShowOperationSummaryDialogComponent } from './show-operation-summary-dialog';

export interface Connector {
  id: number;
  name: string;
  icon: string;
  position: number;
  type: string;
  isSelected: boolean;
  isPrimary: boolean;
  workflowConnectorId: number;
}

/**
 * Each node has a name and an optional list of children.
 */
interface TreeNode {
  id: number,
  name: string;
  isActive: boolean,
  children?: TreeNode[];
}

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  isActive: boolean,
  level: number;
}

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})

export class ProjectComponent implements OnInit {

  faCoffee = faFileExcel;
  connectorName: string;

  allConnectors: Connector[] = []
  displayConnectorLists: Connector[] = []
  currentStepCount: number = 1;
  workflowConnectors: Connector[] = [];
  projectId: number;
  savedWorkFlows = [];
  savedOperations = [];
  salectedWorkflowId: any;
  selectedConnector: any;
  selectedOperationData: any = {};
  inputOperationList: [] = [];
  outputOperationList: [] = [];
  selectedSaleforceObjectName: any;
  salesforceToken: string;
  selectedWorkflowId: any;
  workflowConnectorList = [];
  newWorkflowTitle: string;
  selectedTabIndex: number;
  packageList: string[];
  packageName: string;
  activeWorkflows: any[] = [];
  selectedOperationState: any;
  operationTypes: any[] = [];
  selectedWorkflowConnectorId: number;
  transformationData: any;
  savedTransformations: any;
  workflowsList: any;

  private _transformer = (node: TreeNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      id: node.id,
      level: level,
      isActive: node.isActive
    };
  }

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  constructor(private dialog: MatDialog,
    private route: ActivatedRoute,
    private projectService: ProjectService,
    private salesforceService: SalesforceService,
    private localStorageService: LocalStorageService,
    private _snackBar: MatSnackBar) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.projectId = params['id'];
      this.getData();
    });
  }

  getData() {
    this.getConnectors();
    this.getProjectWorkflows();
    this.getOperationTypes();
  }

  getOperationTypes() {
    this.projectService.getOperationTypes().subscribe(res => {
      this.operationTypes = res;
    });
  }

  getConnectors() {
    this.projectService.getConnectors().subscribe((res) => {
      this.allConnectors = res;
      this.displayConnectorLists = Object.assign([], res.filter(item => item.isPrimary));
    });
  }

  getProjectWorkflows() {
    this.projectService.getProjectWorkflows(this.projectId).subscribe((res) => {
      this.savedWorkFlows = res;
      if (this.savedWorkFlows.length == 0) {
        this.createProjectWorkflows(null);
      }

      this.savedWorkFlows.forEach((item, index) => {
        if (item.isActive) {
          this.selectedWorkflowId = item.id;
          this.selectedTabIndex = index;
        }
      });

      this.generateWorkflowTree();
      this.setDefaultActiveWorkflow();
    });
  }

  getOperations() {
    return this.projectService.getOperations(this.selectedWorkflowConnectorId).subscribe((res) => {
      this.savedOperations = res;
    });
  }

  getTransformations() {
    return this.projectService.getTransformations(this.selectedWorkflowId).subscribe((res) => {
      if (res && res.length) {
        this.savedTransformations = res[0];
      }
    });
  }

  getAllWorkflowConnector(workflowId) {
    if (workflowId) {
      this.projectService.getAllWorkflowConnector(workflowId).subscribe((res) => {
        this.workflowConnectorList = res;
        //this.getProjectWorkflows();
        this.buildCurrentWorkflow();
      });
    }
  }

  setDefaultActiveWorkflow() {
    if (this.savedWorkFlows.length > 5) {
      this.activeWorkflows = this.savedWorkFlows.filter(item => item.isActive);
    } else {
      this.activeWorkflows = this.savedWorkFlows;
    }
  }

  saveOperationState(operationType, value, stepCount) {
    let operationTypeId = 0;
    let saveOperationId = 0;
    this.operationTypes.forEach((type: any) => {
      if (type.name == operationType) {
        operationTypeId = type.id;
      }
    });
    this.savedOperations.forEach((ele) => {
      if (ele.operationTypeId == operationTypeId) {
        saveOperationId = ele.id;
      }
    });

    this.selectedOperationState = {
      id: saveOperationId,
      operationTypeId: operationTypeId,
      workflowConnectorId: this.selectedWorkflowConnectorId,
      content: value,
      stepCount: stepCount
    };

    this.projectService.saveOperations(this.selectedOperationState).subscribe((res) => {
      // this.getOperations();
    });
  }

  openWorkflow(node) {

    this.activeWorkflows.forEach(item => item.isActive = false);

    let index = this.activeWorkflows.findIndex(item => item.id == node.id);

    if (index == -1) {
      node.isActive = true;
      this.selectedTabIndex = this.activeWorkflows.length;
      this.activeWorkflows.push(node);
    } else {
      this.activeWorkflows[index].isActive = true;
      this.selectedTabIndex = index;
    }

    this.selectedWorkflowId = node.id;

    this.getAllWorkflowConnector(node.id);
  }

  generateWorkflowTree() {
    let nodes = [], groupedResult;

    this.packageList = [];

    groupedResult = this.savedWorkFlows.reduce(function (r, a) {
      let key = a.packageName;
      r[key] = r[key] || [];
      r[key].push(a);
      return r;
    }, Object.create(null));

    Object.keys(groupedResult).forEach(item => {
      this.packageList.push(item);
      nodes.push({
        name: item,
        children: groupedResult[item]
      });
    });

    this.packageList = this.packageList.sort(function (a, b) {
      var x = a.toLowerCase(); // ignore upper and lowercase
      var y = b.toLowerCase(); // ignore upper and lowercase
      if (x < y) {
        return -1;
      }
      if (x > y) {
        return 1;
      }
      // names must be equal
      return 0;
    });

    this.dataSource.data = nodes;
  }

  buildCurrentWorkflow() {
    this.workflowConnectors = [];
    this.workflowConnectorList.forEach((item, index) => {
      this.allConnectors.forEach((obj) => {
        if (obj.id == item.connectorId) {
          obj.position = item.pos;
          obj.workflowConnectorId = item.id;
          obj.isSelected = false;
          this.workflowConnectors.push(obj);

          return;
        }
      });
    });
  }

  checkSaleforceAuth() {
    let token = localStorage.getItem('sf_token');

    if (!token) {
      const dialogRef = this.dialog.open(SalesforceLoginModalComponent, {
        width: '300',
        height: '75%',
        data: {
          workflowId: this.selectedWorkflowId,
          connectorId: this.selectedConnector.id
        }
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          localStorage.setItem('sf_token', result.token);
          this.salesforceToken = result.token;
        }
      });
    } else {
      this.salesforceService.refreshToken(this.selectedWorkflowId, 1).subscribe(result => {
        if (result) {
          localStorage.setItem('sf_token', result);
          this.salesforceToken = result;
        }
      });
    }
  }

  getSaleforceObject(token) {
    this.salesforceService.getSalesforceObjects(token).subscribe((res) => {
      if (res && res.sobjects) {
        this.outputOperationList = res.sobjects;
      }
    });
  }

  saveCurrentWorkflowState(item, prev, curr) {
    var alreadyExist = this.workflowConnectorList.filter((obj) => item.id == obj.connectorId).length;

    if (!alreadyExist) {
      this.createWorkflowConnectorAssociation(this.selectedWorkflowId, item, this.workflowConnectorList.length);
    } else {
      let connectorIds = this.workflowConnectors.map((item) => item.id);

      this.updateWorkflowConnectorAssociation(this.selectedWorkflowId, connectorIds);
    }
  }

  addScriptingConnector() {
    let scriptConnector = this.allConnectors.filter(item => item.name == 'Script')[0];
    let isScriptConnectorAdded = this.workflowConnectorList.filter((item) => item.connectorId == scriptConnector.id).length > 0;

    if (this.workflowConnectorList.length && !isScriptConnectorAdded) {
      this.createWorkflowConnectorAssociation(this.selectedWorkflowId, isScriptConnectorAdded, 1);
    } else {
      this.buildCurrentWorkflow();
    }
  }

  createProjectWorkflows(data) {
    let body = {
      name: data ? data.newWorkflowTitle : 'Default workflow',
      packageName: data ? data.packageName : 'Default Package',
      projectId: this.projectId
    };
    this.projectService.createProjectWorkflows(body).subscribe((res) => {
      this.openSnackBar("Project Workflow", "Created");
      this.workflowConnectorList = [];
      this.workflowConnectors = [];
      this.selectedOperationData = {};
      this.getProjectWorkflows();
    });
  }

  removeWorkflow(workflow) {
    this.projectService.removeWorkflow(workflow.id).subscribe((res) => {
      this.openSnackBar("Project Workflow", "Removed");
      this.getProjectWorkflows();
    });
  }

  updateProjectWorkflows(item, index) {
    this.projectService.updateProjectWorkflows(this.projectId, item.workflowId).subscribe((res) => {
      this.openSnackBar("Project Workflow", "Created");
      this.getProjectWorkflows();
    });
  }

  createWorkflowConnectorAssociation(workflowId, item, index) {
    let transformationConnector = this.allConnectors.filter(item => item.name == 'Transformation')[0];
    let isTransformtionAdded = this.workflowConnectorList.filter((item) => item.connectorId == transformationConnector.id).length > 0;

    let body = {
      connectorId: item.id,
      workflowId: workflowId,
      pos: index,
      isPreScriptingEnabled: true,
      isPostScriptingEnabled: true,
      isTransformationEnabled: !isTransformtionAdded
    };

    if (item.name.toLowerCase() !== "mysql") {
      body.isPreScriptingEnabled = false;
      body.isPostScriptingEnabled = false;
    }

    this.projectService.createWorkflowConnectorAssociation(workflowId, body).subscribe((res) => {
      this.getAllWorkflowConnector(workflowId);
    });
  }

  updateWorkflowConnectorAssociation(workflowId, connectorIds) {
    this.projectService.updateWorkflowConnectorAssociation(workflowId, connectorIds).subscribe((res) => {
      this.getAllWorkflowConnector(workflowId);
    });
  }

  removeTab(event, tab) {
    let index = this.activeWorkflows.findIndex(item => item.id == tab.id);
    if (index > -1) {
      this.activeWorkflows.splice(index, 1);
    }
    // this.projectService.removeWorkflow(tab.id).subscribe((res) => {
    //   if (res) {
    //     this.getProjectWorkflows();
    //   }
    // });
  }

  onTabChange(event) {
    if (this.activeWorkflows.length) {
      this.selectedOperationData = {};
      this.activeWorkflows[event.index].isActive = true;
      this.getAllWorkflowConnector(this.activeWorkflows[event.index].id);
    }
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // from first list to second list
      if (event.previousContainer.id === 'cdk-drop-list-0') {
        if (this.validateCurrentWorkflowState(event.container.data, event.item.data)) {
          transferArrayItem(event.previousContainer.data,
            event.container.data,
            event.previousIndex,
            event.currentIndex);

          this.displayConnectorLists = Object.assign([], this.allConnectors.filter(item => item.isPrimary));

          this.saveCurrentWorkflowState(event.item.data, event.previousIndex, event.currentIndex);
        }
      }
    }
  }

  validateCurrentWorkflowState(workflowConnectors, currentItem): boolean {
    if (workflowConnectors.length) {
      return workflowConnectors.filter(item => item.id == currentItem.id).length == 0;
    } else {
      return true;
    }
  }

  onActionSelected(event) {
    if (event) {
      this.workflowConnectors.map((item) => item.isSelected = false);

      this.workflowConnectors.forEach(item => {
        if (item.position == event.pos) {
          item.isSelected = true;
          this.chooseConnectors(item);
        }
      })
    }
  }

  onInputOperationChanged(event) {
    this.inputOperationList = event;
  }

  onOutputOperationChanged(event) {
    this.outputOperationList = event;
  }

  onOperationStateChanged(event) {
    if (event) {
      this.saveOperationState(event.type, event.content, this.currentStepCount);
      this.currentStepCount = event.stepCount + 1;
    }
  }

  onExcelOperationChanged(event) {
    if (event) {
      this.selectedSaleforceObjectName = event.tableName;
      this.inputOperationList = event.columns.map(item => {
        return {
          name: item,
          type: 'string'
        }
      });

      this.transformationData = {
        tableName: event.tableName
      }

      this.getObjectDesctiption(this.salesforceToken, event.tableName);
    }

  }

  onDragDropped(event: CdkDragDrop<any>) {

  }

  onTransformationChanged(event) {
    if (event) {
      let transformationObj = {
        id: this.savedTransformations ? this.savedTransformations.id : 0,
        workflowId: this.selectedWorkflowId,
        payload: JSON.stringify(event.payload),
        script: JSON.stringify(event.script),
        input: JSON.stringify(event.output),
        output: JSON.stringify(event.output)
      };
      transformationObj.workflowId = this.selectedWorkflowId

      this.projectService.saveTransformations(transformationObj).subscribe((res) => {
        this.getTransformations();
      });
    }
  }

  onScriptChanged(event) {

  }

  onHttpChanged(event) {

  }

  onSqlChanged(event) {

  }

  openAddNewWorkflowDialog() {
    let dialogRef = this.dialog.open(AddNewWorkflowDialogComponent, {
      height: '300px',
      width: '500px',
      data: {
        packageList: this.packageList,
        savedWorkFlows: this.savedWorkFlows
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.createProjectWorkflows(result)
      }
    });
  }

  getObjectDesctiption(token, objectTypeName) {
    this.salesforceService.getSalesforceObjectDescription(token, objectTypeName).subscribe((res) => {
      if (res) {
        this.outputOperationList = res.fields.map(i => {
          return {
            name: i.name,
            type: i.type
          }
        });
      }
    });
  }

  runOperation() {
    this.projectService.runOperation(this.projectId, this.selectedWorkflowId).subscribe((res) => {
      var blob = new Blob([res.data], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
      var link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = "myFileName.xlsx";
      link.click();

      if (res) {
        this.showOpertionSummaryDialog(res);
      }
    });
  }

  viewLogs() {
    this.showOpertionSummaryDialog(null);
  }

  showOpertionSummaryDialog(res) {
    let dialogRef = this.dialog.open(ShowOperationSummaryDialogComponent, {
      height: '95%',
      width: '75%',
      data: {
        workflowId: this.selectedWorkflowId
      }
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }

  scheduleOperation() {
    let dialogRef = this.dialog.open(ScheduleOperationDialogComponent, {
      height: '95%',
      width: '75%',
      data: {
        workflowId: this.selectedWorkflowId
      }
    });

    dialogRef.afterClosed().subscribe(result => {

    });
  }


  chooseConnectors(data) {
    this.workflowConnectors.map((item) => item.isSelected = false);
    data.isSelected = true;

    this.selectedWorkflowConnectorId = data.workflowConnectorId;
    this.selectedConnector = data;

    this.selectedOperationData = {
      token: localStorage.getItem('sf_token'),
      workflowId: this.selectedWorkflowId,
      connectorType: this.selectedConnector.name.toLowerCase(),
      connectorPos: this.selectedConnector.position == 0 ? 'source' : 'target'
    };

    this.checkSaleforceAuth();
    this.getOperations();
    this.getTransformations();
  }

  openLoginModel() {

    const dialogRef = this.dialog.open(SalesforceLoginModalComponent, {
      width: '33%',
      height: '75%',
      data: {
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.salesforceToken = result;
        this.localStorageService.setItem<string>('sf_token', result);
      }
    });
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
  }
}

