import { LocalStorageService } from '~/core/services/localStorage.service';
import { LoaderService } from './loader.service';
import { UserService } from '~/core/services/user.service';
import { AlertService } from './alert.service';

export const SERVICES = [LocalStorageService, LoaderService, UserService, AlertService]

export {
    LocalStorageService,
    LoaderService,
    UserService,
    AlertService
}
