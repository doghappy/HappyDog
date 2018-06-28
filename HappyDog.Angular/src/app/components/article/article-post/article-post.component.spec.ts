import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlePostComponent } from './article-post.component';

describe('ArticlePostComponent', () => {
  let component: ArticlePostComponent;
  let fixture: ComponentFixture<ArticlePostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticlePostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticlePostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
