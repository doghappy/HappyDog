import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment'
import { Observable } from 'rxjs';
import { HttpDataResult } from '../models/http-data-result';
import { Pagination } from '../models/pagination';
import { Article } from '../models/article';

@Injectable({
    providedIn: 'root'
})
export class ArticleServiceService {

    constructor(
        private http: HttpClient
    ) { }

    search(q: string): Observable<HttpDataResult<Pagination<Article>>> {
        return this.http.get<HttpDataResult<Pagination<Article>>>(`${environment.commonApiBaseAddress}/api/article/search?q=${q}`);
    }

    getEnabledArticle(id: number): Observable<Article> {
        return this.http.get<Article>(`${environment.commonApiBaseAddress}/api/article/${id}`);
    }
}
