import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { Validation } from 'src/app/_helpers/validation';
import { AttendancelogService } from 'src/app/services/attendancelog.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-guest-attendance-log',
  templateUrl: './guest-attendance-log.component.html',
  styleUrl: './guest-attendance-log.component.scss'
})
export class GuestAttendanceLogComponent extends MasterBaseComponent implements AfterViewInit, OnInit {
  formModel: FormGroup;
  recordId: any = 0;
  initialGridParams: any;

  displayModal: boolean;

  @ViewChild('dt') dt: Table;
  constructor(
    private logService: AttendancelogService,
    public confirmationService: ConfirmationService,
    public validation: Validation,
    notifService: NotificationService) {
    super(notifService);
  }

  ngOnInit(): void {
    this._initializeFormModel();
    this._setBreadcrumbs();

    this.cols = [
      { field: 'name', filter: true, header: 'Name', sortable: false, type: 'text' },
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
    this.logService.getGuestAttendanceLogs(this.dataParameter).subscribe({
      next: (data: any) => {
        this.reloadData(data);
      },
      error: () => {
        this.notifService.showErrorToast('Oops!', 'Server error. Please try again later.');
      }
    })
  }

  _initializeFormModel(): void {
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Attendance Logs' },
      { label: 'Guest Attendance Logs', url: '' }
    ]);
  }

  reInitGrid() {
    this.dataParameter = this.initialGridParams;
    this._callLoadService();
  }
}


