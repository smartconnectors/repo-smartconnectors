import { Injectable } from '@angular/core';
import { HttpService } from '~/core/http';
import { environment } from 'src/environments/environment';
import { Observable, of } from 'rxjs';

@Injectable()
export class ProjectService {
    constructor(private http: HttpService) { }

    getConnectors() {
        let url = environment.baseUrl + '/connectors';

        return this.http.get(url, true);
    }

    getProjectWorkflows(projectId) {
        let url = environment.baseUrl + '/projects/' + projectId + '/workflows';

        return this.http.get(url, true);
    }

    getAllWorkflowConnector(workflowId) {
        let url = environment.baseUrl + '/workflows/' + workflowId + '/connectors';

        return this.http.get(url, true);
    }

    getRepeatOptions() {
        let url = environment.baseUrl + '/lookups/RepeatOption';

        return this.http.get(url, true);
    }


    createProjectWorkflows(body) {
        let url = environment.baseUrl + '/projects/' + body.projectId + '/workflows';

        return this.http.post(url, body, true);
    }

    updateProjectWorkflows(projectId, workflowId) {

        let url = environment.baseUrl + '/projects/' + projectId + '/workflows/' + workflowId;
        let body = {
            name: '',
            projectId: projectId
        };
        return this.http.put(url, body, true);
    }

    removeWorkflow(workflowId) {
        let url = environment.baseUrl + '/workflows/' + workflowId;
        return this.http.del(url, true);
    }

    createWorkflowConnectorAssociation(workflowId, body) {
        let url = environment.baseUrl + '/workflows/' + workflowId + '/connectors';
        return this.http.post(url, body, true);
    }

    updateWorkflowConnectorAssociation(workflowId, connectorIds) {
        let url = environment.baseUrl + '/workflows/' + workflowId + '/connectors/update-position';
        let body = {
            connectorIds: connectorIds
        };
        return this.http.put(url, body, true);
    }

    getOperations(workflowConnectorId) {
        return this.http.get(environment.baseUrl + `/operations?workflowConnectorId=${workflowConnectorId}`, true);
    }


    getOperationTypes() {
        return this.http.get(environment.baseUrl + '/operationTypes', true);
    }

    saveOperations(state) {
        if (state.id) {
            return this.updateOperation(state.id, state);
        } else {
            return this.createOperation(state);
        }
    }

    createOperation(data) {
        let url = environment.baseUrl + '/operations';
        let body = {
            content: data.content,
            operationTypeId: data.operationTypeId,
            workflowConnectorId: data.workflowConnectorId,
            stepCount: data.stepCount
        };
        return this.http.post(url, body, true);

    }

    updateOperation(id, data) {
        let url = environment.baseUrl + '/operations/' + id;
        let body = {
            id: id,
            content: data.content,
            operationTypeId: data.operationTypeId,
            workflowConnectorId: data.workflowConnectorId,
            stepCount: data.stepCount
        };
        return this.http.put(url, body, true);

    }

    runOperation(projectId, workflowId): Observable<any> {
        let url = environment.baseUrl + `/projects/${projectId}/workflows/${workflowId}/run-operation`;
        return this.http.get(url, true);
    }

    saveTransformations(data) {
        if (data.id) {
            return this.updateTransformation(data.id, data);
        } else {
            return this.createTransformation(data);
        }
    }

    createTransformation(data) {
        let url = environment.baseUrl + `/workflows/${data.workflowId}/transformations`;
        return this.http.post(url, data, true);
    }

    updateTransformation(id, data) {
        let url = environment.baseUrl + `/workflows/${data.workflowId}/transformations/` + id;
        return this.http.put(url, data, true);
    }

    getTransformations(workflowId) {
        let url = environment.baseUrl + `/workflows/${workflowId}/transformations`;
        return this.http.get(url, true);
    }

    getSavedSchedules(workflowId) {
        let url = environment.baseUrl + `/workflows/${workflowId}/schedulers`;
        return this.http.get(url, true);
    }

    createScheduleOperation(workflowId, data): Observable<any> {
        let url = environment.baseUrl + `/workflows/${workflowId}/schedulers`;
        return this.http.post(url, data, true);
    }

    updateScheduleOperation(id, workflowId, data): Observable<any> {
        let url = environment.baseUrl + `/workflows/${workflowId}/schedulers/${id}`;
        return this.http.put(url, data, true);
    }

    removeSchedule(id, workflowId): Observable<any> {
        let url = environment.baseUrl + `/workflows/${workflowId}/schedulers/${id}`;
        return this.http.del(url, true);
    }

    getOperationLogs(workflowId) {
        let url = environment.baseUrl + `/workflows/${workflowId}/operation-logs`;
        return this.http.get(url, true);
    }
}