import { Component, OnInit } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { ActivatedRoute } from '@angular/router';
import { Tag } from '../../models/tag/tag';
import { ToastrService } from 'ngx-toastr';
import { ErrorHandlerService } from '../../services/error-handler.service';

@Component({
    selector: 'app-tag-edit',
    templateUrl: './tag-edit.component.html',
    styleUrls: ['./tag-edit.component.css']
})
export class TagEditComponent implements OnInit {

    constructor(
        private tagService: TagService,
        private activatedRoute: ActivatedRoute,
        private toastr: ToastrService,
        private errorHandler: ErrorHandlerService
    ) { }

    public tag: Tag;
    public isActive: boolean;

    ngOnInit() {
        const name = this.activatedRoute.snapshot.paramMap.get("name");
        if (name) {
            this.isActive = true;
            this.tagService.getTag(name).subscribe(r => {
                this.tag = r;
                console.log(r);
                this.isActive = false;
            });
        }
    }

    put(): void {
        this.isActive = true;
        this.tagService.put(this.tag.id, this.tag).subscribe(r => {
            this.isActive = false;
            this.toastr.success("修改成功");
        }, err => {
            this.isActive = false;
            this.errorHandler.handleError(err)
        });
    }
}
