import { ArticleSummary } from "../models/articleSummary";
import { Pagination } from "../models/pagination";
import { ArticleService } from "../services/article.service";
import { OnInit } from '@angular/core';

export abstract class ArticleBaseComponent implements OnInit {

  constructor(articleService: ArticleService) {
    this.articleService = articleService;
  }

  protected abstract categoryId?: number;

  protected pageNumber: number = 1;

  public pageArticles: Pagination<ArticleSummary>;

  protected articleService: ArticleService;

  ngOnInit(): void {
    this.getPageArticles();
  }

  protected getPageArticles(): void {
    this.articleService.getPageArticles(this.pageNumber, this.categoryId)
      .subscribe(d => this.pageArticles = d);
  }
}
