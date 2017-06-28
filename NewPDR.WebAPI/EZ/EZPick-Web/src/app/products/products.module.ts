import { NgModule }                     from '@angular/core';
import { CommonModule }                 from '@angular/common';
import { FormsModule }                  from '@angular/forms';
import { RouterModule }                 from '@angular/router';
import { MaterialModule }               from '@angular/material';

import { productsRouting }              from './products.routing';

import { ProductsComponent }            from './products.component';
import { ProductTypesPageComponent }    from './product-types-page.component';
import { ProductsPageComponent }        from './products-page.component';
import { PageDetailsPageComponent }     from './product-details-page.component';
import {  ProductTypeComponent }        from './product-type.component';
import { QuickViewDialogComponent } from './quick-view-dialog.component';

import { ProductsService }             from './products.service';


import { SharedModule }                 from '../shared/shared.module';




@NgModule({

    imports: [
        CommonModule,
        RouterModule,
        FormsModule,
        productsRouting,
        MaterialModule.forRoot(),
        SharedModule
    ],
    declarations: [
        ProductsComponent,
        ProductTypesPageComponent,
        ProductTypeComponent,
        ProductsPageComponent,
        PageDetailsPageComponent,
        QuickViewDialogComponent
    ],
    exports: [
        ProductsComponent,
        ProductTypesPageComponent,
        ProductTypeComponent,
        ProductsPageComponent
    ],
    entryComponents: [
        QuickViewDialogComponent
    ],
    providers: [
        ProductsService
    ]


})
export class ProductsModule {

}