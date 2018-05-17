import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie';

@Component({
  selector: 'app-article-edit-button',
  templateUrl: './article-edit-button.component.html',
  styleUrls: ['./article-edit-button.component.css']
})
export class ArticleEditButtonComponent implements OnInit {

  constructor(private cookieService: CookieService) { }

  public hasAuthCookie: boolean;

  ngOnInit() {
    this.hasAuthCookie = this.cookieService.get('.AspNetCore.Cookies') !== undefined;
  }

}
