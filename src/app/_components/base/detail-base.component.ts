import { Injectable, OnInit } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";

import { AppPageBaseComponent } from "./app-page-base.component";
import { NotificationService } from "src/app/services/notification.service";


@Injectable()
export abstract class DetailBaseComponent extends AppPageBaseComponent implements OnInit {

    formMode: string;
    formModel: FormGroup;
    isViewMode: boolean;
    recordId: number;

    abstract _initializeFormModel(): void;
    abstract _loadRecordData(): void;
    abstract _setBreadcrumbs(): void;

    constructor(
        public route: ActivatedRoute,
        public router: Router,
        public override notifService: NotificationService) {
        super(notifService);

        this.getRecordIdFromRoute();

        this.checkViewMode();
    }

    ngOnInit(): void {
        this._setBreadcrumbs();

        this._initializeFormModel();

        if (this.recordId > 0) {
            this._loadRecordData();

            // Enable/Disable Form
            this.isViewMode ? this.formModel.disable() : this.formModel.enable();

            this.formMode = this.isViewMode ? 'View' : 'Edit';
        }
        else {
            this.formMode = 'Add';
        }
    }

    /******************************************************************************/

    private checkViewMode() {
        this.isViewMode = !this.router.url.includes("detail");
    }

    public formatDate(date) {
        const d = new Date(date);
        let month = '' + (d.getMonth() + 1);
        let day = '' + d.getDate();
        let year = d.getFullYear();

        if (month.length < 2) {
            month = '0' + month;
        }

        if (day.length < 2) {
            day = '0' + day;
        }

        return [year, month, day].join('-');
    }

    public formatDateToYearMonth(date) {
        const d = new Date(date);
        let month = '' + (d.getMonth() + 1);
        let year = d.getFullYear();

        if (month.length < 2) {
            month = '0' + month;
        }

        return [year, month].join('-');
    }

    private getRecordIdFromRoute() {
        this.route.params.subscribe(p => {
            if (+p['id']) {
                this.recordId = +p['id'];
            }
        });
    }
}