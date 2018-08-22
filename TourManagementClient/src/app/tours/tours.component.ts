import { Component, OnInit } from '@angular/core';
import { TourService } from './shared/tour.service';
import { Tour } from './shared/tour.model';
import { Observable } from 'rxjs';
import { OpenIdConnectService } from '../shared/open-id-connect.service';
import { ToursStateService } from './shared/tours-state.service';


@Component({
  selector: 'app-tours',
  templateUrl: './tours.component.html',
  styleUrls: ['./tours.component.css'],
})
export class ToursComponent implements OnInit {

  title = 'Tour Overview';
  tours: Tour[];
  filteredTours: Tour[];
  isAdmin;

  private _tourService: TourService;
  private _tourStateService: ToursStateService;

  constructor(tourSerivce: TourService, tourStateService: ToursStateService, openIdConnectService: OpenIdConnectService) {
    this._tourService = tourSerivce;
    this._tourStateService = tourStateService;
    this.isAdmin = openIdConnectService.isAdmin;
  }

  ngOnInit() {
    this._tourService.getTours().subscribe(tours => {
      this.tours = tours;
      this.filterBy = this._tourStateService.filterBy;
    });
  }

  get filterBy(): string {
    return this._tourStateService.filterBy;
  }

  set filterBy(value: string) {
    this._tourStateService.filterBy = value;
    this.performFilter(value);
  }

  private performFilter(filterBy: string): void {
    if (filterBy) {
      this.filteredTours = this.tours.filter( (tour: Tour) => tour.band.toLowerCase().indexOf(filterBy.toLowerCase()) !== -1 );
    } else {
      this.filteredTours = this.tours;
    }
  }
}
