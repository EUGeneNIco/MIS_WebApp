import { Component, OnInit } from '@angular/core';
import { ConfirmationService, LazyLoadEvent, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { AppPageBaseComponent } from 'src/app/_components/base/app-page-base.component';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { IDataParameter } from 'src/app/_models/data-parameter.model';
import { UiService } from 'src/app/layout/service/ui.service';
import { MemberService } from 'src/app/services/member.service';
import { NotificationService } from 'src/app/services/notification.service';
import * as XLSX from 'xlsx';
import { EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-import-member-data',
  templateUrl: './import-member-data.component.html',
  styleUrl: './import-member-data.component.scss'
})
export class ImportMemberDataComponent extends AppPageBaseComponent implements OnInit {
  excelData: any;

  uploadedData: any[] = [];

  loading: boolean;
  hasLoaded: boolean = false;
  hasLoadedButNotSuccessful: boolean = false;
  ongoingTransaction: boolean = false;

  // grid fields

  cols: any[];
  dataParameter: IDataParameter;
  rows: any[];
  totalRecords: number;
  totalRecordCountLabel: string
  table: Table;

  @Output() initComponent = new EventEmitter();

  constructor(
    private messageService: MessageService,
    notifService: NotificationService,
    public confirmationService: ConfirmationService,
    public uiService: UiService,
    public memberService: MemberService,
  ) {
    super(notifService);
  }
  ngOnInit(): void {
    this._setBreadcrumbs();

    this.dataParameter = {
      filters: [],
      sortKey: '',
      sortDirection: 1,
      offset: 0,
      limit: 0
    };

    this.cols = [
      { field: 'NAME', filter: false, header: 'Name', sortable: false, type: 'text' },
      { field: 'ADDRESS', filter: false, header: 'Address', sortable: false, type: 'text' },
      { field: 'BIRTHDAY', filter: false, header: 'Birthday', sortable: false, type: 'text' },
      { field: 'AGE', filter: false, header: 'Age', sortable: false, type: 'text' },
      { field: 'GENDER', filter: false, header: 'Gender', sortable: false, type: 'text' },
      { field: 'CONTACTNUMBER', filter: false, header: 'Contact No.', sortable: false, type: 'text' },
      { field: 'CIVILSTATUS', filter: false, header: 'Civil Status', sortable: false, type: 'text' },
      { field: 'CATEGORY', filter: false, header: 'Category', sortable: false, type: 'text' },
      { field: 'CODE', filter: false, header: 'Code', sortable: false, type: 'text' },
      { field: 'NETWORK', filter: false, header: 'Network', sortable: false, type: 'text' },
      { field: 'EXTENSION', filter: false, header: 'Extension', sortable: false, type: 'text' },
      // { field: 'FIRSTNAME', filter: false, header: 'First Name', sortable: false, type: 'text' },
      // { field: 'MIDDLENAME', filter: false, header: 'Middle Name', sortable: false, type: 'text' },
      // { field: 'LASTNAME', filter: false, header: 'Last Name', sortable: false, type: 'text' },
      // { field: 'NO.', filter: false, header: 'Action', sortable: false, type: 'actions' }
    ];

    // this.loading = true;
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

  openImportModal(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      if (this.isValidExcelFile(file)) {
        this.readExcel(file);
      } else {
        this.hasLoadedButNotSuccessful = true;
        this.notifService.showErrorToast(
          NotificationMessages.GenericError.Title,
          'Please select a valid Excel file.');
      }
    }
  }

  isValidExcelFile(file: File): boolean {
    const validExtensions = ['xls', 'xlsx'];
    const fileExtension = file.name.split('.').pop();
    return fileExtension ? validExtensions.includes(fileExtension.toLowerCase()) : false;
  }

  readExcel(file: File) {
    let fileReader = new FileReader();
    fileReader.readAsBinaryString(file);
    fileReader.onload = (e) => {
      var wb = XLSX.read(fileReader.result, { type: 'binary' });
      var sheetNames = wb.SheetNames;
      this.excelData = XLSX.utils.sheet_to_json(wb.Sheets[sheetNames[0]]);

      this.validateImportedData();
    }
  }

  validateImportedData() {
    let validated = true;

    if (!this.excelData || this.excelData.length === 0) validated = false

    let firstData = this.excelData[0];

    let firstNameData = firstData['FIRSTNAME'];
    let lastNameData = firstData['LASTNAME'];

    if (!firstNameData || !lastNameData) validated = false;

    if (validated) {
      console.log('file validated!')
      this.hasLoaded = true;
      this.reloadData();
    }
    else {
      this.hasLoadedButNotSuccessful = true;
      this.notifService.showErrorToast(
        NotificationMessages.GenericError.Title,
        'There is something wrong with the file you have uploaded. Please check if you are selecting the correct file.');
    }

    this.loading = false;
  }

  mapData() {
    return this.excelData.map(d => {
      let birthDate: string = null;

      if (d.BIRTHDAY) {
        // DATE in excel is a serial, so we need to transform it to date in JS
        // Excel serial date starts from January 1, 1900
        const excelEpoch = new Date(1900, 0, 1);
        // Adjust for the serial number 1 in Excel representing January 1, 1900
        const jsDate = new Date(excelEpoch.getTime() + (d.BIRTHDAY - 1) * 24 * 60 * 60 * 1000);

        if (jsDate && !isNaN(jsDate.getTime())) {
          // Create a new Date object
          birthDate = jsDate.toISOString(); // Note: Month is 0-based in JS Date
        }
      }

      return {
        firstName: d.FIRSTNAME ?? '',
        middleName: d.MIDDLENAME ?? '',
        lastName: d.LASTNAME ?? '',
        memberCode: d.CODE ?? '',
        address: d.ADDRESS ?? '',
        gender: d.GENDER ?? '',
        category: d.CATEGORY ?? '',
        contactNumber: d.CONTACTNUMBER && d.CONTACTNUMBER !== undefined ? String(d.CONTACTNUMBER) : '',
        civilStatus: d.CIVILSTATUS ?? '',
        extension: d.EXTENSION ?? '',
        birthDate: birthDate ?? null,
        age: d.AGE && d.AGE != undefined ? String(d.AGE) : null,
        networkImported: d.NETWORK ?? '',
      }
    })
  }

  onCancel() {
    this.initializeComponent();
  }

  initializeComponent(fromParent?: boolean) {
    this.hasLoaded = false;
    this.hasLoadedButNotSuccessful = false;
    if (!fromParent)
      this.initComponent.emit(true);

    setTimeout(() => this.excelData = [], 3000);
  }

  saveToDb() {
    this.confirmationService.confirm({
      message: 'This action will import member data to database and there is a possibility to update existing records. Proceed?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.ongoingTransaction = true;

        let mappedData = this.mapData();

        this.memberService.import({ importedData: mappedData }).subscribe({
          next: (data: any) => {
            this.notifService.showSuccessToast(
              NotificationMessages.SaveSuccessful.Title,
              NotificationMessages.SaveSuccessful.Message);

            this.initializeComponent();
            this.ongoingTransaction = false;
          },
          error: (e) => {
            this.handleErrorMessage(e, NotificationMessages.SaveError.Message);
            this.uiService.unBlock()
          }
        })
      }
    })
  }

  //********************************* Grid Methods - from Masterbase component *********************************
  _callLoadService() {
    this.reloadData();
  };

  onRowSelect(event: any) {
    const selectedRowData = event.data;
  }

  public loadLazy(event: LazyLoadEvent) {
    // this.loading = true;

    this.dataParameter.sortKey = event.sortField ? event.sortField : ''; // At start, event.sorField is undefined
    this.dataParameter.sortDirection = event.sortOrder;
    this.dataParameter.offset = event.first;
    this.dataParameter.limit = event.rows;

    this._callLoadService();
  }

  public reloadData() {
    let res = this.excelData;
    if (!res || res.length === 0) return;

    let _recordsFiltered = res.length;
    let _recordsTotal = res.length;
    let _addToStart = _recordsFiltered === 0 ? 0 : 1;

    this.totalRecordCountLabel = _recordsFiltered == _recordsTotal
      ? `Showing ${this.dataParameter.offset + _addToStart} to ${this.dataParameter.offset + res.length} of ${_recordsFiltered} entries`
      : `Showing ${this.dataParameter.offset + _addToStart} to ${this.dataParameter.offset + res.length} of ${_recordsFiltered} entries (filtered from ${_recordsTotal} entries)`;

    this.totalRecords = res.length;

    this.rows = this.dataParameter.limit > 0
      ? res.slice(this.dataParameter.offset, this.dataParameter.offset + this.dataParameter.limit)
      : res;

    this.loading = false;
  }

  public removeFilterData(filterField: string): void {
    let dataIndex = this.dataParameter.filters.findIndex(p => p.field == filterField);

    this.dataParameter.filters.splice(dataIndex, 1);
  }

  private upSertFilterData(filterField: string, filterValue: string): void {
    let filterData = this.dataParameter.filters.find(p => p.field == filterField);

    if (filterData == null) {
      this.dataParameter.filters.push({ field: filterField, value: filterValue })
    } else {
      filterData.value = filterValue;
    }
  }
}
