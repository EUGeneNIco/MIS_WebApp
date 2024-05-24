import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GuestService extends BaseService {
  readonly resource: string = 'Guest';

  constructor(override http: HttpClient) { super(http) }

  get(id: number) {
    return this.http.get(this.API_URL + `/${this.resource}/` + id);
  }
  create(data: any) {
    return this.http.post(this.API_URL + `/${this.resource}`, data);
  }
  update(data: any) {
    return this.http.put(this.API_URL + `/${this.resource}`, data);
  }
  delete(id: any) {
    console.log(id)
    return this.http.delete(this.API_URL + `/${this.resource}/${id}`);
  }
  getGrid(data: any) {
    return this.http.post(this.API_URL + `/${this.resource}/getGrid`, data);
  }
}
