import { Injectable, OnDestroy } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToursStateService implements OnDestroy {

  filterBy: string;

  constructor() {
    console.log('TourState Service created!');
  }

  ngOnDestroy() {
    console.log('TourState Service destroyed!');
  }
}
