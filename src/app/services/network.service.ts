import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class NetworkService extends BaseService {
  readonly resource: string = 'Network';

  constructor(override http: HttpClient) { super(http) }

  getNetworks() {
    return this.http.get(this.API_URL + `/${this.resource}/getList`);
  }
}
