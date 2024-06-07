import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ServiceService extends BaseService {
  readonly resource: string = 'Service';

  constructor(override http: HttpClient) { super(http) }

  getServicesList() {
    return this.http.get(this.API_URL + `/${this.resource}/getServices`);
  }
}
