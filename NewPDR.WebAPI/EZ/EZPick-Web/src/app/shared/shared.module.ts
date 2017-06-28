import { NgModule }                     from '@angular/core';
import { CommonModule }                 from '@angular/common';
import { RouterModule }                 from '@angular/router';
import { MaterialModule }               from '@angular/material';

//import { AccountModule }                from '../account/account.module';

import { ShoppingCartService }             from './shopping-cart.service';
import { AuthGuard }             from './auth-guard.service';
import { SpinnerComponent } from './spinner.component';



@NgModule({

    imports: [
        CommonModule,
        RouterModule,
        MaterialModule.forRoot()
    ],
    declarations: [
        SpinnerComponent
    ],
    exports: [
        SpinnerComponent
    ],
    providers: [
        ShoppingCartService,
        AuthGuard
    ]


})
export class SharedModule {

}