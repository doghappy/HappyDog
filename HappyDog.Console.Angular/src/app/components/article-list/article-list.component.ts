import { Component, OnInit, Input } from '@angular/core';
import { Article } from '../../models/article';
import { BaseStatus } from '../../models/base-status';

@Component({
    selector: 'app-article-list',
    templateUrl: './article-list.component.html',
    styleUrls: ['./article-list.component.css']
})
export class ArticleListComponent implements OnInit {

    constructor() { }

    @Input()
    articles: Article[];

    public _baseStatus = BaseStatus;

    ngOnInit() {

    }

}
