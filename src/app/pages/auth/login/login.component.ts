import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ApiCallStatusCodes } from 'src/app/_enums/enums';
import { NotificationMessages } from 'src/app/_enums/notification-messages';
import { Validation } from 'src/app/_helpers/validation';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { AuthService } from 'src/app/services/auth.service';
import { NotificationService } from 'src/app/services/notification.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent {

    valCheck: string[] = ['remember'];

    formModel: FormGroup;
    get username() { return this.formModel.get('username'); }
    get password() { return this.formModel.get('password'); }

    constructor(
        public layoutService: LayoutService,
        public authService: AuthService,
        public fb: FormBuilder,
        public validation: Validation,
        public notifService: NotificationService,
        public router: Router) {

        if (!this.formModel) {
            this.formModel = this.fb.group({
                username: ['', Validators.required],
                password: ['', Validators.required]
            })
        }
    }

    signIn() {
        if (this.formModel.valid) {
            const creds = this.formModel.getRawValue();
            this.authService.login(creds).subscribe({
                next: (data: string) => {
                    this.authService.authToken = data;
                    this.router.navigateByUrl('');
                },
                error: (err) => {
                    console.log('Error in log in: ', err);
                    if (err.status == 0) {
                        this.notifService.showErrorToast(
                            NotificationMessages.ApiError.Title,
                            NotificationMessages.ApiError.Message);
                    }
                    else if (err.status == ApiCallStatusCodes.BADREQUEST) {
                        this.notifService.showErrorToast(
                            'Oops!',
                            err.error == null ? 'Something is wrong with your request.' : err.error);
                    }
                    else if (err.status == ApiCallStatusCodes.UNAUTHORIZED) {
                        this.notifService.showErrorToast(
                            'Unauthorized',
                            err.error);
                    }
                    else {
                        this.notifService.showErrorToast(
                            NotificationMessages.GenericError.Title,
                            err.Message == null ? NotificationMessages.GenericError.Message : err.error);
                    }
                }
            })
        }
        else {
            this.validation.validateAllFormFields(this.formModel);
        }
    }
}
