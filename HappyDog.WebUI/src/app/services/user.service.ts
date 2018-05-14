import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http'
import { HttpBaseResult } from '../models/results/httpBaseResult';
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';
import { BaseService } from './base.service';

@Injectable()
export class UserService extends BaseService {

  constructor(private client: HttpClient) {
    super();
  }

  private url: string = `${this.server}/api/user`;

  public login(userName: string, password: string, rememberMe: boolean): Observable<HttpBaseResult> {
    const url: string = `${this.url}/login`;
    var data = {
      UserName: userName,
      Password: password,
      Remember: rememberMe
    };
    return this.client.post<HttpBaseResult>(url, data);
  }
}
