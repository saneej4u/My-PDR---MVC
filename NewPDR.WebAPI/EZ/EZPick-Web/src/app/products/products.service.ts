import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import {Observer} from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { Product } from './product';
import { ProductType } from './product-type';
import { Catgory }          from './catgory';

@Injectable()
export class ProductsService {

    baseUrl: string = '';

    constructor(private http: Http) {
        // this.baseUrl = "http://san-angular.azurewebsites.net/";
        this.baseUrl = "http://localhost:51012/";

    }

    getCatgories(): Observable<Catgory[]> {
        return this.http.get(this.baseUrl + 'api/catgories')
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);

    }

     getCatgoriesById(catid:number): Observable<Catgory> {
        return this.http.get(this.baseUrl + 'api/catgories/' + catid)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);

    }

   getProductTypeById(productTypeId:number): Observable<ProductType> {
        return this.http.get(this.baseUrl + 'api/producttypes/' + productTypeId)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);

    }


    getProducts(productTypeId:number): Observable<Product[]> {
        return this.http.get(this.baseUrl + 'api/producttypes/' + productTypeId + '/products')
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);

    }

    getProduct(productId:number): Observable<Product> {
        return this.http.get(this.baseUrl + 'api/products/' + productId)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);

    }


    getProductsTypesByCategoryId(categoryid: number): Observable<ProductType[]> {
        return this.http.get(this.baseUrl + 'api/category/' + categoryid + '/producttypes')
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);

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
