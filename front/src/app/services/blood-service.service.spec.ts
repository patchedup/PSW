import { TestBed } from '@angular/core/testing';

import { BloodServiceService } from './blood-service.service';

describe('BloodServiceService', () => {
  let service: BloodServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BloodServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
