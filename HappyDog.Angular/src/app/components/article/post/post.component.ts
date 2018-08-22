import { Component, OnInit } from '@angular/core';
import { Article } from '../../../models/article';
import { Category } from '../../../models/category';
import { Configuration } from '../../../data/configuration';
import { BaseState } from '../../../enums/baseState';
import { ArticleService } from '../../../services/article.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-article-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class ArticlePostComponent implements OnInit {

  constructor(private articleService: ArticleService, private router: Router) {
    this.article = new Article();
    this.categories = Configuration.categories;
    this.article.state = BaseState.disable;
    this.article.categoryId = 1;
  }

  public article: Article;
  public categories: Category[];

  ngOnInit() {
  }

  public post(): void {
    this.articleService.post(this.article)
      .subscribe(r => this.router.navigateByUrl(`/detail/${r.data}`))
  }
}
