import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserRoles } from '../_enums/UserRoles';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard {

    constructor(
        private authService: AuthService,
        private router: Router) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.authToken != null) {
            // Allow Access to Dashboard
            if (state.url === '/') {
                return true;
            }

            const role = next.data['role'];
            let hasAccess = this.authService.userRoleHasAccessToResource(role);
            if (!hasAccess) {
                this.router.navigate(['access-denied']);
            }

            return hasAccess;
        }
        else {
            console.log("Navigated by Auth Guard");
            this.router.navigate(['/login']);

            return false;
        }
    }
}