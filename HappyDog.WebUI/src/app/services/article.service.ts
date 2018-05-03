import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs/Observable';
import { ArticleSummary } from '../models/articleSummary';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Pagination } from '../models/pagination';

@Injectable()
export class ArticleService {

  constructor(private client: HttpClient) { }

  private url = `${environment.server}/api/article`;

  getPageArticles(cid?: number, page?: number): Observable<Pagination<ArticleSummary>> {
    return this.client.get<Pagination<ArticleSummary>>({this.url});
  }
}
