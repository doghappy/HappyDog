import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment'

@Injectable({
    providedIn: 'root'
})
export class ArticleServiceService {

    constructor(
        private http: HttpClient
    ) { }

    search(q: string) {
        return this.http.get(`${environment.commonApiBaseAddress}/api/article/search?q=${q}`);
    }
}
