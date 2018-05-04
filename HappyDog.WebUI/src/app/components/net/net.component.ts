import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../articleBase.component';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-net',
  templateUrl: './net.component.html',
  styleUrls: ['./net.component.css']
})
export class NetComponent extends ArticleBaseComponent {

  protected categoryId?: number = 1;

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}
