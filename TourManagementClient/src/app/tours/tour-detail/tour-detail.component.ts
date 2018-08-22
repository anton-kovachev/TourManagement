import { Component, OnInit, OnDestroy } from '@angular/core';
import { TourService } from '../shared/tour.service';
import { Tour } from '../shared/tour.model';
import { ActivatedRoute } from '@angular/router';
import { OpenIdConnectService } from '../../shared/open-id-connect.service';

@Component({
  selector: 'app-tour-detail',
  templateUrl: './tour-detail.component.html',
  styleUrls: ['./tour-detail.component.css']
})
export class TourDetailComponent implements OnInit, OnDestroy {

  private tour: Tour;
  private tourId: string;
  private subsription: any;
  private isAdmin: boolean = this.openIdConnectService.user.profile.role === 'Admin';

  constructor(private tourService: TourService,
              private openIdConnectService: OpenIdConnectService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.subsription = this.route.params.subscribe(params => {
        this.tourId = params['tourId'];

        if (this.isAdmin) {
          this.tourService.getTourWithEstimatedProfitsAndShows(this.tourId).subscribe(tour => {
            this.tour = tour;
          });
        } else {
          this.tourService.getTour(this.tourId).subscribe(tour => {
            this.tour = tour;
          });
        }
    });
  }

  ngOnDestroy() {
    this.subsription.unsubscribe();
  }
}
