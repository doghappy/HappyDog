import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../../services/article.service';
import { Pagination } from '../../models/pagination';
import { ArticleSummary } from '../../models/articleSummary';
import { ArticleBaseComponent } from '../articleBase.component';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent extends ArticleBaseComponent implements OnInit {

  protected categoryId: number;

  //constructor(private articleService: ArticleService) {
  //  super(articleService);
  //}

  pageArticles: Pagination<ArticleSummary>;

  ngOnInit() {
    this.getPageArticles();
  }
}
