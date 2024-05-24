// import { AfterViewInit, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';

// import { Table } from 'primeng/table';

// import { MemberService } from 'src/app/_services/member.service';
// import { NotificationService } from 'src/app/_services/notification.service';

// import { GridViewBaseComponent } from '../base/gridview-base.component';

// @Component({
//     selector: 'app-member-select-dialog',
//     styleUrls: ['./member-select-dialog.component.css'],
//     templateUrl: './member-select-dialog.component.html'
// })
// export class MemberSelectDialogComponent extends GridViewBaseComponent implements AfterViewInit, OnInit {

//     display: boolean;

//     selectedMember: any;

//     @Output()
//     memberSelected: EventEmitter<any> = new EventEmitter<any>();

//     @ViewChild('dt') dt: Table;
//     @ViewChild('keyword') keyword: ElementRef;

//     constructor(
//         private memberService: MemberService,
//         public notifService: NotificationService) {
//         super();
//     }

//     ngOnInit() {
//         this.cols = [
//             { field: 'serialNo', filter: true, header: 'Serial No.', sortable: true, type: 'text' },
//             { field: 'name', filter: true, header: 'Name', sortable: true, type: 'text' },
//             { field: 'rank', filter: true, header: 'Rank', sortable: true, type: 'text' },
//             { field: 'branchOfService', filter: true, header: 'Branch of Service', sortable: true, type: 'text' }
//         ];

//         this.loading = true;
//     }

//     ngAfterViewInit(): void {
//         this.table = this.dt;
//     }

//     /******************************************************************************/

//     _callLoadService(): void {
//         this.memberService.getMembersGrid(this.dataParameter).subscribe({
//             next: (data) => this.reloadData(data),
//             error: (e) => {
//                 this.notifService.showErrorToast('Oops!', 'Server error. Please try again later.');
//             }
//         });
//     }

//     /******************************************************************************/

//     onClearFilter() {
//         this.keyword.nativeElement.value = '';

//         this.columnFilter('globalFilter', '');
//     }

//     onRowSelect(event: any) {
//         this.selectedMember = event.data;

//         this.hide();

//         this.memberSelected.emit(this.selectedMember);
//     }

//     /******************************************************************************/

//     hide() {
//         this.display = false;
//     }

//     show() {
//         this.display = true;
//     }
// }