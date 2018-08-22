import { TestBed, inject } from '@angular/core/testing';

import { ConfirmExitOnUnsubmittedFormService } from './confirm-exit-on-unsubmitted-form.service';

describe('ConfirmExitOnUnsubmittedFormService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConfirmExitOnUnsubmittedFormService]
    });
  });

  it('should be created', inject([ConfirmExitOnUnsubmittedFormService], (service: ConfirmExitOnUnsubmittedFormService) => {
    expect(service).toBeTruthy();
  }));
});
