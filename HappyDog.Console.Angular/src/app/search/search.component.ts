import { Component, OnInit, Input } from '@angular/core';
import { ArticleServiceService } from '../services/article-service.service';
import { Article } from '../models/article';
import { BaseStatus } from '../models/base-status';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

    constructor(
        private articleService: ArticleServiceService,
        private router: Router,
        private activatedRouter: ActivatedRoute
    ) {
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }

    protected q: string;
    protected articles: Article[];

    ngOnInit() {
        const q = this.activatedRouter.snapshot.queryParamMap.get("q");
        if (q) {
            this.q = q;
            this.articleService
                .search(q)
                .subscribe(r => {
                    this.articles = r.data.data;
                    this.articles[0].status = BaseStatus.Disabled;
                });
        }
    }

    search() {
        if (this.q) {
            this.router.navigate(['search'], { queryParams: { q: this.q } });
        }
    }
}
