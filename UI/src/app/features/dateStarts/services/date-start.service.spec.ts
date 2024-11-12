import { TestBed } from '@angular/core/testing';

import { DateStartService } from './date-start.service';

describe('DateStartService', () => {
  let service: DateStartService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DateStartService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
