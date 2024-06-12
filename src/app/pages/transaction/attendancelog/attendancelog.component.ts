import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ConfirmationService, Message, SelectItem } from 'primeng/api';
import { AppPageBaseComponent } from 'src/app/_components/base/app-page-base.component';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { Validation } from 'src/app/_helpers/validation';
import { AttendancelogService } from 'src/app/services/attendancelog.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-attendancelog',
  templateUrl: './attendancelog.component.html',
  styleUrl: './attendancelog.component.scss'
})
export class AttendancelogComponent extends AppPageBaseComponent implements OnInit {
  formModel: FormGroup;
  readonly createFormTitle: string = 'Guest Registration Form';

  // Modal
  formMode: string;
  messages: Message[] = [];
  displayModal: boolean;
  editMode: boolean;
  isQrLog: boolean;

  readonly QR: string = 'QR';
  readonly Name: string = 'Name';

  logOptions: SelectItem[] = [
    { label: this.QR, value: this.QR },
    { label: this.Name, value: this.Name }
  ]

  nameQueryResults: any[] = [];

  get code() { return this.formModel.get('code'); }
  get name() { return this.formModel.get('name'); }
  get selectedLogOption() { return this.formModel.get('selectedLogOption'); }

  constructor(
    private attendanceLogService: AttendancelogService,
    private router: Router,
    private fb: FormBuilder,
    public confirmationService: ConfirmationService,
    public validation: Validation,
    notifService: NotificationService) {
    super(notifService);
  }

  ngOnInit(): void {
    this._initializeFormModel();
    this.displayModal = true;

    this.setBreadcrumbs([
      { label: 'Transaction' },
      { label: 'Attendance', url: '' }
    ]);

    this.isQrLog = true;
    this.selectedLogOption.setValue(this.QR);
  }

  _initializeFormModel(): void {
    if (!this.formModel) {
      this.formModel = this.fb.group({
        code: [''],
        name: [''],
        selectedLogOption: [''],
      })
    }
  }

  onExit() {
    this.router.navigateByUrl('');
  }

  onLogOptionChange(val) {
    this.isQrLog = val.checked;
    this.code.reset();
    this.name.reset();
    this.nameQueryResults = [];
  }

  onSelectMemberOrGuest(event: any) {
    if (!event.data.memberId && !event.data.guestId)
      return;

    this.attendanceLogService.log({ code: '', memberId: event.data.memberId, guestId: event.data.guestId }).subscribe({
      next: (data: any) => {
        // console.log('logged: ', data);
        this.code.setValue('');
        if (data && data.message?.trim().length > 0) {
          this.messages.push({ key: 'tst', severity: 'success', detail: data.message, life: 5000 })
        }

        this.nameQueryResults = [];
      },
      error: (e) => {
        this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
        this.code.setValue('');
      }
    })
  }

  onSubmit() {
    this.messages = [];

    if (this.isQrLog) {
      if (this.code && this.code.value.trim().length > 0) {
        this.attendanceLogService.log({ code: this.code.value, memberId: null, guestId: null }).subscribe({
          next: (data: any) => {
            // console.log('logged: ', data);
            this.code.setValue('');
            if (data && data.message?.trim().length > 0) {
              this.messages.push({ key: 'tst', severity: 'success', detail: data.message, life: 5000 })
            }
          },
          error: (e) => {
            this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
            this.code.setValue('');
          }
        })
      }
    }
    else {
      if (this.name && this.name.value.trim().length > 0) {
        this.attendanceLogService.getQuery({ name: this.name.value }).subscribe({
          next: (data: any) => {
            // console.log('query results: ', data);
            this.name.setValue('');

            this.nameQueryResults = data.results;
          },
          error: (e) => {
            this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
            this.code.setValue('');
          }
        })
      }
    }
  }
}
