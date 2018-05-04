import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

  constructor(private route: ActivatedRoute, private articleService: ArticleService) { }

  id: number;

  ngOnInit() {
    this.route.params.subscribe(p => this.id = p.id)
    //this.articleService
  }

}
