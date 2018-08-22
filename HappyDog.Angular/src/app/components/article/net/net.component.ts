import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../articleBase.component';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-article-net',
  templateUrl: './net.component.html',
  styleUrls: ['./net.component.css']
})
export class NetComponent extends ArticleBaseComponent {

  protected categoryValue = "net";

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}