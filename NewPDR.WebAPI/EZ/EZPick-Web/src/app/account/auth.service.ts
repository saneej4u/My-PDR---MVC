import { Injectable, EventEmitter } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/delay';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { RegisterBindingModel } from  './registerbindingmodel';

import { LoginModel } from './loginModel';

@Injectable()
export class AuthService {

  baseUrl: string = '';
  isLoggedIn: boolean = false;
  redirectUrl: string;

  public token: string;

  HasloogedIn: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private http: Http) {
    // this.baseUrl = "http://san-angular.azurewebsites.net/";
    this.baseUrl = "http://localhost:51012/";

  }


  signUp(model: RegisterBindingModel) {

    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');
    return this.http.post(this.baseUrl + 'api/registeruser', JSON.stringify(model), { headers: headers })
      .map((res: Response) => {
        console.log("STATUS: " + res.status);

        return res.json;
      });
    // .catch(this.handleError);
  }


  login(username: string, password: string): Observable<boolean> {

    var data = "grant_type=password&username=" + username + "&password=" + password;

    let headers = new Headers();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');
    headers.append('Accept', 'application/json');


    return this.http.post(this.baseUrl + 'oauth2/token', data, { headers: headers })
      .map((res: Response) => {
        let token = res.json() && res.json().access_token;
        if (token) {
          this.token = token;
          var data = JSON.stringify({ username: username, token: token });
          localStorage.setItem("currentuser", data);
          this.isLoggedIn = true;
          this.HasloogedIn.emit(true);
          return true;

        }
        else {
          return false;
        }

      }).catch(this.handleError);


  }

  IsLoggedIn(): boolean {

    if (localStorage.getItem('currentuser')) {
      return true;
    }

    return false;
  }

  logout(): void {
    // clear token remove user from local storage to log user out
    this.token = null;
    localStorage.removeItem('currentuser');
  }

  private handleError(error: any) {
    var applicationError = error.headers.get('Application-Error');
    var serverError = error.json();
    var modelStateErrors: string = '';

    if (!serverError.type) {
      console.log("serverError: san: " + serverError);
      for (var key in serverError) {
        if (serverError[key])
          modelStateErrors += serverError[key] + '\n';
      }
    }

    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;

    console.log("modelStateErrors: san: " + modelStateErrors);

    return Observable.throw(applicationError || modelStateErrors || 'Server error');
  }


}
