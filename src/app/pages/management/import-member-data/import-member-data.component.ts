import { AfterViewInit, Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AppPageBaseComponent } from 'src/app/_components/base/app-page-base.component';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { NotificationService } from 'src/app/services/notification.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-import-member-data',
  templateUrl: './import-member-data.component.html'
})
export class ImportMemberDataComponent extends AppPageBaseComponent implements OnInit {
  excelData: any;

  uploadedData: any[] = [];
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
      this.uploadedData.push(file);
    }

    this.messageService.add({ severity: 'info', summary: 'Success', detail: 'File Uploaded' });
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Management' },
      { label: 'Import Data', url: '' }
    ]);
  }

  readExcel(event) {
    let file = event.target.files[0];

    let fileReader = new FileReader();
    fileReader.readAsBinaryString(file);
    fileReader.onload = (e) => {
      var wb = XLSX.read(fileReader.result, { type: 'binary' });
      var sheetNames = wb.SheetNames;
      this.excelData = XLSX.utils.sheet_to_json(wb.Sheets[sheetNames[0]]);
      console.log(this.excelData);
    }

  }
}
