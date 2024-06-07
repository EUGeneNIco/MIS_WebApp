import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { Validation } from 'src/app/_helpers/validation';
import { UiService } from 'src/app/layout/service/ui.service';
import { AttendancelogService } from 'src/app/services/attendancelog.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-member-attendance-log',
  templateUrl: './member-attendance-log.component.html',
  styleUrl: './member-attendance-log.component.scss'
})
export class MemberAttendanceLogComponent extends MasterBaseComponent implements AfterViewInit, OnInit {
  formModel: FormGroup;
  recordId: any = 0;
  initialGridParams: any;

  // Modal
  displayModal: boolean;

  // get firstName() { return this.formModel.get('firstName'); }

  @ViewChild('dt') dt: Table;
  constructor(
    private logService: AttendancelogService,
    private fb: FormBuilder,
    private uiService: UiService,
    public confirmationService: ConfirmationService,
    public validation: Validation,
    notifService: NotificationService) {
    super(notifService);
  }

  ngOnInit(): void {
    this._initializeFormModel();
    this._setBreadcrumbs();

    this.cols = [
      { field: 'member', filter: true, header: 'Name', sortable: false, type: 'text' },
      { field: 'logDateTime', filter: true, header: 'Logged Date/Time', sortable: true, type: 'text', isDate: true },
      { field: 'service', filter: false, header: 'Service', sortable: false, type: 'text' },
      // { field: 'id', filter: false, header: 'Action', sortable: false, type: 'actions' }
    ];

    this.loading = true;
    this.initialGridParams = Object.assign({}, this.dataParameter);
  }


  ngAfterViewInit(): void {
    this.table = this.dt;
  }

  _callLoadService(): void {
    this.logService.getMemberAttendanceLogs(this.dataParameter).subscribe({
      next: (data: any) => {
        this.reloadData(data);
      },
      error: (e) => {
        this.notifService.showErrorToast('Oops!', 'Server error. Please try again later.');
      }
    })
  }

  _initializeFormModel(): void {
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Attendance Logs' },
      { label: 'Member Attendance Log', url: '' }
    ]);
  }

  reInitGrid() {
    this.dataParameter = this.initialGridParams;
    this._callLoadService();
  }
}

