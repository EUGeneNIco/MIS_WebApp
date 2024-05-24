import { Injectable } from "@angular/core";

import { LazyLoadEvent, SelectItem } from "primeng/api";
import { Table } from "primeng/table";

import { IDataParameter } from "src/app/_models/data-parameter.model";

@Injectable()
export abstract class GridViewBaseComponent {

    cols: any[];
    dataParameter: IDataParameter;
    rows: any[];
    loading: boolean;
    totalRecords: number;
    totalRecordCountLabel: string

    table: Table;

    constructor() {

        this.dataParameter = {
            filters: [],
            sortKey: '',
            sortDirection: 1,
            offset: 0,
            limit: 0
        };
    }

    abstract _callLoadService(): void;

    public columnFilter(filterField: string, filterValue: string) {
        if (filterValue == null || filterValue == '') {
            this.removeFilterData(filterField);
        }
        else {
            this.upSertFilterData(filterField, filterValue);
        }

        // Fix paging/offset when filtering
        this.table.reset();

        // this._callLoadService(); // Unnecessary call? Results to double call to Api
    }

    public loadLazy(event: LazyLoadEvent) {
        this.loading = true;

        this.dataParameter.sortKey = event.sortField ? event.sortField : ''; // At start, event.sorField is undefined
        this.dataParameter.sortDirection = event.sortOrder;
        this.dataParameter.offset = event.first;
        this.dataParameter.limit = event.rows;

        // console.log(this.dataParameter);

        this._callLoadService();
    }

    public reloadData(res) {
        // console.log(res);

        let _recordsFiltered = res["filteredDataCount"];
        let _recordsTotal = res["totalDataCount"];
        let _addToStart = _recordsFiltered === 0 ? 0 : 1;

        this.totalRecordCountLabel = _recordsFiltered == _recordsTotal
            ? `Showing ${this.dataParameter.offset + _addToStart} to ${this.dataParameter.offset + res["data"].length} of ${_recordsFiltered} entries`
            : `Showing ${this.dataParameter.offset + _addToStart} to ${this.dataParameter.offset + res["data"].length} of ${_recordsFiltered} entries (filtered from ${_recordsTotal} entries)`;

        this.totalRecords = res["filteredDataCount"];
        this.rows = res["data"];

        this.loading = false;
    }

    public removeFilterData(filterField: string): void {
        let dataIndex = this.dataParameter.filters.findIndex(p => p.field == filterField);

        this.dataParameter.filters.splice(dataIndex, 1);

        // console.log(this.dataParameter.filters);
    }

    private upSertFilterData(filterField: string, filterValue: string): void {
        let filterData = this.dataParameter.filters.find(p => p.field == filterField);

        if (filterData == null) {
            this.dataParameter.filters.push({ field: filterField, value: filterValue })
        } else {
            filterData.value = filterValue;
        }

        // console.log(this.dataParameter.filters);
    }
}