import {    Component,
    OnInit,
    OnDestroy,
    trigger,
    state,
    style,
    transition,
    animate }       from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import {ProductsService }           from './products.service';

import { ProductType } from './product-type';
import { Catgory } from './catgory';


@Component({
    selector: 'product-types-page',
    templateUrl: './product-types-page.component.html',
    animations: [
        trigger('flyInOut', [
            state('in', style({ transform: 'translateX(0)' })),
            transition('void => *', [
                style({ transform: 'translateX(-100%)' }),
                animate(500)
            ]),
            transition('* => void', [
                animate(500, style({ transform: 'translateX(100%)' }))
            ])
        ])
    ]

})
export class ProductTypesPageComponent implements OnInit, OnDestroy {

    flyInOut: any;
    id: number;
    productTypes: ProductType[];
    private sub: any;
    catDescription: string;
    catHeader: string;

    constructor(private productsService: ProductsService, private activatedRoute: ActivatedRoute) {

    }

    ngOnInit() {

        this.flyInOut = "in";

        this.sub = this.activatedRoute.params.subscribe(params => {
            this.id = +params['id']; // (+) converts string 'id' to a number

            this.productsService.getCatgoriesById(this.id).subscribe((response: Catgory) => {
              this.catDescription = response.description;
              this.catHeader = response.name;
          
            })

            this.productsService.getProductsTypesByCategoryId(this.id)
                .subscribe((response: ProductType[]) => {
                    this.productTypes = response;
                }, error => {

                })
        });

    }

    ngOnDestroy() {
        this.flyInOut = "in";
        this.sub.unsubscribe();
    }
}