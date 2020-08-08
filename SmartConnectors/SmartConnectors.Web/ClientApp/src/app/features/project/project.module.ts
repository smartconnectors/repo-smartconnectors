import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent } from './project.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '~/shared/shared.module';
import { SalesforceModule } from '~/connectors/salesforce/salesforce.module';
import { ExcelModule } from '~/connectors/excel/configuration-dialog/excel.module';
import { TransformationModule } from '~/connectors/transformation/transformation.module';
import { ProjectService } from './project.service';
import { ScheduleOperationDialogComponent } from './schedule-operation-dialog/schedule-operation-dialog.component';
import { AddNewWorkflowDialogComponent } from './add-new-workflow-dialog/add-new-workflow-dialog.component';
import { ScriptModule } from '~/connectors/script/script.module';
import { SqlModule } from '~/connectors/sql/sql.module';
import { HttpConnectorModule } from '~/connectors/http/http.module';
import { ShowOperationSummaryDialogComponent } from './show-operation-summary-dialog/show-operation-summary-dialog.component';

const routes: Routes = [
  {
    path: '',
    component: ProjectComponent
  }
];


@NgModule({
  declarations: [ProjectComponent, ScheduleOperationDialogComponent, AddNewWorkflowDialogComponent, ShowOperationSummaryDialogComponent, ShowOperationSummaryDialogComponent],
  entryComponents: [AddNewWorkflowDialogComponent, ScheduleOperationDialogComponent, ShowOperationSummaryDialogComponent],
  imports: [
    SharedModule,
    SalesforceModule,
    ExcelModule,
    TransformationModule,
    ScriptModule,
    SqlModule,
    HttpConnectorModule,
    RouterModule.forChild(routes)
  ],
  providers: [ProjectService]
})
export class ProjectModule { }
