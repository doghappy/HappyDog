import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie';

@Injectable()
export class AuthenticationService {

  constructor(private cookieService: CookieService) {
    this.hasAuthCookie = this.cookieService.get('.AspNetCore.Cookies') !== undefined;
  }

  public hasAuthCookie: boolean;
}
