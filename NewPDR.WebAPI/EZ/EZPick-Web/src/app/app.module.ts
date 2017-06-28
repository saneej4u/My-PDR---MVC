import { BrowserModule }      from '@angular/platform-browser';
import { NgModule }           from '@angular/core';
import { RouterModule }        from '@angular/router';
import { FormsModule }        from '@angular/forms';
import { HttpModule }         from '@angular/http';
import { MaterialModule }     from '@angular/material';

import  { ProductsModule }    from './products/products.module';
import { ShoppingCartModule } from './shopping-cart/shopping-cart.module';
import { SharedModule }                 from './shared/shared.module';
import { CheckoutModule  }    from './checkout/checkout.module';
import { AccountModule }       from './account/account.module';

import { routing }            from './app.routing';

import { AppComponent }       from './app.component';
import { HomeComponent }      from './home.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule,
    HttpModule,
    MaterialModule.forRoot(),
    routing,
    ProductsModule,
    ShoppingCartModule,
    SharedModule,
    CheckoutModule,
    AccountModule
  ],
  providers: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
