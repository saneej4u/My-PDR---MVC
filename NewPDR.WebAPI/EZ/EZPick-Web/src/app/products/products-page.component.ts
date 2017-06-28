import { Component, OnInit, OnDestroy} from '@angular/core';

import { ActivatedRoute } from '@angular/router';

import { Product }                  from './product';
import {ProductsService }           from './products.service';
import { ProductType } from './product-type';


@Component({
    selector: 'product-page',
    templateUrl: './products-page.component.html'
})
export class ProductsPageComponent implements OnInit, OnDestroy {

    private sub: any;
    id: number;
    products: Product[];
    public isRequesting: boolean;
    productTypeHeader: string;

    constructor(private activatedRoute: ActivatedRoute, private productsService: ProductsService) {
        this.isRequesting = true;
    }

    ngOnInit() {

        this.sub = this.activatedRoute.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number

            this.productsService.getProductTypeById(this.id).subscribe((response: ProductType) => {

                this.productTypeHeader = response.name;
            })

            this.productsService.getProducts(this.id)
                .subscribe((response: Product[]) => {
                    this.products = response;
                }, error => {
                    this.isRequesting = false;
                }, () => {
                    this.isRequesting = false;
                })

        });

    }

    ngOnDestroy() {
        this.sub.unsubscribe();
    }
}