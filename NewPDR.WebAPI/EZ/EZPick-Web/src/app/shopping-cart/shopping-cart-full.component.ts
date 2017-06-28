import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ShoppingBasket }  from '../shared/shopping-basket';

import { ShoppingCartService }           from '../shared/shopping-cart.service';


@Component({
    selector: 'my-shopping-cart-full',
    templateUrl: './shopping-cart-full.component.html'
})

export class ShoppingCartFullComponent implements OnInit {

    shoppingcount: number;
    items: any;
    subscription: any;
    addedItems: ShoppingBasket[];
    totalPrice: number;

    constructor(private shoppingCartService: ShoppingCartService, private router: Router) { }

    ngOnInit() {

        this.addedItems = this.shoppingCartService.getAddedShoppingCartItems();
        this.totalPrice = this.shoppingCartService.GetTotalPrice(this.addedItems);

        this.subscription = this.shoppingCartService.productAddedToBasket.subscribe((value) => {
            this.shoppingcount = value;
            this.totalPrice = this.shoppingCartService.GetTotalPrice(this.addedItems);

        });

        this.shoppingCartService.itemsAddedToBasket
            .subscribe((items) => {
                this.addedItems = items;
            }
            );



    }



    onCheckOutClicked() {
        this.router.navigate(['review-pay']);
    }

    removeProduct(product) {
        this.shoppingCartService.removeProduct(product);
    }

    minusQty(items) {
        this.shoppingCartService.minusQty(items);
    }

    plusQty(items) {
        this.shoppingCartService.plusQty(items);
    }
    onContinueShoppingClicked()
    {
         this.router.navigate(['home']);
    }
}