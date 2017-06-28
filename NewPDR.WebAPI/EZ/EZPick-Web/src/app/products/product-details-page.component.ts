import { Component, OnInit }  from '@angular/core';

import { ActivatedRoute, Params } from '@angular/router';

import {ProductsService }           from './products.service';
import { ShoppingCartService }           from '../shared/shopping-cart.service';

import { Product } from './product';


@Component({
    selector: 'page-details-page',
    templateUrl: './product-details-page.component.html'
})
export class PageDetailsPageComponent implements OnInit {

    productId: number;
    selectedProduct: Product;
    mainImageUrl: string = '';
    quantity:number = 1;

    constructor(private activatedRoute: ActivatedRoute, private productsService: ProductsService, private shoppingCartService: ShoppingCartService) { }

    ngOnInit() {

        this.activatedRoute.params.forEach((params: Params) => {
            this.productId = +params['id'];

            this.productsService.getProduct(this.productId).
                subscribe(response => {
                    this.selectedProduct = response;
                    this.mainImageUrl = this.selectedProduct.productImages[0].imageUrl;
                    this.quantity = 1;
                })
        }
        );
    }

    showSelectedImage(selectedImage) {
        this.mainImageUrl = selectedImage.imageUrl
    }

    addToShoppingBag(item: Product) {
        this.shoppingCartService.addToCart(item, this.quantity);
    }
}
