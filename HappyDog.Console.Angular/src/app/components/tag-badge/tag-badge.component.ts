import { Component, OnInit, Input } from '@angular/core';
import { Tag } from '../../models/tag/tag';

@Component({
  selector: 'app-tag-badge',
  templateUrl: './tag-badge.component.html',
  styleUrls: ['./tag-badge.component.css']
})
export class TagBadgeComponent implements OnInit {

  constructor() { }

  @Input()
  tag: Tag;

  get isCustomIcon(): boolean {
    if (this.tag) {
      return !!this.tag.glyph && !!this.tag.glyphFont;
    }
    return false;
  }

  ngOnInit() {
  }

}
