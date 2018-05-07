import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSignInComponent } from './admin-sign-in.component';

describe('AdminSignInComponent', () => {
  let component: AdminSignInComponent;
  let fixture: ComponentFixture<AdminSignInComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminSignInComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminSignInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
