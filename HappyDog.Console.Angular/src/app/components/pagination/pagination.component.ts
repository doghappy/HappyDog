import { Component, OnInit, Input } from '@angular/core';
import { Pagination } from '../../models/pagination';
import { IPaginationQueryBuilder } from './i-pagination-query-builder';

@Component({
    selector: 'app-pagination',
    templateUrl: './pagination.component.html',
    styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit {

    constructor() {
    }

    @Input()
    pagination: Pagination<any>;

    @Input()
    link: string;

    @Input()
    paginationQueryBuilder: IPaginationQueryBuilder;

    arr: number[];

    ngOnInit() {
        this.arr = [];
        for (var i = 1; i <= this.pagination.totalPages; i++) {
            this.arr.push(i);
        }
    }
}
