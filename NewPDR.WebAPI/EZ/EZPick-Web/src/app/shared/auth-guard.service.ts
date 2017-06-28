import { Injectable }           from '@angular/core';
import {
    CanActivate, Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
}                              from '@angular/router';

import { AuthService }      from '../account/auth.service';



@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        let url: string = state.url;

        return this.checkLogin(url);
    }

    checkLogin(url: string) {

        if (localStorage.getItem('currentuser')) {
            return true;
        }
      
        this.authService.redirectUrl = url;
        this.router.navigate(['login']);
        return false;
    }
}