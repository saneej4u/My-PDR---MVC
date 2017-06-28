import { Component, OnInit  } from '@angular/core';
import { MdDialogRef } from '@angular/material';
import { Product }                  from './product';

import { ShoppingCartService }           from '../shared/shopping-cart.service';


@Component({

    selector: 'quick-view-dialog',
    templateUrl: './quick-view-dialog.component.html'
})
export class QuickViewDialogComponent implements OnInit {

    productId: number;
    selectedProduct: Product;
    mainImageUrl: string = '';
    quantity: number = 1;

    constructor(public dialogRef: MdDialogRef<QuickViewDialogComponent>, private shoppingCartService: ShoppingCartService) { }

    ngOnInit() {
        this.mainImageUrl = this.selectedProduct.productImages[0] !=null ? this.selectedProduct.productImages[0].imageUrl: "";
        this.quantity = 1;
    }

    showSelectedImage(selectedImage) {
        this.mainImageUrl = selectedImage.imageUrl
    }

    addToShoppingBag(item: Product) {
        this.shoppingCartService.addToCart(item, this.quantity);
    }

}