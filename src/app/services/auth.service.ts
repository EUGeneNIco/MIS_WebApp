import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Globals } from '../_auth/globals';
import { UserRoles } from '../_enums/UserRoles';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {
  readonly resource: string = 'Auth';


  get authToken(): string {
    return localStorage.getItem(this.globals.TOKEN_NAME);
  }

  set authToken(val: string) {
    localStorage.setItem(this.globals.TOKEN_NAME, val);
  }

  get userID(): number {
    const payLoad = JSON.parse(this.getPayload());
    const userId = payLoad.UserGuid;

    return +userId;
  }

  get userName(): string {
    const payLoad = JSON.parse(this.getPayload());

    return payLoad.unique_name;
  }

  get role(): string {
    const payLoad = JSON.parse(this.getPayload());

    return payLoad.role;
  }

  constructor(
    override http: HttpClient,
    private globals: Globals
  ) { super(http) }

  login(data: any) {
    return this.http.post(this.API_URL + `/${this.resource}/login`, data, { responseType: 'text' });
  }

  destroyAuthToken() {
    localStorage.removeItem(this.globals.TOKEN_NAME);
  }

  userRoleHasAccessToResource(roleToCheck: string): boolean {
    let hasAccess = this.role === roleToCheck;
    if (!hasAccess && this.role === UserRoles.Admin) hasAccess = true;

    return hasAccess;
  }

  getPayload() {
    return window.atob(this.authToken.split('.')[1])
  }
}
