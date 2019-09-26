import { Component, OnInit } from '@angular/core';
import { Article } from '../models/article';
import { BaseStatus } from '../models/base-status';

@Component({
    selector: 'app-post',
    templateUrl: './post.component.html',
    styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

    constructor() { }

    protected article: Article;

    ngOnInit() {
        this.article = new Article();
        this.article.status = BaseStatus.Disabled;
        this.article.categoryId = 1;
    }

}
