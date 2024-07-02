import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService extends BaseService {
  readonly resource: string = 'Dashboard';

  constructor(override http: HttpClient) { super(http) }

  getAttendanceData() {
    return this.http.get(this.API_URL + `/${this.resource}/getAttendanceData`);
  }
}