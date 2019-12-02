import { Component, OnInit } from '@angular/core';
import { BaseStatus } from '../../models/base-status';
import { ArticleService } from '../../services/article.service';
import { PostArticleDto } from '../../models/article/post-article-dto';
import { Router } from '@angular/router';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { ArticleOperationComponent } from '../article-operation.component';
import { ToastrService } from 'ngx-toastr';


@Component({
    selector: 'app-post',
    templateUrl: './post.component.html',
    styleUrls: ['./post.component.css']
})
export class PostComponent extends ArticleOperationComponent implements OnInit {

    constructor(
        private articleService: ArticleService,
        private router: Router,
        private errorHandler: ErrorHandlerService,
        private toastr: ToastrService
    ) {
        super();
    }

    public article: PostArticleDto;

    public isActive: boolean;

    ngOnInit() {
        this.article = new PostArticleDto();
        this.article.status = BaseStatus.Disabled;
        this.article.categoryId = 1;
        this.article.tagNames = [];
    }

    post(): void {
        this.isActive = true;
        this.article.tagNames = this.getTagNames();
        this.articleService.post(this.article).subscribe(result => {
            this.isActive = false;
            this.toastr.success("提交成功");
        }, err => {
            this.isActive = false;
            this.errorHandler.handleError(err);
        });
    }
}
