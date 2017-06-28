import { Component, OnInit, ViewChild, ElementRef, Renderer } from '@angular/core';
import { FormGroup,
  FormBuilder,
  Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { ShoppingBasket }  from '../shared/shopping-basket';

import { ShoppingCartService }           from '../shared/shopping-cart.service';
import { Address }  from './address';

@Component({
  selector: 'review-and-pay',
  templateUrl: './review-pay.component.html'
})
export class ReviewPayComponent implements OnInit {

  shoppingcount: number;
  items: any;
  subscription: any;
  addedItems: ShoppingBasket[];
  totalPrice: number;
  deliveryAddress: Address[];
  selectedDelivery: Address;
  delCount: number;
  deliveryAddressForm: FormGroup;
  reviewPayForm: FormGroup;
  errors: string;
  delErrors: string;
  @ViewChild('reviewandpaydiv') reviewandpaydivEl: ElementRef;
  @ViewChild('changeDeliveryAddress') changeDeliveryAddressEl: ElementRef;
  @ViewChild('addNewLink') addNewLinkEl: ElementRef;



  constructor(private shoppingCartService: ShoppingCartService, private router: Router, private formBuilder: FormBuilder, private domElement: Renderer) { }

  ngOnInit() {

    this.deliveryAddressForm = this.formBuilder.group({
      addressLine1: ['', Validators.required],
      addressLine2: ['', Validators.required],
      street: ['', Validators.required],
      postcode: ['', Validators.required],
      country: ['', Validators.required]
    });


    this.reviewPayForm = this.formBuilder.group({
      groupValue: ['', Validators.required]
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


  onSubmitAddDelivery(model: Address) {
    this.shoppingCartService.InsertDeliveryaddress(model).subscribe((result: any) => {
      this.deliveryAddress.push(result);
      this.deliveryAddressForm.reset();
    }, (error) => {
      this.errors = error;
    })

  }

  onSubmitReviewPayForm(model: string) {

    // console.log(model);

  }

  deliverToThisaddress(address: Address) {
    this.selectedDelivery = address;
    this.domElement.invokeElementMethod(this.reviewandpaydivEl.nativeElement, 'focus');
  }

  changeDel() {
    this.domElement.invokeElementMethod(this.changeDeliveryAddressEl.nativeElement, 'focus');
  }

  addNewDelLink() {
    this.domElement.invokeElementMethod(this.addNewLinkEl.nativeElement, 'focus');
  }

  onClickreviewandpay(selectedDelivery: Address) {

    if (selectedDelivery) {
      this.shoppingCartService.UpdateUserDeliveryaddress(selectedDelivery).subscribe((result: any) => {
        this.router.navigate(['order-pay']);
      }, (error) => {
        this.errors = error;
      })
    }
    else {
      this.delErrors = "Please select delivery address, if you have not one add one.";
      return false;
    }


  }

}