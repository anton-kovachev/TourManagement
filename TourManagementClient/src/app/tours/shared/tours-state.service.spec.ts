import { TestBed, inject } from '@angular/core/testing';

import { ToursStateService } from './tours-state.service';

describe('ToursStateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ToursStateService]
    });
  });

  it('should be created', inject([ToursStateService], (service: ToursStateService) => {
    expect(service).toBeTruthy();
  }));
});
