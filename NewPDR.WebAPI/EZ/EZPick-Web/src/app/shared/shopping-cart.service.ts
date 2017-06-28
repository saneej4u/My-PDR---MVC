import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Subject} from  'rxjs';


import { ShoppingBasket } from './shopping-basket';
import { Address }  from '../checkout/address';




@Injectable()
export class ShoppingCartService {

    baseUrl: string = '';
    shoppingCart: ShoppingBasket[] = [];
    productAddedToBasket: Subject<number> = new Subject<number>();
    itemsAddedToBasket: EventEmitter<ShoppingBasket[]> = new EventEmitter<ShoppingBasket[]>();
    orderPlacedEvent: EventEmitter<ShoppingBasket[]> = new EventEmitter<ShoppingBasket[]>();
    totalPriceToPay: number = 0;

    constructor(private http: Http) {
        // this.baseUrl = "http://san-angular.azurewebsites.net/";
        this.baseUrl = "http://localhost:51012/";
        this.getAddedShoppingCartItems();

    }

    getAddedShoppingCartItems(): ShoppingBasket[] {
        var items = localStorage.getItem("shoppingCart");

        if (typeof items !== 'undefined' && items !== null) {
            this.shoppingCart = JSON.parse(items);
        }
        else {
            this.shoppingCart = [];
        }
        return this.shoppingCart;

    }

    addToCart(product, qty: number) {
        var addedToExistingItem = false;

        for (var i = 0; i < this.shoppingCart.length; i++) {
            if (this.shoppingCart[i].name == product.name) {
                this.shoppingCart[i].quantity = (this.shoppingCart[i].quantity * 1) + (1 * qty);
                addedToExistingItem = true;
                break;
            }
        }
        if (!addedToExistingItem) {
            this.shoppingCart.push({
                quantity: qty,
                price: product.unitPrice,
                name: product.name,
                productID: product.productID,
                imageUrl: product.productImages.length > 0 ? product.productImages[0].imageUrl : "",
                count: 1
            });
        }

        this.AddToLocalStorage(this.shoppingCart);

        this.productAddedToBasket.next(this.shoppingCart.length)
        this.itemsAddedToBasket.emit(this.shoppingCart);
    }

    removeProduct(product) {
        for (var i = 0; i < this.shoppingCart.length; i++) {
            if (this.shoppingCart[i].name == product.name) {
                this.shoppingCart.splice(i, 1);
                break;
            }
        }
        this.AddToLocalStorage(this.shoppingCart);

        this.productAddedToBasket.next(this.shoppingCart.length)
        this.itemsAddedToBasket.emit(this.shoppingCart);
    }

    minusQty(product) {
        for (var i = 0; i < this.shoppingCart.length; i++) {
            if (this.shoppingCart[i].name == product.name) {
                this.shoppingCart[i].quantity = (this.shoppingCart[i].quantity * 1) - (1 * 1);
                break;
            }
        }
        this.AddToLocalStorage(this.shoppingCart);

        this.productAddedToBasket.next(this.shoppingCart.length)
        this.itemsAddedToBasket.emit(this.shoppingCart);
    }

    plusQty(product) {
        for (var i = 0; i < this.shoppingCart.length; i++) {
            if (this.shoppingCart[i].name == product.name) {
                this.shoppingCart[i].quantity = (this.shoppingCart[i].quantity * 1) + (1 * 1);
                break;
            }
        }
        this.AddToLocalStorage(this.shoppingCart);

        this.productAddedToBasket.next(this.shoppingCart.length)
        this.itemsAddedToBasket.emit(this.shoppingCart);
    }


    GetTotalPrice(shoppingBag: ShoppingBasket[]): number {


        for (var i = 0; i < shoppingBag.length; i++) {

            this.totalPriceToPay = (this.totalPriceToPay * 1) + (shoppingBag[i].quantity * shoppingBag[i].price);
            console.log("SB: " + this.totalPriceToPay);
        }
        console.log(this.totalPriceToPay);

        return this.totalPriceToPay;

    }

    private AddToLocalStorage(shoppingCart: ShoppingBasket[]) {
        var data = JSON.stringify(shoppingCart);
        localStorage.setItem("shoppingCart", data);
    }

    InsertDeliveryaddress(model: Address) {

        let authToken = localStorage.getItem('currentuser');
        let authTokenJson = JSON.parse(authToken);
        let tokenId = authTokenJson.token;
        let token = "Bearer " + tokenId;

        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + tokenId);
        headers.append('Content-Type', 'application/json ');

        model.addresstypeid = 2;

        return this.http.post(this.baseUrl + 'api/address', JSON.stringify(model), { headers: headers })
            .map((res: Response) => {
                return res.json();
            }).catch(this.handleError);
    }

    InsertInvoiceaddress(model: Address) {

        let authToken = localStorage.getItem('currentuser');
        let authTokenJson = JSON.parse(authToken);
        let tokenId = authTokenJson.token;
        let token = "Bearer " + tokenId;

        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + tokenId);
        headers.append('Content-Type', 'application/json ');

        model.addresstypeid = 1;

        return this.http.post(this.baseUrl + 'api/invoiceaddress', JSON.stringify(model), { headers: headers })
            .map((res: Response) => {
                return res.json();
            }).catch(this.handleError);
    }

    PlaceAnOrder() {

        let authToken = localStorage.getItem('currentuser');
        let authTokenJson = JSON.parse(authToken);
        let tokenId = authTokenJson.token;
        let token = "Bearer " + tokenId;

        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + tokenId);
        headers.append('Content-Type', 'application/json ');



        return this.http.post(this.baseUrl + 'api/order', JSON.stringify(this.shoppingCart), { headers: headers })
            .map((res: Response) => {
                localStorage.removeItem('shoppingCart');
                this.orderPlacedEvent.emit(this.shoppingCart);
                return res.json();
            }).catch(this.handleError);
    }

    UpdateUserDeliveryaddress(model: Address) {

        let authToken = localStorage.getItem('currentuser');
        let authTokenJson = JSON.parse(authToken);
        let tokenId = authTokenJson.token;
        let token = "Bearer " + tokenId;

        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + tokenId);
        headers.append('Content-Type', 'application/json ');

        return this.http.put(this.baseUrl + 'api/deliveryaddress', JSON.stringify(model), { headers: headers })
            .map((res: Response) => {
                return res.json();
            }).catch(this.handleError);
    }

    GetAddress(): Observable<Address> {
        let authToken = localStorage.getItem('currentuser');
        let authTokenJson = JSON.parse(authToken);
        let tokenId = authTokenJson.token;
        let token = "Bearer " + tokenId;

        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + tokenId);
        headers.append('Content-Type', 'application/json ');

        return this.http.get(this.baseUrl + 'api/address/selecteddelivery', { headers: headers })
            .map((res: Response) => {
                console.log(res);

                return res.json();
            }).catch(this.handleError);
    }



    getDeliveryAddress(): Observable<Address[]> {

        let authToken = localStorage.getItem('currentuser');
        let authTokenJson = JSON.parse(authToken);
        let tokenId = authTokenJson.token;

        let token = "Bearer " + tokenId;


        // let headers = new Headers({ 'Accept': 'application/json' });
        let headers = new Headers();
        headers.append('Authorization', 'Bearer ' + tokenId);

        return this.http.get(this.baseUrl + 'api/address/deliveryaddress', { headers: headers })
            .map((res: Response) => {
                return res.json();
            }).catch(this.handleError);

    }


    private handleError(error: any) {
        var applicationError = error.headers.get('Application-Error');
        var serverError = error.json();
        var modelStateErrors: string = '';

        if (!serverError.type) {
            console.log(serverError);
            for (var key in serverError) {
                if (serverError[key])
                    modelStateErrors += serverError[key] + '\n';
            }
        }

        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;

        return Observable.throw(applicationError || modelStateErrors || 'Server error');
    }



}