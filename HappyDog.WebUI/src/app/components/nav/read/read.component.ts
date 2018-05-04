import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../../articleBase.component';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent extends ArticleBaseComponent {

  protected categoryId?: number = 4;

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}
