import { environment } from '../../environments/environment';
const uiApiUrl = environment.baseUrl;

export class ServiceConstants {
    public static LoginApiUrl = environment.baseUrl + '/Account/authenticate';   
}
