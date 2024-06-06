import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AttendancelogService extends BaseService {
  readonly resource: string = 'AttendanceLog';

  constructor(override http: HttpClient) { super(http) }

  log(data: any) {
    return this.http.post(this.API_URL + `/${this.resource}/log`, data);
  }

  getQuery(data: any) {
    return this.http.post(this.API_URL + `/${this.resource}/getQuery`, data);
  }
}
