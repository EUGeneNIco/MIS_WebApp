import { Injectable } from '@angular/core';

import { MessageService } from 'primeng/api';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    constructor(private msgService: MessageService) { }

    showErrorToast(header: string, message: string) {
        this.msgService.add({ key: 'tst', severity: 'error', summary: header, detail: message, life: 6000 });
    }

    showInfoToast(header: string, message: string) {
        this.msgService.add({ key: 'tst', severity: 'info', summary: header, detail: message, life: 6000 });
    }

    showSuccessToast(header: string, message: string) {
        this.msgService.add({ key: 'tst', severity: 'success', summary: header, detail: message, life: 6000 });
    }

    showWarnToast(header: string, message: string) {
        this.msgService.add({ key: 'tst', severity: 'warn', summary: header, detail: message, life: 6000 });
    }
}