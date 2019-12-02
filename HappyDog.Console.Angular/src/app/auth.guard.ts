import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanActivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ArticleService } from './services/article.service';
import { UserService } from './services/user.service';
import { ErrorHandlerService } from './services/error-handler.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private articleService: ArticleService,
        private userService: UserService,
        private errorHandler: ErrorHandlerService
    ) { }

    public isSignIn: boolean;

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this.articleService.getHiddenArticles().pipe(map(result => {
            this.userService.isAuth = true;
            return true;
        }), catchError(err => {
            this.errorHandler.handleError(err);
            this.userService.isAuth = false;
            this.router.navigate(['signin']);
            return of(false);
        }))
    }
}
