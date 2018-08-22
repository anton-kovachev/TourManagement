import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { TourUpdateComponent } from '../tours/tour-update/tour-update.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmExitOnUnsubmittedFormService implements CanDeactivate<TourUpdateComponent> {

  constructor() { }


  canDeactivate(tourUpdateComponent: TourUpdateComponent): boolean {
    if (tourUpdateComponent.tourForm.dirty) {
      return confirm('Unsaved changes! Are you sure you want to leave the page?');
    } else {
      return true;
    }
  }
}
