import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article/article';

@Component({
    selector: 'app-hidden',
    templateUrl: './hidden.component.html',
    styleUrls: ['./hidden.component.css']
})
export class HiddenComponent implements OnInit {

    constructor(
        private articleService: ArticleService
    ) { }

    protected articles: Article[];

    ngOnInit() {
        this.articleService.getHiddenArticles().subscribe(result => {
            this.articles = result
        });
    }

}
