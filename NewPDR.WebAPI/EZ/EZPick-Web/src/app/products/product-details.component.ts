import { Component, OnInit }  from '@angular/core';

import {ProductsService }           from './products.service';
import { ShoppingCartService }           from '../shared/shopping-cart.service';

import { Product } from './product';


@Component({
    selector: 'page-details',
    templateUrl: './product-details.component.html'
})
export class PageDetailsComponent implements OnInit {

    productId: number;
    selectedProduct: Product;
    mainImageUrl: string = '';
    quantity:number;

    constructor(private productsService: ProductsService, private shoppingCartService: ShoppingCartService) { }

    ngOnInit() {
            this.productsService.getProduct(this.productId).
                subscribe(response => {
                    this.selectedProduct = response;
                    this.mainImageUrl = this.selectedProduct.productImages[0].imageUrl;
                    this.quantity = 1;
                })
     
    }

    showSelectedImage(selectedImage) {
        this.mainImageUrl = selectedImage.imageUrl
    }

    addToShoppingBag(item: Product) {
        this.shoppingCartService.addToCart(item, this.quantity);
    }
}
