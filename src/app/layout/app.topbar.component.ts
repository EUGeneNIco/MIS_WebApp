import { Component, ElementRef, ViewChild } from '@angular/core';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { LayoutService } from "./service/app.layout.service";
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ConfirmationMessages } from '../_enums/confirmation-messages';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent {

    items!: MenuItem[];

    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;

    constructor(public layoutService: LayoutService, public authService: AuthService, public router: Router, public confirmationService: ConfirmationService) { }

    signOut() {
        this.confirmationService.confirm({
            message: ConfirmationMessages.ConfirmLogout.Message,
            header: 'Confirmation',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                this.authService.destroyAuthToken();
                this.router.navigate(['login']);
            }
        })
    }
}
