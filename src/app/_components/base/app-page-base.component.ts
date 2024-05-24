import { EventEmitter, Injectable, Output } from "@angular/core";

import { MenuItem } from "primeng/api";

import { ApiCallStatusCodes } from "src/app/_enums/enums";
import { NotificationMessages } from "src/app/_enums/notification-messages";
import { NotificationService } from "src/app/services/notification.service";


@Injectable()
export abstract class AppPageBaseComponent {

    @Output() breadcrumbItems = new EventEmitter();

    constructor(public notifService: NotificationService) { }

    public handleErrorMessage(err: any, customErrorMessage: string) {
        if (err.status == 0) {
            this.notifService.showErrorToast(
                NotificationMessages.ApiError.Title,
                NotificationMessages.ApiError.Message);
        }
        else if (err.status == ApiCallStatusCodes.BADREQUEST) {
            this.notifService.showInfoToast(
                'Oops!',
                err.error == null ? customErrorMessage : err.error);
        }
        else {
            this.notifService.showErrorToast(
                NotificationMessages.GenericError.Title,
                err.Message == null ? NotificationMessages.GenericError.Message : err.error);
        }
    }

    public handleAccedataApiErrorMessage(err: any, customErrorMessage: string) {
        if (err.status == 0) {
            this.notifService.showErrorToast(
                NotificationMessages.AccedataApiError.Title,
                NotificationMessages.AccedataApiError.Message);
        }
        else if (err.status == ApiCallStatusCodes.BADREQUEST) {
            this.notifService.showInfoToast(
                'Oops!',
                err.error == null ? customErrorMessage : err.error);
        }
        else {
            this.notifService.showErrorToast(
                NotificationMessages.GenericError.Title,
                err.Message == null ? NotificationMessages.GenericError.Message : err.error);
        }
    }

    setBreadcrumbs(items: MenuItem[]) {
        this.breadcrumbItems.emit(items);
    }
}