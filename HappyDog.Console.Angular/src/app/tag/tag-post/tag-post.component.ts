import { Component, OnInit } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { Tag } from '../../models/tag/tag';
import { ToastrService } from 'ngx-toastr';
import { ErrorHandlerService } from '../../services/error-handler.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tag-post',
  templateUrl: './tag-post.component.html',
  styleUrls: ['./tag-post.component.css']
})
export class TagPostComponent implements OnInit {

  constructor(
    private tagService: TagService,
    private toastr: ToastrService,
    private errorHandler: ErrorHandlerService,
    private router: Router
  ) { }

  public tag: Tag;
  public isActive: boolean;

  ngOnInit() {
    this.tag = new Tag();
  }

  post(): void {
    this.isActive = true;
    this.tagService.post(this.tag).subscribe(r => {
      this.isActive = false;
      this.toastr.success("创建成功");
      this.router.navigate(['tags']);
    }, err => {
      this.isActive = false;
      this.errorHandler.handleError(err)
    });
  }
}
