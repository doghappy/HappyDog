import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ArticleService } from '../../../services/article.service';
import { Article } from '../../../models/article';
import { Category } from '../../../models/category';
import { Configuration } from '../../../data/configuration';
import { AuthenticationService } from '../../../services/authentication.service';

@Component({
  selector: 'app-article-detail',
  templateUrl: './detail.component.html',
  styles: ['@import "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/2.10.0/github-markdown.min.css";'],
  styleUrls: [
    //'./detail.component.css'
  ]
})
export class DetailComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private articleService: ArticleService,
    authService: AuthenticationService
  ) {
    this.categories = Configuration.categories;
    this.hasAuthCookie = authService.hasAuthCookie;
  }

  private id: number;

  article: Article;
  categories: Category[];
  hasAuthCookie: boolean;
  loading: boolean = false;

  ngOnInit() {
    this.route.params.subscribe(p => this.id = p.id);
    this.loading = true;
    this.articleService.getArticle(this.id).subscribe(d => {
      this.article = d;
      this.loading = false;
    });
  }

  update(): void {
    this.articleService.update(this.article)
      .subscribe(a => location.reload())
  }
}
