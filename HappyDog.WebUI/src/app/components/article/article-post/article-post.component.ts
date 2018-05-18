import { Component, OnInit } from '@angular/core';
import { Article } from '../../../models/article';
import { Category } from '../../../models/category';
import { Configuration } from '../../../data/configuration';
import { BaseState } from '../../../enums/baseState';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-article-post',
  templateUrl: './article-post.component.html',
  styleUrls: ['./article-post.component.css']
})
export class ArticlePostComponent implements OnInit {

  constructor(private articleService: ArticleService) {
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
      .subscribe(r => location.href = `/detail/${r.data}`)
  }
}
