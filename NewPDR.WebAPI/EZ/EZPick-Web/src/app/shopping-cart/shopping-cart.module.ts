import { NgModule }                     from '@angular/core';
import { CommonModule }                 from '@angular/common';
import { RouterModule }                 from '@angular/router';
import { MaterialModule }               from '@angular/material';

import { ShoppingCartComponent }        from './shopping-cart.component';
import { ShoppingCartFullComponent }    from './shopping-cart-full.component';

import { ProductsService }              from '../products/products.service';

import { SharedModule }                 from '../shared/shared.module';



@NgModule({
    imports: [
        CommonModule,
        RouterModule,
        MaterialModule.forRoot(),
        SharedModule
    ],
    declarations: [
        ShoppingCartComponent,
        ShoppingCartFullComponent
    ],
    exports: [
        ShoppingCartComponent,
        ShoppingCartFullComponent
    ],
    providers: [
        ProductsService
    ]
})
export class ShoppingCartModule {

}