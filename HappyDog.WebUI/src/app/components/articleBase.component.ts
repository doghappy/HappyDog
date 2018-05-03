import { ArticleSummary } from "../models/articleSummary";
import { Pagination } from "../models/pagination";
import { ArticleService } from "../services/article.service";

export abstract class ArticleBaseComponent {

  constructor(protected articleService: ArticleService) { }

  protected abstract categoryId: number;

  protected pageArticles: Pagination<ArticleSummary>;

  protected getPageArticles(): void {
    this.articleService.getPageArticles()
      .subscribe(d => this.pageArticles = d);
  }

}
