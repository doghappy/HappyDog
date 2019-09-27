import { Component, OnInit, Input } from '@angular/core';
import { ArticleServiceService } from '../services/article-service.service';
import { Article } from '../models/article';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpDataResult } from '../models/http-data-result';
import { Pagination } from '../models/pagination';
import { IPaginationQueryBuilder } from '../components/pagination/i-pagination-query-builder';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, IPaginationQueryBuilder {

    constructor(
        private articleService: ArticleServiceService,
        private router: Router,
        private activatedRouter: ActivatedRoute
    ) {
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }


    protected q: string;
    protected data: HttpDataResult<Pagination<Article>>;

    ngOnInit() {
        const q = this.activatedRouter.snapshot.queryParamMap.get("q");
        if (q) {
            const page = Number(this.activatedRouter.snapshot.queryParamMap.get("page") || 1);
            this.q = q;
            this.articleService
                .search(q, page)
                .subscribe(r => {
                    this.data = r;
                });
        }
    }

    protected search(): void {
        if (this.q) {
            this.router.navigate(['search'], { queryParams: { q: this.q } });
        }
    }

    public buildQueryParams(page: number): object {
        return {
            q: this.q,
            page
        };
    }
}
