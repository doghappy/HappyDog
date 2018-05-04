import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styles: ['@import "https://cdnjs.cloudflare.com/ajax/libs/github-markdown-css/2.10.0/github-markdown.min.css";'],
  styleUrls: [
    //'./detail.component.css'
  ]
})
export class DetailComponent implements OnInit {

  constructor(private route: ActivatedRoute, private articleService: ArticleService) { }

  private id: number;

  article: Article;

  ngOnInit() {
    this.route.params.subscribe(p => this.id = p.id)
    this.articleService.getArticle(this.id).subscribe(d => this.article = d);
  }
}
