import {  Component,
    EventEmitter,
    Input,
    Output,
    OnInit }                  from '@angular/core';

import { Router } from '@angular/router';

import { ProductType } from './product-type';


@Component({
    selector: 'my-product-type',
    templateUrl: './product-type.component.html',
    styleUrls: ['./products.component.css']
})
export class ProductTypeComponent implements OnInit {

    @Input() productType: ProductType;


    constructor(private router:Router) { }

    ngOnInit() {


    }

    productTypeClicked(pt: ProductType) {
        this.router.navigate(['/products', pt.productTypeID]);
    }
}
