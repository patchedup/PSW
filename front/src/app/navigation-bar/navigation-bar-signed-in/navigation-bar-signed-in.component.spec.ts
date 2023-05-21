import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavigationBarSignedInComponent } from './navigation-bar-signed-in.component';

describe('NavigationBarSignedInComponent', () => {
  let component: NavigationBarSignedInComponent;
  let fixture: ComponentFixture<NavigationBarSignedInComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavigationBarSignedInComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavigationBarSignedInComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
