import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ArticleServiceService {

    constructor(
        private http: HttpClient
    ) { }

    search(q: string) {
        //return this.http.get('https://')
    }
}
