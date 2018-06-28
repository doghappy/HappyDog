import { Component, OnInit } from '@angular/core';
import { Article } from '../../../models/article';
import { BaseState } from '../../../enums/baseState';
import { Category } from '../../../models/category';
import { Configuration } from '../../../data/configuration';
import { ArticleService } from '../../../services/article.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-article-edit',
  templateUrl: './article-edit.component.html',
  styleUrls: ['./article-edit.component.css']
})
export class ArticleEditComponent implements OnInit {

  constructor(
    private articleService: ArticleService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.categories = Configuration.categories;
  }

  private id: number;

  public article: Article;
  public categories: Category[];

  ngOnInit() {
    this.route.params.subscribe(p => this.id = p.id);
    this.articleService.getArticle(this.id).subscribe(d => this.article = d);
  }

  public update(): void {
    this.articleService.update(this.article)
      .subscribe(r => this.router.navigateByUrl(`/detail/${this.article.id}`))
  }
}
