import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { environment } from '../../environments/environment';
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
    const data = {
      UserName: userName,
      Password: password,
      RememberMe: rememberMe
    };
    return this.client.post<HttpBaseResult>(url, data, this.reqOptions);
  }
}
