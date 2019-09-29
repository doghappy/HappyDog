import { Component, OnInit } from '@angular/core';
import { BaseStatus } from '../models/base-status';
import { ArticleService } from '../services/article.service';
import { PostArticleDto } from '../models/article/post-article-dto';
import { Router } from '@angular/router';

@Component({
    selector: 'app-post',
    templateUrl: './post.component.html',
    styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

    constructor(
        private articleService: ArticleService,
        private router: Router
    ) { }

    protected article: PostArticleDto;

    ngOnInit() {
        this.article = new PostArticleDto();
        this.article.status = BaseStatus.Disabled;
        this.article.categoryId = 1;
    }

    post(): void {
        this.articleService.post(this.article).subscribe(result => {
            this.router.navigate(['/']);
        });
    }
}
