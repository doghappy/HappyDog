import { Component, OnInit, Input } from '@angular/core';
import { ArticleServiceService } from '../services/article-service.service';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    constructor(
        private articleService: ArticleServiceService
    ) { }

    @Input()
    public q: string;

    ngOnInit() {
    }

    search() {
        if (this.q) {
            this.articleService
                .search(this.q)
                .subscribe(r => {
                    console.log(r);
                });
        }
    }
}
