import { Component, OnInit} from '@angular/core';
import { FormGroup,
    FormBuilder,
    Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { ShoppingBasket }  from '../shared/shopping-basket';

import { ShoppingCartService }           from '../shared/shopping-cart.service';
import { Address }  from './address';


@Component({
    selector: 'order-pay',
    templateUrl: './order-pay.component.html',
    styleUrls: ["./order-pay.component.css"]
})
export class OrderPayComponent implements OnInit {


    shoppingcount: number;
    items: any;
    subscription: any;
    addedItems: ShoppingBasket[];
    totalPrice: number;
    deliveryAddress: Address[];
    selectedDelivery: Address;
    invoiceAddress: Address;
    delCount: number;
    errors: string;
    invoiceAddressForm: FormGroup;

    constructor(private shoppingCartService: ShoppingCartService, private router: Router, private formBuilder: FormBuilder) { }


    ngOnInit() {


        this.invoiceAddressForm = this.formBuilder.group({
            addressLine1: ['', Validators.required],
            addressLine2: ['', Validators.required],
            street: ['', Validators.required],
            postcode: ['', Validators.required],
            country: ['', Validators.required]
        });

        this.addedItems = this.shoppingCartService.getAddedShoppingCartItems();
        this.totalPrice = this.shoppingCartService.GetTotalPrice(this.addedItems);

        this.subscription = this.shoppingCartService.productAddedToBasket.subscribe((value) => {
            this.shoppingcount = value;
            this.totalPrice = this.shoppingCartService.GetTotalPrice(this.addedItems);

        });

        this.shoppingCartService.itemsAddedToBasket
            .subscribe((items) => {
                this.addedItems = items;
            }
            );

        this.shoppingCartService.getDeliveryAddress().subscribe((result) => {

            this.deliveryAddress = result;
            this.delCount = result.length;
        });

        this.shoppingCartService.GetAddress().subscribe((result) => {
            this.selectedDelivery = result;
        });
    }

    changeDel() {

    }

    onSubmitAddInvoice(model: Address) {
        this.shoppingCartService.InsertInvoiceaddress(model).subscribe((result: any) => {
            this.shoppingCartService.PlaceAnOrder().subscribe((result: any) => {
                this.router.navigate(['order-confirmation']);

            }, () => {

                console.log("Error -  Place order");

            })

        }, (error) => {
            this.errors = error;
        })

    }

}