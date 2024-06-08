import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SelectItem, ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { Validation } from 'src/app/_helpers/validation';
import { UiService } from 'src/app/layout/service/ui.service';
import { AttendancelogService } from 'src/app/services/attendancelog.service';
import { NotificationService } from 'src/app/services/notification.service';
import { ServiceService } from 'src/app/services/service.service';

@Component({
  selector: 'app-guest-attendance-unidentified-log',
  templateUrl: './guest-attendance-unidentified-log.component.html',
  styleUrl: './guest-attendance-unidentified-log.component.scss'
})
export class GuestAttendanceUnidentifiedLogComponent extends MasterBaseComponent implements AfterViewInit, OnInit {
  formModel: FormGroup;
  recordId: any = 0;
  processMessage: string;
  initialGridParams: any;

  // Modal
  displayModal: boolean;
  services: SelectItem[] = [];

  get service() { return this.formModel.get('service'); }

  @ViewChild('dt') dt: Table;
  constructor(
    private logService: AttendancelogService,
    private serviceService: ServiceService,
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
      { field: 'name', filter: true, header: 'Name', sortable: false, type: 'text' },
      { field: 'logDateTime', filter: true, header: 'Logged Date/Time', sortable: true, type: 'text', isDate: true },
      { field: 'id', filter: false, header: 'Action', sortable: false, type: 'actions' }
    ];

    this.loading = true;
    this.initialGridParams = Object.assign({}, this.dataParameter);
    this.getServicesList();
  }


  ngAfterViewInit(): void {
    this.table = this.dt;
  }

  _callLoadService(): void {
    this.logService.getGuestUnidentifiedLogs(this.dataParameter).subscribe({
      next: (data: any) => {
        this.reloadData(data);
      },
      error: (e) => {
        this.notifService.showErrorToast('Oops!', 'Server error. Please try again later.');
      }
    })
  }

  _initializeFormModel(): void {
    if (!this.formModel) {
      this.formModel = this.fb.group({
        service: ['', Validators.required],
      })
    }
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Attendance Logs' },
      { label: 'Guest Attendance Unidentified Logs', url: '' }
    ]);
  }

  reInitGrid() {
    this.dataParameter = this.initialGridParams;
    this._callLoadService();
  }

  openProcessingModal(record: any) {
    this.processMessage = `Processing log of ${record.name} last ${record.logDateTime}`
    this.recordId = record.id;
    this.displayModal = true;
  }

  getServicesList() {
    this.serviceService.getServicesList().subscribe({
      next: (data: any) => {
        this.services = data.map(d => {
          return {
            label: d.name,
            value: d.id
          }
        })
      },
      error: (e) => {
        this.handleErrorMessage(e, NotificationMessages.GenericError.Message);
      }
    })
  }

  resetData() {
    this.recordId = 0;
    this.formModel.reset();
    this.processMessage = null;
  }

  onSubmit() {
    if (this.formModel.valid) {
      if (this.recordId > 0) {
        this.uiService.block();
        this.logService.processGuestUnidentifiedLog({
          serviceId: this.service.value.value,
          unidentifiedLogId: this.recordId
        }).subscribe({
          next: (data: any) => {
            // console.log('updated: ', data);
            this.notifService.showSuccessToast(
              NotificationMessages.SaveSuccessful.Title,
              NotificationMessages.SaveSuccessful.Message);

            this.reInitGrid();
            this.displayModal = false;
            setTimeout(() => this.resetData(), 300);
            this.uiService.unBlock();
          },
          error: (e) => {
            this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
            this.uiService.unBlock()
          }
        })
      }
    }
    else {
      this.validation.validateAllFormFields(this.formModel);
    }
  }
}
