import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { HttpBaseResult } from '../models/http-base-result';
import { environment } from '../../environments/environment'
import { SignInDto } from '../models/user/signin-dto';
import { Article } from '../models/article';
import { AuthGuard } from '../auth.guard';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(
        private http: HttpClient,
        private authGuard: AuthGuard
    ) { }

    public signIn(signInDto: SignInDto): Observable<HttpBaseResult> {
        return this.http.post<HttpBaseResult>(`${environment.consoleApiBaseAddress}/api/user/signin`, signInDto, {
            withCredentials: true
        });
    }

    public checkAuth(): void {
        this.http.get<Article[]>(`${environment.consoleApiBaseAddress}/api/article/disabled`, { withCredentials: true }).subscribe(result => {
            if (result) {
                this.authGuard.isSignIn = true;
            }
        })
    }
}
