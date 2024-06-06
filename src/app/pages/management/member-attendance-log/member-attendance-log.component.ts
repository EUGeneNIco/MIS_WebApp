import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService } from 'primeng/api';
import { Table } from 'primeng/table';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { ConfirmationMessages } from 'src/app/_enums/confirmation-messages';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { DateUtils } from 'src/app/_helpers/dateUtils';
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
      { field: 'member', filter: true, header: 'Member', sortable: false, type: 'text' },
      { field: 'logDateTime', filter: true, header: 'Logged Date/Time', sortable: true, type: 'text', isDate: true },
      { field: 'service', filter: false, header: 'Service', sortable: false, type: 'text' },
      { field: 'id', filter: false, header: 'Action', sortable: false, type: 'actions' }
    ];

    this.loading = true;
    this.initialGridParams = Object.assign({}, this.dataParameter);
  }


  ngAfterViewInit(): void {
    this.table = this.dt;
  }

  _callLoadService(): void {
    this.logService.getGrid(this.dataParameter).subscribe({
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
        // firstName: ['', Validators.required],
      })
    }
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Management' },
      { label: 'Member Attendance Log', url: '' }
    ]);
  }

  reInitGrid() {
    this.dataParameter = this.initialGridParams;
    this._callLoadService();
  }

  // ******************************************Modal(Add, Edit and View)************************************************

  openModal() {
    this.displayModal = true;
    this.formModel.enable();
  }

  onRowSelect(event: any) {
    this.displayModal = true;
    this.formModel.disable();

    const selectedRowId = event.data['id'];
    this.getDetails(selectedRowId);
  }

  onEditMode() {
    this.formModel.enable();
  }

  openEditModal(record: any) {
    const recordId = record.id;
    this.displayModal = true;
    this.onEditMode();
    this.getDetails(recordId);
  }

  getDetails(id: any) {
    // this.recordId = id;
    // this.logService.get(id).subscribe({
    //   next: (data: any) => {
    //     // console.log("get data: ", data);

    //     this.formModel.patchValue(data);
    //   },
    //   error: (e) => {
    //     this.handleErrorMessage(e, NotificationMessages.GenericError.Message);
    //   }
    // })
  }

  closeModal() {
    this.displayModal = false;
  }

  onExit() {
    this.recordId = 0;
    this.formModel.reset();
  }

  // mapData(record: any) {
  //   let birthDate = null;
  //   if (record.birthDate)
  //     birthDate = DateUtils.getFormattedDate(record.birthDate);

  //   return {
  //     firstName: record.firstName ?? '',
  //   }
  // }

  // onSubmit() {
  //   if (this.formModel.valid) {
  //     let record = this.formModel.getRawValue();
  //     let mappedData = this.mapData(record);
  //     // console.log('mapped: ', mappedData);

  //     if (this.recordId > 0) {
  //       this.confirmationService.confirm({
  //         message: ConfirmationMessages.ConfirmUpdate.Message,
  //         header: 'Confirmation',
  //         icon: 'pi pi-exclamation-triangle',
  //         accept: () => {
  //           this.uiService.block();

  //           this.logService.update({ ...mappedData, id: this.recordId }).subscribe({
  //             next: (data: any) => {
  //               // console.log('updated: ', data);

  //               this.notifService.showSuccessToast(
  //                 NotificationMessages.SaveSuccessful.Title,
  //                 NotificationMessages.SaveSuccessful.Message);

  //               this.reInitGrid();
  //               this.recordId = 0;
  //               setTimeout(() => this.closeModal(), 500);
  //               this.uiService.unBlock();
  //             },
  //             error: (e) => {
  //               this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
  //               this.uiService.unBlock()
  //             }
  //           })
  //         }
  //       })
  //     }
  //     else {
  //       this.confirmationService.confirm({
  //         message: ConfirmationMessages.ConfirmSave.Message,
  //         header: 'Confirmation',
  //         icon: 'pi pi-exclamation-triangle',
  //         accept: () => {
  //           this.uiService.block();

  //           this.logService.create(mappedData).subscribe({
  //             next: (data: any) => {
  //               // console.log('created: ', data);

  //               this.notifService.showSuccessToast(
  //                 NotificationMessages.SaveSuccessful.Title,
  //                 NotificationMessages.SaveSuccessful.Message);

  //               this.reInitGrid();
  //               setTimeout(() => this.closeModal(), 500);
  //               this.uiService.unBlock()
  //             },
  //             error: (e) => {
  //               this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
  //               this.uiService.unBlock()
  //             }
  //           })
  //         }
  //       })
  //     }
  //   }
  //   else {
  //     this.validation.validateAllFormFields(this.formModel);
  //   }
  // }
}

