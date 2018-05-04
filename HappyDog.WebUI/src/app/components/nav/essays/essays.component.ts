import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../../articleBase.component';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-essays',
  templateUrl: './essays.component.html',
  styleUrls: ['./essays.component.css']
})
export class EssaysComponent extends ArticleBaseComponent {

  protected categoryId?: number = 5;

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}
