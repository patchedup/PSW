import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InternistDataComponent } from './internist-data.component';

describe('InternistDataComponent', () => {
  let component: InternistDataComponent;
  let fixture: ComponentFixture<InternistDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InternistDataComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InternistDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
