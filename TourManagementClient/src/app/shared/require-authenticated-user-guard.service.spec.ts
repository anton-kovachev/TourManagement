import { TestBed, inject } from '@angular/core/testing';

import { RequireAuthenticatedUserGuardService } from './require-authenticated-user-guard.service';

describe('RequireAuthenticatedUserGuardService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RequireAuthenticatedUserGuardService]
    });
  });

  it('should be created', inject([RequireAuthenticatedUserGuardService], (service: RequireAuthenticatedUserGuardService) => {
    expect(service).toBeTruthy();
  }));
});
