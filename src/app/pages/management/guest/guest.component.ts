import { formatDate } from '@angular/common';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, SelectItem } from 'primeng/api';
import { Table } from 'primeng/table';
import { first } from 'rxjs';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { ConfirmationMessages } from 'src/app/_enums/confirmation-messages';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { Validation } from 'src/app/_helpers/validation';
import { UiService } from 'src/app/layout/service/ui.service';
import { GuestService } from 'src/app/services/guest.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
  selector: 'app-guest',
  templateUrl: './guest.component.html',
  styleUrl: './guest.component.scss'
})
export class GuestComponent extends MasterBaseComponent implements AfterViewInit, OnInit {
  formModel: FormGroup;
  recordId: any = 0;
  civilStatuses: SelectItem[] = [
    { label: 'Single', value: 'Single' },
    { label: 'Married', value: 'Married' },
    { label: 'Widowed', value: 'Widowed' },
  ];
  networks: SelectItem[] = [
    { label: 'Youth', value: 1 },
    { label: 'Men', value: 2 },
    { label: 'Women', value: 3 },
    { label: 'Children', value: 4 },
    { label: 'YAN', value: 5 },
  ];
  genders: SelectItem[] = [
    { label: 'Male', value: 'Male' },
    { label: 'Female', value: 'Female' },
  ];
  readonly createFormTitle: string = 'Guest Registration Form';
  readonly updateFormTitle: string = 'Guest Registration Form';

  // Modal
  displayModal: boolean;
  formMode: any;
  editMode: boolean;

  get firstName() { return this.formModel.get('firstName'); }
  get middleName() { return this.formModel.get('middleName'); }
  get lastName() { return this.formModel.get('lastName'); }
  get address() { return this.formModel.get('address'); }
  get gender() { return this.formModel.get('gender'); }
  get contactNumber() { return this.formModel.get('contactNumber'); }
  get civilStatus() { return this.formModel.get('civilStatus'); }
  get birthDate() { return this.formModel.get('birthDate'); }
  get age() { return this.formModel.get('age'); }
  get network() { return this.formModel.get('network'); }

  @ViewChild('dt') dt: Table;
  constructor(
    private guestService: GuestService,
    private fb: FormBuilder,
    private uiService: UiService,
    public confirmationService: ConfirmationService,
    public validation: Validation,
    public override notifService: NotificationService) {
    super(notifService);
  }

  ngOnInit(): void {
    this._initializeFormModel();
    this._setBreadcrumbs();

    this.cols = [
      { field: 'name', filter: false, header: 'Name', sortable: false, type: 'text' },
      { field: 'address', filter: false, header: 'Address', sortable: false, type: 'text' },
      { field: 'contactNumber', filter: false, header: 'Contact No.', sortable: false, type: 'text' },
      { field: 'network', filter: false, header: 'Network', sortable: false, type: 'text' },
      { field: 'id', filter: false, header: 'Action', sortable: false, type: 'actions' }
    ];

    this.loading = true;
  }


  ngAfterViewInit(): void {
    this.table = this.dt;
  }

  _callLoadService(): void {
    this.guestService.getGrid(this.dataParameter).subscribe({
      next: (data: any) => {
        console.log("Params: ", data);
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
        firstName: ['', Validators.required],
        middleName: ['', Validators.required],
        lastName: ['', Validators.required],
        address: ['', Validators.required],
        age: ['', Validators.required],
        birthDate: ['', Validators.required],
        civilStatus: ['', Validators.required],
        contactNumber: ['', Validators.required],
        network: ['', Validators.required],
        gender: ['', Validators.required],
      })
    }
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Management' },
      { label: 'Guest', url: '' }
    ]);
  }

  getCivilStatuses() {
    // this.causeOfDeathDisabilityService.getGetDescriptionsOfClaim().subscribe({
    //   next: (data: any) => {
    //     this.descriptionOfClaimOptions = data.map((des) => {
    //       return { label: des.descriptionOfClaim, value: des.id };
    //     });

    //     if (this.descriptionOfClaimRetrieved !== "") {
    //       this.descriptionOfClaim.setValue(this.descriptionOfClaimOptions.find(des => {
    //         return des.label === this.descriptionOfClaimRetrieved;
    //       }));
    //     }
    //   },
    //   error: (e) => {
    //     this.handleErrorMessage(e, NotificationMessages.GenericError.Message);
    //   }
    // })
  }

  // ******************************************Modal(Add, Edit and View)************************************************

  onViewAddNewModal() {
    this.displayModal = true;
    this.formMode = 'Add';
    this.editMode = true;
    this.formModel.enable();
  }

  onRowSelect(event: any) {
    const selectedRowId = event.data['id'];
    this.editMode = false;
    this.displayModal = true;
    this.formMode = 'View';
    this.formModel.disable();;
    this.getDetails(selectedRowId);
  }

  onEditMode() {
    this.formModel.enable();
    this.formMode = 'Edit';
    this.editMode = true;
  }

  onViewEditModal(record: any) {
    const recordId = record.id;
    this.displayModal = true;
    this.onEditMode();
    this.getDetails(recordId);
  }

  getDetails(id: any) {
    this.recordId = id;
    this.guestService.get(id).subscribe({
      next: (data: any) => {
        console.log("get data: ", data);

        this.firstName.setValue(data.firstName);
        this.middleName.setValue(data.middleName);
        this.lastName.setValue(data.firstName);
        this.address.setValue(data.address);
        this.civilStatus.setValue(data.civilStatus);
        this.gender.setValue(data.gender);
        this.contactNumber.setValue(data.contactNumber);
        this.age.setValue(data.age);
        this.network.setValue(data.network);

        const formattedDate = new Date(data.birthDate)
        this.birthDate.setValue(formattedDate);
      },
      error: (e) => {
        this.handleErrorMessage(e, NotificationMessages.GenericError.Message);
      }
    })
  }

  deleteRecord(record: any) {
    const recordId = record.id;
    console.log('id to delete: ', recordId);
    if (recordId > 0) {
      this.guestService.delete(recordId).subscribe({
        next: (data: any) => {
          console.log('deleted: ', data);

          this.notifService.showSuccessToast(
            NotificationMessages.DeleteSuccessful.Title,
            NotificationMessages.DeleteSuccessful.Message);

          this._callLoadService();
          setTimeout(() => this.displayModal = false, 500);
          this.uiService.unBlock()
        },
        error: (e) => {
          this.handleErrorMessage(e, NotificationMessages.DeleteError.Message);
          this.uiService.unBlock()
        }
      })
    }
    // this.confirmationService.confirm({
    //   message: ConfirmationMessages.ConfirmDelete.Message,
    //   header: 'Confirmation',
    //   icon: 'pi pi-exclamation-triangle',
    //   accept: () => {
    //     this.uiService.block();


    //   }
    // })

  }

  onExit() {
    this.displayModal = false;
    this.recordId = 0;
  }

  mapData(record: any) {
    let birthDate = new Date(this.birthDate.value);

    return {
      firstName: record.firstName,
      lastName: record.lastName,
      middleName: record.middleName,
      address: record.address,
      civilStatus: record.civilStatus.value,
      age: +record.age,
      birthDate: birthDate,
      networkId: record.network.value,
      gender: record.gender.value,
      contactNumber: record.contactNumber,
    }
  }

  onSubmit() {
    if (this.formModel.valid) {
      let record = this.formModel.getRawValue();
      let mappedData = this.mapData(record);
      console.log('to save: ', mappedData);

      if (this.recordId > 0) {
        // this.confirmationService.confirm({
        //   message: ConfirmationMessages.ConfirmUpdate.Message,
        //   header: 'Confirmation',
        //   icon: 'pi pi-exclamation-triangle',
        //   accept: () => {
        this.uiService.block();

        this.guestService.update(mappedData).subscribe({
          next: (data: any) => {
            console.log('updated: ', data);

            this.notifService.showSuccessToast(
              NotificationMessages.SaveSuccessful.Title,
              NotificationMessages.SaveSuccessful.Message);

            this._callLoadService();
            this.recordId = 0;
            setTimeout(() => this.displayModal = false, 1000);
            this.uiService.unBlock()
          },
          error: (e) => {
            this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
            this.uiService.unBlock()
          }
        })
        //   }
        // })
      }
      else {
        // this.confirmationService.confirm({
        //   message: ConfirmationMessages.ConfirmSave.Message,
        //   header: 'Confirmation',
        //   icon: 'pi pi-exclamation-triangle',
        //   accept: () => {
        this.uiService.block();

        this.guestService.create(mappedData).subscribe({
          next: (data: any) => {
            console.log('created: ', data);

            this.notifService.showSuccessToast(
              NotificationMessages.SaveSuccessful.Title,
              NotificationMessages.SaveSuccessful.Message);

            this._callLoadService();
            setTimeout(() => this.displayModal = false, 1000);
            this.uiService.unBlock()
          },
          error: (e) => {
            this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
            this.uiService.unBlock()
          }
        })
        //   }
        // })
      }
    }
    else {
      this.validation.validateAllFormFields(this.formModel);
    }
  }
}
