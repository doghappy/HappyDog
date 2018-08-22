import { Component, OnInit } from '@angular/core';
import { ArticleBaseComponent } from '../articleBase.component';
import { ArticleService } from '../../../services/article.service';

@Component({
  selector: 'app-windows',
  templateUrl: './windows.component.html',
  styleUrls: ['./windows.component.css']
})
export class WindowsComponent extends ArticleBaseComponent {

  protected categoryValue = "windows";

  constructor(articleService: ArticleService) {
    super(articleService);
  }
}
