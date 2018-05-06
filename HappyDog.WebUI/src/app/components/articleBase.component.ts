import { ArticleSummary } from "../models/articleSummary";
import { Pagination } from "../models/pagination";
import { ArticleService } from "../services/article.service";
import { OnInit } from '@angular/core';
//import { Router } from "@angular/router";

export abstract class ArticleBaseComponent implements OnInit {

  constructor(protected articleService: ArticleService) {
    this.target = '#paginationTarget';
  }

  protected abstract categoryId?: number;
  protected target: string;

  private _pageNumber: number;
  get pageNumber(): number {
    return this._pageNumber;
  }
  set pageNumber(value: number) {
    this._pageNumber = value;
    this.getPageArticles();
  }

  public pageArticles: Pagination<ArticleSummary>;

  ngOnInit(): void {
    this.pageNumber = 1;
  }

  protected getPageArticles(): void {
    this.articleService.getPageArticles(this.pageNumber, this.categoryId)
      .subscribe(d => this.pageArticles = d);
  }

  public pageChanged({ page, itemsPerPage }): void {
    if (this.target && this.target.length > 0) {
      try {
        document.querySelector(this.target).scrollIntoView();
      } catch (e) { }
    }
  }
}
