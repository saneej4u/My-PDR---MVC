import { Component, OnInit, AfterContentInit } from '@angular/core';
import { Router } from '@angular/router';

import { ShoppingBasket }  from '../shared/shopping-basket';

import { ShoppingCartService }           from '../shared/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit, AfterContentInit {

  shoppingcount: number;
  items: any;
  subscription: any;
  addedItems: ShoppingBasket[];
  constructor(private shoppingCartService: ShoppingCartService, private router: Router) { }
  totalPrice: number;

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

    this.shoppingCartService.orderPlacedEvent
      .subscribe((items) => {
        this.addedItems = items;
      }
      );

  }

  goToCheckOut() {
    this.router.navigate(['checkout']);
  }

  ngAfterContentInit() {

  }
}
