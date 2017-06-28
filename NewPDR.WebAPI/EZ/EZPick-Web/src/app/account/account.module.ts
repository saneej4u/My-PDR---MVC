import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule }               from '@angular/material';

import { FormsModule }                  from '@angular/forms';

import { ReactiveFormsModule } from '@angular/forms';
import { accountRouting } from './account.routing';



import { AuthService }   from './auth.service';

import { AccountComponent }  from './account.component';
import { SignUpComponent }    from './sign-up.component';


@NgModule({
    imports: [
        CommonModule,
        accountRouting,
        ReactiveFormsModule,
        FormsModule,
        MaterialModule.forRoot(),
    ],
    declarations: [
        AccountComponent,
        SignUpComponent

    ],
    exports: [
        AccountComponent,
        SignUpComponent

    ],
    providers: [
        AuthService
    ]
})
export class AccountModule { }