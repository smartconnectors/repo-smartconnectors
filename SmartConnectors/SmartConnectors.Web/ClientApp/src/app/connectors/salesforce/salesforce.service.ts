import { Injectable, EventEmitter } from '@angular/core';
import { HttpService } from '~/core/http';
import { environment } from 'src/environments/environment';
import { tap, map } from 'rxjs/operators';

@Injectable()
export class SalesforceService {
  objectList = [];


  constructor(private http: HttpService) {

  }

  login(loginModel: string, workflowId, connectorId) {
    return this.http.post<any>(environment.baseUrl + `/workflows/${workflowId}/${connectorId}/salesforce-authenticate`, loginModel)
      .pipe(map(data => {
        // login successful if there's a jwt token in the response
        if (data && data.value) {
          return data.value;
        }
      }));
  }

  refreshToken(workflowId, connectorId) {
    return this.http.get<any>(environment.baseUrl + `/workflows/${workflowId}/${connectorId}/salesforce-refresh-token`, true)
      .pipe(map(data => {
        // login successful if there's a jwt token in the response
        if (data && data.value) {
          return data.value.token;
        }
      }));
  }

  getObjectList() {
    return this.objectList;
  }

  getSalesforceObjects(token) {
    let obj = {
      token: token,
      instanceUrl: 'https://practice13131-dev-ed.my.salesforce.com/',
      apiVersion: 'v37.0',
      SOAPAction: ''
    };

    return this.http.post(environment.baseUrl + '/salesforce/objects', obj).pipe(tap((res) => {
      this.objectList = res;
    }));
  }

  //salesforce/objects/describe
  getSalesforceObjectDescription(token, objectTypeName) {
    let obj = {
      token: token,
      instanceUrl: 'https://practice13131-dev-ed.my.salesforce.com/',
      apiVersion: 'v37.0',
      SOAPAction: '',
      ObjectTypeName: objectTypeName
    };

    return this.http.post(environment.baseUrl + '/salesforce/objects/describe', obj);
  }

  executeSalesforceQuery(token, sql) {
    let obj = {
      token: token,
      instanceUrl: 'https://practice13131-dev-ed.my.salesforce.com/',
      apiVersion: 'v37.0',
      SOAPAction: '',
      queryString: sql,
      queryAll: false
    };

    return this.http.post(environment.baseUrl + '/salesforce/objects/query', obj);
  }
  ////salesforce/objects/update

  updateSalesforceSObject(token, objectTypeName, objectId, sfObject) {
    let obj = {
      token: token,
      instanceUrl: 'https://practice13131-dev-ed.my.salesforce.com/',
      apiVersion: 'v37.0',
      SOAPAction: '',
      ObjectTypeName: objectTypeName,
      ObjectId: objectId,
      SfObject: sfObject,
      queryAll: false
    };
    return this.http.post(environment.baseUrl + 'salesforce/objects/update', obj);
  }
}