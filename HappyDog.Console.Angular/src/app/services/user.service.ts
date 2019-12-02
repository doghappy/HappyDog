import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { HttpBaseResult } from '../models/http-base-result';
import { environment } from '../../environments/environment'
import { SignInDto } from '../models/user/signin-dto';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(
        private http: HttpClient
    ) { }

    isAuth: boolean;

    public signIn(signInDto: SignInDto): Observable<HttpBaseResult> {
        return this.http.post<HttpBaseResult>(`/api/user/signin`, signInDto, {
            withCredentials: true
        });
    }

    public signOut(): Observable<HttpBaseResult> {
        return this.http.post<HttpBaseResult>(`/api/user/signout`, {}, {
            withCredentials: true
        });
    }
}
