import { Component, OnInit } from '@angular/core';
import { Tag } from '../../models/tag/tag';
import { TagService } from '../../services/tag.service';

@Component({
    selector: 'app-tag-list',
    templateUrl: './tag-list.component.html',
    styleUrls: ['./tag-list.component.css']
})
export class TagListComponent implements OnInit {

    constructor(
        private tagService: TagService
    ) { }

    public tags: Tag[];

    ngOnInit() {
        this.tagService.getTags().subscribe(r => {
            this.tags = r;
        });
    }
}
