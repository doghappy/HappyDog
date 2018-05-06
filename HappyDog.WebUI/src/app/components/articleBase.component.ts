import { ArticleSummary } from "../models/articleSummary";
import { Pagination } from "../models/pagination";
import { ArticleService } from "../services/article.service";
import { OnInit } from '@angular/core';

export abstract class ArticleBaseComponent implements OnInit {

  constructor(articleService: ArticleService) {
    this.articleService = articleService;
  }

  protected abstract categoryId?: number;

  private _pageNumber: number;
  get pageNumber(): number {
    return this._pageNumber;
  }
  set pageNumber(value: number) {
    this._pageNumber = value;
    this.getPageArticles();
  }

  public pageArticles: Pagination<ArticleSummary>;

  protected articleService: ArticleService;

  ngOnInit(): void {
    this.pageNumber = 1;
  }

  protected getPageArticles(): void {
    this.articleService.getPageArticles(this.pageNumber, this.categoryId)
      .subscribe(d => this.pageArticles = d);
  }
}
