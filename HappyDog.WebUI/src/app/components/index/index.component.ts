import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { Pagination } from '../../models/pagination';
import { ArticleSummary } from '../../models/articleSummary';
import { ArticleBaseComponent } from '../articleBase.component';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent extends ArticleBaseComponent {

  constructor(
    articleService: ArticleService,
    authService: AuthenticationService
  ) {
    super(articleService);
    this.hasAuthCookie = authService.hasAuthCookie;
  }

  protected categoryId?: number;
  public hasAuthCookie: boolean;
}
