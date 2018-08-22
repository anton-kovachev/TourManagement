import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { BaseService } from '../../shared/base.service';
import { Tour } from './tour.model';
import { Observable } from 'rxjs';
import { TourForUpdate } from './tour-for-update.model';
import { Operation } from 'fast-json-patch';
import { TourWithShowsForCreation } from './tour-with-shows-for-creation.model';
import { TourWithManagerAndShowsForCreation } from './tour-with-manager-shows-for-creation.model';
import { TourWithManagerForCreation } from './tour-with-manager-for-creation.model';
import { TourForCreation } from './tour-for-creation.model';
import { TourWithEstimatedProfits } from './tour-with-estimated-profits.model';
import { TourWithEstimatedProfitsAndManager } from './tour-with-estimated-profits-and-manager.model';
import { TourWithShows } from './tour-with-shows.model';
import { TourWithEstimatedProfitsAndManagerAndShows } from './tour-with-estimated-profits-and-manager-and-shows.model';

@Injectable({
  providedIn: 'root'
})
export class TourService extends BaseService {

    constructor(private http: HttpClient) {
      super();
    }

    getTours(): Observable<Tour[]> {
      const tours = this.http.get<Tour[]>(`${this.apiUrl}tours`);
      return tours;
    }

    getTour(tourId: string): Observable<Tour> {
      return this.http.get<Tour>(`${this.apiUrl}tours/${tourId}`);
    }

    getTourWithEstimatedProfits(tourId: string): Observable<TourWithEstimatedProfits> {
      return this.http.get<TourWithEstimatedProfits>(`${this.apiUrl}tours/${tourId}`,
        {headers: {'Accept': 'application/vnd.toursltd.tourwithestimatedprofits+json'}});
    }

    getTourWithEstimatedProfitsAndManager(tourId: string): Observable<TourWithEstimatedProfitsAndManager> {
      return this.http.get<TourWithEstimatedProfitsAndManager>(`${this.apiUrl}tours/${tourId}`,
        {headers: {'Accept': 'application/vnd.toursltd.tourwithestimatedprofitsandmanager+json'}});
    }

    getTourWithEstimatedProfitsAndManagerAndShows(tourId: string): Observable<TourWithEstimatedProfitsAndManagerAndShows> {
      return this.http.get<TourWithEstimatedProfitsAndManagerAndShows>(`${this.apiUrl}tours/${tourId}`,
        {headers: {'Accept': 'application/vnd.toursltd.tourwithestimatedprofitsandmanagerandshows+json'}});
    }

    getTourWithShows(tourId: string): Observable<TourWithShows> {
      return this.http.get<TourWithShows>(`${this.apiUrl}tours/${tourId}`,
        {headers: {'Accept': 'application/vnd.toursltd.tourwithshows+json'}});
    }

    getTourWithEstimatedProfitsAndShows(tourId: string): Observable<Tour> {
      return this.http.get<Tour>(`${this.apiUrl}tours/${tourId}`);
    }

    addTour(tour: TourForCreation): Observable<TourForCreation> {
      return this.http.post<TourForCreation>(`${this.apiUrl}tours`, tour,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourforcreation+json'}});
    }

    addTourWithManager(tour: TourWithManagerForCreation): Observable<TourWithManagerForCreation> {
      return this.http.post<TourWithManagerForCreation>(`${this.apiUrl}tours`, tour,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithmanagerforcreation+json'}});
    }

    addTourWithShows(tour: TourWithShowsForCreation): Observable<TourWithShowsForCreation> {
      return this.http.post<TourWithShowsForCreation>(`${this.apiUrl}tours`, tour,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithshowsforcreation+json'}});
    }

    addTourWithManagerAndShows(tour: TourWithManagerAndShowsForCreation): Observable<TourWithManagerAndShowsForCreation> {
      return this.http.post<TourWithManagerAndShowsForCreation>(`${this.apiUrl}tours`, tour,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithmanagerandshowsforcreation+json'}});
    }

    partiallyUpdateTour(tourId, patchDocument: Operation[] ): Observable<any> {
      return this.http.patch(`${this.apiUrl}tours/${tourId}`, patchDocument,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourforupdate-json-patch+json'}});
    }

    partiallyUpdateTourWithShows(tourId, patchDocument: Operation[] ): Observable<any> {
      return this.http.patch(`${this.apiUrl}tours/${tourId}`, patchDocument,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithshowsforupdate-json-patch+json'}});
    }

    partiallyUpdateTourWithEstimatedProfits(tourId, patchDocument: Operation[] ): Observable<any> {
      return this.http.patch(`${this.apiUrl}tours/${tourId}`, patchDocument,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithestimatedprofitsforupdate-json-patch+json'}});
    }

    partiallyUpdateTourWithEstimatedProfitsAndManager(tourId, patchDocument: Operation[] ): Observable<any> {
      return this.http.patch(`${this.apiUrl}tours/${tourId}`, patchDocument,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithestimatedprofitsandmanagerforupdate-json-patch+json'}});
    }

    partiallyUpdateTourWithEstimatedProfitsAndManagerAndShows(tourId, patchDocument: Operation[] ): Observable<any> {
      return this.http.patch(`${this.apiUrl}tours/${tourId}`, patchDocument,
        {headers: {'Content-Type': 'application/vnd.toursltd.tourwithestimatedprofitsandmanagerandshowsforupdate-json-patch+json'}});
    }
}
