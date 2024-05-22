import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-import-member-data',
  templateUrl: './import-member-data.component.html'
})
export class ImportMemberDataComponent {

  uploadedFiles: any[] = [];
  constructor(
    private messageService: MessageService
  ) {
  }

  onUpload(event: any) {
    for (const file of event.files) {
      this.uploadedFiles.push(file);
    }

    this.messageService.add({ severity: 'info', summary: 'Success', detail: 'File Uploaded' });
  }
}
