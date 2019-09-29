import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArticleService } from '../services/article.service';
import { Article } from '../models/article/article';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

    constructor(
        private activatedRoute: ActivatedRoute,
        private articleService: ArticleService,
        private router: Router
    ) { }

    protected article: Article;
    private id: number;

    ngOnInit() {
        this.id = Number(this.activatedRoute.snapshot.paramMap.get("id"));
        this.articleService
            .getArticle(this.id)
            .subscribe(r => {
                this.article = r;
            });
    }

    put(): void {
        this.articleService.put(this.id, {
            title: this.article.title,
            content: this.article.content,
            status: this.article.status,
            categoryId: Number(this.article.categoryId)
        }).subscribe(result => {
            this.router.navigate(['/']);
        });
    }
}
