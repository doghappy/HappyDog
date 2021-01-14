import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TagPostComponent } from './tag-post.component';

describe('TagPostComponent', () => {
  let component: TagPostComponent;
  let fixture: ComponentFixture<TagPostComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TagPostComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TagPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
