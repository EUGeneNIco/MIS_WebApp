import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BreadcrumbModule } from 'primeng/breadcrumb';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ListboxModule } from 'primeng/listbox';
import { MultiSelectModule } from 'primeng/multiselect';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RippleModule } from 'primeng/ripple';
import { StepsModule } from 'primeng/steps';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToggleButtonModule } from 'primeng/togglebutton';
import { ToolbarModule } from 'primeng/toolbar';

import { FieldErrorDisplayComponent } from './field-error-display/field-error-display.component';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';

@NgModule({
    declarations: [
        FieldErrorDisplayComponent,
        // MemberSelectDialogComponent,
    ],
    exports: [
        FieldErrorDisplayComponent,
        // MemberSelectDialogComponent,
    ],
    imports: [
        ButtonModule,
        BreadcrumbModule,
        CalendarModule,
        CardModule,
        CheckboxModule,
        CommonModule,
        ConfirmDialogModule,
        DialogModule,
        DropdownModule,
        FormsModule,
        InputNumberModule,
        InputTextareaModule,
        InputTextModule,
        ListboxModule,
        MultiSelectModule,
        OverlayPanelModule,
        RadioButtonModule,
        ReactiveFormsModule,
        RippleModule,
        StepsModule,
        TableModule,
        ToastModule,
        ToggleButtonModule,
        ToolbarModule,
        MessageModule,
        MessagesModule
    ]
})
export class ComponentsModule { }