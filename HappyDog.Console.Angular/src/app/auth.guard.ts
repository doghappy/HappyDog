import { Injectable } from '@angular/core';
import { CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivateChild {

    constructor(
        private router: Router
    ) { }

    public isSignIn: boolean;

    canActivateChild(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        //const isSignIn = this.cookieService.check(".AspNetCore.Identity.Application");
        //console.log("isSignIn", isSignIn);
        if (this.isSignIn) {
            return true;
        } else {
            this.router.navigate(['signin']);
            return false;
        }
    }
}
