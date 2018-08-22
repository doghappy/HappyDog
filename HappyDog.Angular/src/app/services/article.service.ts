import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ArticleSummary } from '../models/articleSummary';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Pagination } from '../models/pagination';
import { Article } from '../models/article';
import { BaseService } from './base.service';
import { HttpBaseResult } from '../models/results/httpBaseResult';
import { HttpDataResult } from '../models/results/httpDataResult';
import { Category } from '../enums/category';

@Injectable()
export class ArticleService extends BaseService {

  constructor(private client: HttpClient) {
    super();
  }

  private url = `${this.server}/article`;

  getPageArticles(page: number, categoryValue: string): Observable<Pagination<ArticleSummary>> {
    let params = new HttpParams();
    let url = this.url;
    if (page > 1) {
      params = params.set("page", page.toString());
    }
    if (categoryValue) {
      url += `/${categoryValue}`;
    }
    return this.client.get<Pagination<ArticleSummary>>(url, { params, withCredentials: true });
  }

  getArticle(id: number): Observable<Article> {
    return this.client.get<Article>(`${this.url}/${id}`, { withCredentials: true });
  }

  update(article: Article): Observable<HttpBaseResult> {
    return this.client.put<HttpBaseResult>(`${this.url}/${article.id}`, article, { withCredentials: true })
  }

  post(article: Article): Observable<HttpDataResult<number>> {
    return this.client.post<HttpDataResult<number>>(this.url, article, { withCredentials: true });
  }
}
