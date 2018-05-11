import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticleEditButtonComponent } from './article-edit-button.component';

describe('ArticleEditButtonComponent', () => {
  let component: ArticleEditButtonComponent;
  let fixture: ComponentFixture<ArticleEditButtonComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticleEditButtonComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticleEditButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
