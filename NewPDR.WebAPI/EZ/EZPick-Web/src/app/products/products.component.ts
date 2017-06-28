import {  Component,
  EventEmitter,
  Input,
  Output,
  OnInit,
  ViewContainerRef }                  from '@angular/core';


import { Router }    from '@angular/router';

import { MdDialog, MdDialogConfig, MdDialogRef } from '@angular/material';

import { Product }                  from './product';
import { ShoppingCartService }           from '../shared/shopping-cart.service';


import {ProductsService }           from './products.service';
import { ShoppingBasket }  from '../shared/shopping-basket';

import { QuickViewDialogComponent } from './quick-view-dialog.component';

@Component({
  selector: 'my-product',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  @Input() product: Product;
  @Output() productAddedToBasket = new EventEmitter();

  dialogRef: MdDialogRef<QuickViewDialogComponent>;

  constructor(private productsService: ProductsService,
    private shoppingCartService: ShoppingCartService,
    private router: Router,
    public dialog: MdDialog,
    public viewContainerRef: ViewContainerRef) { }

  ngOnInit() {


  }

  addToCart(item: Product): void {
    this.shoppingCartService.addToCart(item, 1);
  }

  productClicked(item: Product) {
    this.router.navigate(['/product-details', item.productID]);
  }

  openQuickViewdialog() {

    let config = new MdDialogConfig();
    config.viewContainerRef = this.viewContainerRef;


    this.dialogRef = this.dialog.open(QuickViewDialogComponent, config);
    this.dialogRef.componentInstance.selectedProduct = this.product;


    this.dialogRef.afterClosed().subscribe(result => {
      console.log('result: ' + result);
      this.dialogRef = null;
    });

  }

}
