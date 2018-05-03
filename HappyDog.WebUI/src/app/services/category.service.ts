import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable()
export class CategoryService {

  constructor(private client: HttpClient) { }

  private url = `${environment.server}/api/category`;

  getCategories(): Observable<Category[]> {
    return this.client.get<Category[]>(this.url);
  }
}
