import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

    constructor(
        private router: ActivatedRoute,
        private articleService: ArticleService
    ) { }

    protected article: Article;

    ngOnInit() {
        const id = Number(this.router.snapshot.paramMap.get("id"));
        this.articleService
            .getArticle(id)
            .subscribe(r => {
                this.article = r;
            });
    }

}
