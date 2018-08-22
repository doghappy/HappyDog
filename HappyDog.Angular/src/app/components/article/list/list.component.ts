import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Pagination } from '../../../models/pagination';
import { ArticleSummary } from '../../../models/articleSummary';

@Component({
  selector: 'app-article-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ArticleListComponent {

  @Input()
  public pageArticles: Pagination<ArticleSummary>;

  @Input()
  public categoryLinkEnable: boolean;
}
