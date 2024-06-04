import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { NotfoundComponent } from './demo/components/notfound/notfound.component';
import { AppLayoutComponent } from "./layout/app.layout.component";
import { ImportMemberDataComponent } from './pages/management/import-member-data/import-member-data.component';
import { GuestComponent } from './pages/management/guest/guest.component';
import { LoginComponent } from './pages/auth/login/login.component';
import { ForbiddenComponent } from './pages/auth/forbidden/forbidden.component';
import { AuthGuard } from './_auth/auth.guard';
import { DashboardComponent } from './pages/common/dashboard/dashboard.component';
import { MemberComponent } from './pages/management/member/member.component';
import { UserRoles } from './_enums/UserRoles';
import { AttendancelogComponent } from './pages/transaction/attendancelog/attendancelog.component';

@NgModule({
    imports: [
        RouterModule.forRoot([
            {
                path: '', component: AppLayoutComponent,
                children: [
                    // { path: '', loadChildren: () => import('./demo/components/dashboard/dashboard.module').then(m => m.DashboardModule) },
                    { path: 'uikit', loadChildren: () => import('./demo/components/uikit/uikit.module').then(m => m.UIkitModule) },
                    { path: 'utilities', loadChildren: () => import('./demo/components/utilities/utilities.module').then(m => m.UtilitiesModule) },
                    { path: 'documentation', loadChildren: () => import('./demo/components/documentation/documentation.module').then(m => m.DocumentationModule) },
                    { path: 'blocks', loadChildren: () => import('./demo/components/primeblocks/primeblocks.module').then(m => m.PrimeBlocksModule) },
                    { path: 'pages', loadChildren: () => import('./demo/components/pages/pages.module').then(m => m.PagesModule) },

                    { path: '', component: DashboardComponent, canActivate: [AuthGuard] },
                    { path: 'management/import-member-data', component: ImportMemberDataComponent, canActivate: [AuthGuard], data: { role: UserRoles.Admin } },
                    { path: 'management/guest', component: GuestComponent, canActivate: [AuthGuard], data: { role: UserRoles.Admin } },
                    { path: 'management/member', component: MemberComponent, canActivate: [AuthGuard], data: { role: UserRoles.Admin } },

                    { path: 'transaction/attendance-log', component: AttendancelogComponent, canActivate: [AuthGuard], data: { role: UserRoles.Staff } },
                ],
            },
            { path: 'login', component: LoginComponent },
            { path: 'access-denied', component: ForbiddenComponent },
            { path: 'auth', loadChildren: () => import('./demo/components/auth/auth.module').then(m => m.AuthModule) },
            { path: 'landing', loadChildren: () => import('./demo/components/landing/landing.module').then(m => m.LandingModule) },
            { path: 'notfound', component: NotfoundComponent },
            { path: '**', redirectTo: '/notfound' },
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
