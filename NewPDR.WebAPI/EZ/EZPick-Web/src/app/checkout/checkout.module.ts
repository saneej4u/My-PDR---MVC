import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { ReviewPayComponent } from './review-pay.component';
import { MaterialModule }     from '@angular/material';

import { ReactiveFormsModule } from '@angular/forms';

import { checkoutRouting }   from './checkout.routing';

import { ShoppingCartModule } from '../shopping-cart/shopping-cart.module';

import { OrderPayComponent } from './order-pay.component';
import { OrderConfirmationComponent } from './order-confirmation.component';

@NgModule({
  imports: [
    CommonModule,
    ShoppingCartModule,
    checkoutRouting,
    MaterialModule.forRoot(),
    ReactiveFormsModule
  ],
  declarations: [
    CheckoutComponent,
    ReviewPayComponent,
    OrderPayComponent,
    OrderConfirmationComponent
  ],
  exports: [
    CheckoutComponent,
    ReviewPayComponent,
    OrderPayComponent,
    OrderConfirmationComponent
  ]
})
export class CheckoutModule { }
