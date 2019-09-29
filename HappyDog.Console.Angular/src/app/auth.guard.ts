import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanActivate } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ArticleService } from './services/article.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private articleService: ArticleService
    ) { }

    public isSignIn: boolean;

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this.articleService.getHiddenArticles().pipe(map(result => {
            return true;
        }), catchError(() => {
            this.router.navigate(['signin']);
            return of(false);
        }))
    }
}
