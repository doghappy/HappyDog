import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../articleBase.component';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-article-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent extends ArticleBaseComponent {

  protected categoryValue = "read";

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}
