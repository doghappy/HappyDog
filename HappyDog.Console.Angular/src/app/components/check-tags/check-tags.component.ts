import { Component, Input, OnInit } from '@angular/core';
import { Article } from 'src/app/models/article/article';
import { Tag } from 'src/app/models/tag/tag';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-check-tags',
  templateUrl: './check-tags.component.html',
  styleUrls: ['./check-tags.component.css']
})
export class CheckTagsComponent implements OnInit {

  constructor(
    private tagService: TagService
  ) { }

  @Input()
  tagIds: number[];

  tags:Tag[];

  @Input()
  article: Article[];

  ngOnInit(): void {
    this.tagService.getTags().subscribe(x => this.tags = x);
  }

  selectionChanged(event): void {
    const tagId = Number(event.target.value);
    if (event.target.checked) {
      this.tagIds.push(tagId)
    } else {
      const index = this.tagIds.indexOf(tagId);
      if (index > -1)
        this.tagIds.splice(index, 1);
    }
  }
}
