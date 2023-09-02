import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BloodAppointmentComponent } from './blood-appointment.component';

describe('BloodAppointmentComponent', () => {
  let component: BloodAppointmentComponent;
  let fixture: ComponentFixture<BloodAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BloodAppointmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BloodAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
