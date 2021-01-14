import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckTagsComponent } from './check-tags.component';

describe('CheckTagsComponent', () => {
  let component: CheckTagsComponent;
  let fixture: ComponentFixture<CheckTagsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CheckTagsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckTagsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
