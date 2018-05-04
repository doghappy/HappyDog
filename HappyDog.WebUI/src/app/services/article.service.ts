import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { ArticleSummary } from '../models/articleSummary';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Pagination } from '../models/pagination';
import { Article } from '../models/article';

@Injectable()
export class ArticleService {

  constructor(private client: HttpClient) { }

  private url = `${environment.server}/api/article`;

  getPageArticles(page: number, cid?: number): Observable<Pagination<ArticleSummary>> {
    let params = new HttpParams();
    if (page > 1) {
      params = params.set("page", page.toString());
    }
    if (cid) {
      params = params.append("cid", cid.toString());
    }
    return this.client.get<Pagination<ArticleSummary>>(this.url, { params });
  }

  getArticle(id: number): Observable<Article> {
    return this.client.get<Article>(`${this.url}/${id}`);
  }
}
