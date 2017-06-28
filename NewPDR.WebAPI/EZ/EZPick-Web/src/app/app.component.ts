import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router }      from '@angular/router';


import { ProductsService } from './products/products.service';

import { Catgory }          from '../app/products/catgory';
import { ShoppingCartService }           from './shared/shopping-cart.service';
import { AuthService } from './account/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {

  formShowing: boolean = false;
  categories: Catgory[];
  basketCount: number;
  subscription: any;
  isLoggedin: boolean;

  constructor(private productsService: ProductsService, private shoppingCartService: ShoppingCartService, private authService: AuthService, public router: Router) {

  }

  ngOnInit() {
    this.productsService.getCatgories()
      .subscribe((response: Catgory[]) => {
        this.categories = response;

      }, error => {

      })


    this.authService.HasloogedIn.subscribe((items) => {
      this.isLoggedin = items;
    }
    );


    this.basketCount = this.shoppingCartService.getAddedShoppingCartItems().length;

    var subscription = this.shoppingCartService.productAddedToBasket.subscribe((value) => {
      this.basketCount = value;
    });


    this.isLoggedin = this.authService.IsLoggedIn();

  }

  views: Object[] = [
    {
      name: "Living Room",
      description: "Edit my account information",
      icon: "tv"
    },
    {
      name: "Kitchen",
      description: "Find your soulmate!",
      icon: "kitchen"
    },
    {
      name: "Bathroom",
      description: "Edit my account information",
      icon: "pool"
    },
    {
      name: "Bedroom",
      description: "Find your soulmate!",
      icon: "room"
    }
  ];

  someItems: Object[] = [
    {
      name: "Product 1",
      description: "Edit my account information",
      icon: "tv"
    },
    {
      name: "Product 2",
      description: "Find your soulmate!",
      icon: "kitchen"
    },
    {
      name: "Product 3",
      description: "Edit my account information",
      icon: "pool"
    },
    {
      name: "Product 4",
      description: "Find your soulmate!",
      icon: "room"
    }
  ];

  onQuickViewClicked() {

  }

  logOut() {
    this.isLoggedin = false;
    this.authService.logout();
    this.router.navigate(["home"])

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }



}
