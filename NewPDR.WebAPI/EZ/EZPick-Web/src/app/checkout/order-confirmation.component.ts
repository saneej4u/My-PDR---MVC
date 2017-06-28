import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';

import { ShoppingCartService }           from '../shared/shopping-cart.service';


@Component({
    selector: 'order-confirmation',
    templateUrl: './order-confirmation.component.html'
})
export class OrderConfirmationComponent implements OnInit {


    shoppingcount: number;
    items: any;
    subscription: any;

    delCount: number;
    errors: string;


    constructor(private shoppingCartService: ShoppingCartService,
        private router: Router) { }


    ngOnInit() {




    }

    changeDel() {

    }



}