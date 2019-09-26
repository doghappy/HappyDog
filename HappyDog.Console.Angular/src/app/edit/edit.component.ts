import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleServiceService } from '../services/article-service.service';
import { Article } from '../models/article';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

    constructor(
        private router: ActivatedRoute,
        private articleService: ArticleServiceService
    ) { }

    protected article: Article;

    ngOnInit() {
        const id = Number(this.router.snapshot.paramMap.get("id"));
        this.articleService
            .getEnabledArticle(id)
            .subscribe(r => {
                this.article = r;
                console.log(this.article);
            });
    }

}
