import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TagBadgeComponent } from './tag-badge.component';

describe('TagBadgeComponent', () => {
  let component: TagBadgeComponent;
  let fixture: ComponentFixture<TagBadgeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TagBadgeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TagBadgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
