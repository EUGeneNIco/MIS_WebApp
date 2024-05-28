import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AppPageBaseComponent } from 'src/app/_components/base/app-page-base.component';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-import-member-data',
  templateUrl: './import-member-data.component.html'
})
export class ImportMemberDataComponent extends AppPageBaseComponent implements OnInit {


  uploadedFiles: any[] = [];
  constructor(
    private messageService: MessageService,
    notifService: NotificationService
  ) {
    super(notifService);
  }
  ngOnInit(): void {
    this._setBreadcrumbs();
  }

  onUpload(event: any) {
    for (const file of event.files) {
      this.uploadedFiles.push(file);
    }

    this.messageService.add({ severity: 'info', summary: 'Success', detail: 'File Uploaded' });
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Management' },
      { label: 'Import Data', url: '' }
    ]);
  }
}
