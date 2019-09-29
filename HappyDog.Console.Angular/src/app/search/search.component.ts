import { Component, OnInit, Input } from '@angular/core';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpDataResult } from '../models/http-data-result';
import { Pagination } from '../models/pagination';
import { IPaginationQueryBuilder } from '../components/pagination/i-pagination-query-builder';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit, IPaginationQueryBuilder {

    constructor(
        private articleService: ArticleService,
        private router: Router,
        private activatedRouter: ActivatedRoute,
        private spinner: NgxSpinnerService
    ) {
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }


    protected q: string;
    protected data: HttpDataResult<Pagination<Article>>;

    ngOnInit() {
        const q = this.activatedRouter.snapshot.queryParamMap.get("q");
        if (q) {
            this.spinner.show();
            const page = Number(this.activatedRouter.snapshot.queryParamMap.get("page") || 1);
            this.q = q;
            this.articleService
                .search(q, page)
                .subscribe(r => {
                    this.data = r;
                    this.spinner.hide();
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
