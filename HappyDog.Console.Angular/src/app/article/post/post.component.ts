import { Component, OnInit } from '@angular/core';
import { BaseStatus } from '../../models/base-status';
import { ArticleService } from '../../services/article.service';
import { PostArticleDto } from '../../models/article/post-article-dto';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { ToastrService } from 'ngx-toastr';
import { TagService } from 'src/app/services/tag.service';
import { Tag } from 'src/app/models/tag/tag';
import { Router } from '@angular/router';

@Component({
    selector: 'app-post',
    templateUrl: './post.component.html',
    styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

    constructor(
        private articleService: ArticleService,
        private errorHandler: ErrorHandlerService,
        private toastr: ToastrService,
        private router: Router
    ) {
    }

    public article: PostArticleDto;

    public isActive: boolean;

    ngOnInit() {
        this.article = new PostArticleDto();
        this.article.status = BaseStatus.Disabled;
        this.article.categoryId = 1;
        this.article.tagIds = [];
    }

    post(): void {
        this.isActive = true;
        this.articleService.post(this.article).subscribe(result => {
            this.isActive = false;
            this.toastr.success("提交成功");
            this.router.navigate(['edit', result.id]);
        }, err => {
            this.isActive = false;
            this.errorHandler.handleError(err);
        });
    }
}
