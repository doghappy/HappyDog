import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpDataResult } from '../models/http-data-result';
import { Pagination } from '../models/pagination';
import { Article } from '../models/article/article';
import { PostArticleDto } from '../models/article/post-article-dto';
import { PutArticleDto } from '../models/article/put-article-dto';

@Injectable({
    providedIn: 'root'
})
export class ArticleService {

    constructor(
        private http: HttpClient
    ) { }

    search(q: string, page: number): Observable<HttpDataResult<Pagination<Article>>> {
        return this.http.get<HttpDataResult<Pagination<Article>>>(`${environment.commonApiBaseAddress}/api/article/search?q=${q}&page=${page}`);
    }

    getArticle(id: number): Observable<Article> {
        return this.http.get<Article>(`/api/article/${id}`, { withCredentials: true });
    }

    getHiddenArticles(): Observable<Article[]> {
        return this.http.get<Article[]>(`/api/article/disabled`, { withCredentials: true });
    }

    post(postDto: PostArticleDto): Observable<Article> {
        return this.http.post<Article>(`/api/article`, postDto, { withCredentials: true });
    }

    put(id: number, putDto: PutArticleDto): Observable<Article> {
        return this.http.put<Article>(`/api/article/${id}`, putDto, { withCredentials: true });
    }
}
