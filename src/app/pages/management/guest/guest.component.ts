import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, SelectItem } from 'primeng/api';
import { Table } from 'primeng/table';
import { MasterBaseComponent } from 'src/app/_components/base/master-base.component';
import { ConfirmationMessages } from 'src/app/_enums/confirmation-messages';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { DateUtils } from 'src/app/_helpers/dateUtils';
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
  initialGridParams: any;
  civilStatuses: SelectItem[] = [
    { label: 'Single', value: 'Single' },
    { label: 'Married', value: 'Married' },
    { label: 'Widowed', value: 'Widowed' },
  ];
  extensions: SelectItem[] = [
    { label: 'Cabuyao', value: 'Cabuyao' },
    { label: 'HK Village', value: 'HK Village' },
    { label: 'SJV7', value: 'SJV7' },
  ];
  networks: SelectItem[] = [
    { label: 'KKB/CYN', value: 1 },
    { label: 'Men', value: 2 },
    { label: 'Women', value: 3 },
    { label: 'Children', value: 4 },
    { label: 'Y-AM', value: 5 },
  ];
  genders: SelectItem[] = [
    { label: 'Male', value: 'Male' },
    { label: 'Female', value: 'Female' },
  ];
  readonly createFormTitle: string = 'Guest Registration Form';
  readonly updateFormTitle: string = 'Guest Details';

  // Modal
  displayModal: boolean;
  formMode: string;
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
  get extension() { return this.formModel.get('extension'); }

  get addMode() {
    return this.formMode === 'Add';
  }

  @ViewChild('dt') dt: Table;
  constructor(
    private guestService: GuestService,
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
      { field: 'name', filter: true, header: 'Name', sortable: true, type: 'text' },
      { field: 'address', filter: false, header: 'Address', sortable: false, type: 'text' },
      { field: 'network', filter: false, header: 'Network', sortable: true, type: 'text' },
      { field: 'contactNumber', filter: false, header: 'Contact No.', sortable: false, type: 'text' },
      { field: 'id', filter: false, header: 'Action', sortable: false, type: 'actions' }
    ];

    this.loading = true;
    this.initialGridParams = Object.assign({}, this.dataParameter);
  }


  ngAfterViewInit(): void {
    this.table = this.dt;
  }

  _callLoadService(): void {
    this.guestService.getGrid(this.dataParameter).subscribe({
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
        firstName: ['', Validators.required],
        middleName: [''],
        lastName: ['', Validators.required],
        address: [''],
        age: [''],
        birthDate: [''],
        civilStatus: [''],
        contactNumber: [''],
        network: [''],
        gender: [''],
        extension: [''],
      })
    }
  }

  _setBreadcrumbs(): void {
    this.setBreadcrumbs([
      { label: 'Management' },
      { label: 'Guest', url: '' }
    ]);
  }

  reInitGrid() {
    this.dataParameter = this.initialGridParams;
    this._callLoadService();
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

  openModal() {
    this.formMode = 'Add';
    this.displayModal = true;
    this.editMode = true;
    this.formModel.enable();
  }

  onRowSelect(event: any) {
    this.formMode = 'View';
    this.editMode = false;
    this.displayModal = true;
    this.formModel.disable();

    const selectedRowId = event.data['id'];
    this.getDetails(selectedRowId);
  }

  onEditMode() {
    this.formMode = 'Edit';
    this.formModel.enable();
    this.editMode = true;
  }

  openEditModal(record: any) {
    const recordId = record.id;
    this.displayModal = true;
    this.onEditMode();
    this.getDetails(recordId);
  }

  getDetails(id: any) {
    this.recordId = id;
    this.guestService.get(id).subscribe({
      next: (data: any) => {
        // console.log("get data: ", data);

        this.formModel.patchValue(data);

        this.civilStatus.setValue(this.civilStatuses.find(x => x.value === data.civilStatus));
        this.gender.setValue(this.genders.find(x => x.value === data.gender));
        this.network.setValue(this.networks.find(x => x.value === data.networkId));
        this.extension.setValue(this.extensions.find(x => x.value === data.extension));

        if (data.networkId) {
          this.network.setValue(this.networks.find(x => x.value === data.networkId));
        }

        if (data.birthDate) {
          const formattedDate = new Date(data.birthDate)
          this.birthDate.setValue(formattedDate);
        }
      },
      error: (e) => {
        this.handleErrorMessage(e, NotificationMessages.GenericError.Message);
      }
    })
  }

  deleteRecord(record: any) {
    this.confirmationService.confirm({
      message: ConfirmationMessages.ConfirmDelete.Message,
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.uiService.block();
        const recordId = record.id;
        if (recordId > 0) {
          this.guestService.delete(recordId).subscribe({
            next: (data: any) => {
              // console.log('deleted: ', data);

              this.notifService.showSuccessToast(
                NotificationMessages.DeleteSuccessful.Title,
                NotificationMessages.DeleteSuccessful.Message);

              this.reInitGrid();
              setTimeout(() => this.closeModal(), 500);
              this.uiService.unBlock();
            },
            error: (e) => {
              this.handleErrorMessage(e, NotificationMessages.DeleteError.Message);
              this.uiService.unBlock()
            }
          })
        }

      }
    })

  }

  closeModal() {
    this.displayModal = false;
  }

  onExit() {
    this.recordId = 0;
    this.formModel.reset();
  }

  mapData(record: any) {
    let birthDate = null;
    if (record.birthDate)
      birthDate = DateUtils.getFormattedDate(record.birthDate);

    return {
      firstName: record.firstName ?? '',
      lastName: record.lastName ?? '',
      middleName: record.middleName ?? '',
      address: record.address ?? '',
      civilStatus: record.civilStatus?.value ?? '',
      age: +record.age,
      birthDate: birthDate,
      networkId: record.network?.value ?? null,
      gender: record.gender?.value ?? '',
      contactNumber: record.contactNumber ?? '',
      extension: record.extension?.value ?? '',
    }
  }

  onSubmit() {
    if (this.formModel.valid) {
      let record = this.formModel.getRawValue();
      let mappedData = this.mapData(record);
      // console.log('mapped: ', mappedData);

      if (this.recordId > 0) {
        this.confirmationService.confirm({
          message: ConfirmationMessages.ConfirmUpdate.Message,
          header: 'Confirmation',
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
            this.uiService.block();

            this.guestService.update({ ...mappedData, id: this.recordId }).subscribe({
              next: (data: any) => {
                // console.log('updated: ', data);

                this.notifService.showSuccessToast(
                  NotificationMessages.SaveSuccessful.Title,
                  NotificationMessages.SaveSuccessful.Message);

                this.reInitGrid();
                this.recordId = 0;
                setTimeout(() => this.closeModal(), 500);
                this.uiService.unBlock();
              },
              error: (e) => {
                this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
                this.uiService.unBlock()
              }
            })
          }
        })
      }
      else {
        this.confirmationService.confirm({
          message: ConfirmationMessages.ConfirmSave.Message,
          header: 'Confirmation',
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
            this.uiService.block();

            this.guestService.create(mappedData).subscribe({
              next: (data: any) => {
                // console.log('created: ', data);

                this.notifService.showSuccessToast(
                  NotificationMessages.SaveSuccessful.Title,
                  NotificationMessages.SaveSuccessful.Message);

                this.reInitGrid();
                setTimeout(() => this.closeModal(), 500);
                this.uiService.unBlock()
              },
              error: (e) => {
                this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
                this.uiService.unBlock()
              }
            })
          }
        })
      }
    }
    else {
      this.validation.validateAllFormFields(this.formModel);
    }
  }
}
