import { Component, OnInit, Input } from '@angular/core';
import { ArticleServiceService } from '../services/article-service.service';
import { Article } from '../models/article';
import { BaseStatus } from '../models/base-status';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    constructor(
        private articleService: ArticleServiceService
    ) { }

    protected q: string;
    protected articles: Article[];

    ngOnInit() {
    }

    search() {
        if (this.q) {
            this.articleService
                .search(this.q)
                .subscribe(r => {
                    this.articles = r.data.data;
                    this.articles[0].status = BaseStatus.Disabled;
                    console.log(this.articles);
                });
        }
    }
}
