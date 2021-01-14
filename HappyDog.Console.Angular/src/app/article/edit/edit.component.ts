import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleService } from '../../services/article.service';
import { Article } from '../../models/article/article';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-edit',
    templateUrl: './edit.component.html',
    styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

    constructor(
        private activatedRoute: ActivatedRoute,
        private articleService: ArticleService,
        private errorHandler: ErrorHandlerService,
        private toastr: ToastrService
    ) {
    }

    public article: Article;
    private id: number;
    public isActive: boolean;
    public tagIds: number[] = [];

    ngOnInit() {
        this.isActive = true;
        this.id = Number(this.activatedRoute.snapshot.paramMap.get("id"));
        this.articleService
            .getArticle(this.id)
            .subscribe(r => {
                this.article = r;
                this.tagIds = r.tags.map(t => t.id);
                this.isActive = false;
            });
    }

    put(): void {
        this.isActive = true;
        this.articleService.put(this.id, {
            title: this.article.title,
            content: this.article.content,
            status: this.article.status,
            categoryId: this.article.categoryId,
            tagIds: this.tagIds
        }).subscribe(result => {
            this.isActive = false;
            this.toastr.success("修改成功");
        }, err => {
            this.isActive = false;
            this.errorHandler.handleError(err);
        });
    }
}
