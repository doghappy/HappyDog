import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../../articleBase.component';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-db',
  templateUrl: './db.component.html',
  styleUrls: ['./db.component.css']
})
export class DbComponent extends ArticleBaseComponent {

  protected categoryId?: number = 2;

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}
