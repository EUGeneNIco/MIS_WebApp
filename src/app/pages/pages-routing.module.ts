import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ImportMemberDataComponent } from './import-member-data/import-member-data.component';

@NgModule({
    imports: [RouterModule.forChild([
        { path: 'import-member-data', data: { breadcrumb: 'Form Layout' }, component: ImportMemberDataComponent },
    ])],
    exports: [RouterModule]
})
export class PagesRoutingModule { }
