import {    Component,
    OnInit,
    OnDestroy,
    trigger,
    state,
    style,
    transition,
    animate }       from '@angular/core'
import { ProductsService } from './products/products.service';
import { Product }                  from './products/product';


@Component({
    selector: '',
    templateUrl: './home.component.html' ,
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
export class HomeComponent implements  OnInit, OnDestroy {
    products: Product[];
    flyInOut:any;
    constructor(private productsService: ProductsService) { }

    ngOnInit() {

        this.flyInOut = "in";

        this.productsService.getProducts(1)
            .subscribe((response: Product[]) => {
                this.products = response;

            }, error => {

            })

    }

   
    ngOnDestroy()
    {
         this.flyInOut = "out";
    }

    

}