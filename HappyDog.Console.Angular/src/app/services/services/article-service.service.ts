import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment'
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ArticleServiceService {

    constructor(
        private http: HttpClient
    ) { }

    search(q: string): Observable<any> {
        return this.http.get(`${environment.commonApiBaseAddress}/api/article/search?q=${q}`);
    }
}
