import { Component, OnInit, OnDestroy } from '@angular/core';
import { TourService } from '../shared/tour.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';

import { compare } from 'fast-json-patch';
import { } from 'automapper-ts';

import { DatePipe } from '@angular/common';
import { Manager } from '../../shared/manager.model';
import { MasterDataService } from '../../shared/master-data.service';
import { CustomValidators } from '../../shared/custom-validators';
import { OpenIdConnectService } from '../../shared/open-id-connect.service';
import { catchError } from 'rxjs/operators';
import { empty } from 'rxjs';
import { ValidationErrorHandler } from '../../shared/validation-error-handler';
import { ShowSingleComponent } from '../shows/show-single/show-single.component';

@Component({
  selector: 'app-tour-update',
  templateUrl: './tour-update.component.html',
  styleUrls: ['./tour-update.component.css']
})
export class TourUpdateComponent implements OnInit, OnDestroy {


  public tourForm: FormGroup;
  public isAdmin = this.openIdConnectService.isAdmin;

  private tour: any;
  private originalTourForUpdate: any;
  private subscribtion: any;
  private tourId: string;
  private managers: Manager[];

  constructor(private tourService: TourService,
              private masterDateService: MasterDataService,
              private openIdConnectService: OpenIdConnectService,
              private route: ActivatedRoute,
              private router: Router,
              private formBuilder: FormBuilder
            ) { }

  ngOnInit() {

    this.masterDateService.getManagers().subscribe(managers => {
      this.managers = managers;

    this.tourForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      manager: [, this.isAdmin ? Validators.required : '' ],
      description: [, [Validators.required, Validators.maxLength(2000)]],
      estimatedProfits: this.isAdmin ? [, [Validators.required, Validators.min(0), Validators.max(100000000000)]] : [],
      startDate: [, Validators.required],
      endDate: [, Validators.required],
      shows: new FormArray([], CustomValidators.ShowDateInTourRangeValidator()),
    }, { validator: CustomValidators.StartDateBeforeEndDateValidator });

    this.subscribtion = this.route.params.subscribe(params => {
      this.tourId = params['tourId'];

      if (this.isAdmin) {
        // this.tourService.getTourWithEstimatedProfitsAndManager(this.tourId).subscribe(tour => {
        //   this.tour = tour;
        //   this.updateForm();
        //   this.originalTourForUpdate = automapper.map('TourFormModel', 'TourWithEstimatedProfitsAndManagerForUpdate', this.tourForm.value);
        this.tourService.getTourWithEstimatedProfitsAndManagerAndShows(this.tourId).subscribe(tour => {
          this.tour = tour;
          this.updateForm();
          this.originalTourForUpdate = automapper.map('TourFormModel',
                                                      'TourWithEstimatedProfitsAndManagerAndShowsForUpdate',
                                                       this.tourForm.value);
        });
       } else {
        this.tourService.getTourWithShows(this.tourId).subscribe(tour => {
          this.tour = tour;

          this.updateForm();
          this.originalTourForUpdate = automapper.map('TourFormModel', 'TourWithShowsForUpdate', this.tourForm.value);
        });
      }
    });
  });
  }

  ngOnDestroy() {
    this.subscribtion.unsubscribe();
  }

  saveTour() {
    if (this.tourForm.dirty && this.tourForm.valid) {
      if (this.isAdmin) {
/*         const updatedTour = automapper.map('TourFormModel', 'TourWithEstimatedProfitsAndManagerForUpdate', this.tourForm.value);
        const pacthDocument = compare(this.originalTourForUpdate, updatedTour);

        this.tourService.partiallyUpdateTourWithEstimatedProfitsAndManager(this.tourId, pacthDocument)
          .pipe(catchError( (err, caught) => { alert('Tour Update Failed'); return empty(); } ))
          .subscribe( _ => this.router.navigateByUrl('/tours')); */

        const updatedTour = automapper.map('TourFormModel', 'TourWithEstimatedProfitsAndManagerAndShowsForUpdate', this.tourForm.value);
        const pacthDocument = compare(this.originalTourForUpdate, updatedTour);

        this.tourService.partiallyUpdateTourWithEstimatedProfitsAndManagerAndShows(this.tourId, pacthDocument)
          .pipe(catchError( (err, caught) => { alert('Tour Update Failed'); return empty(); } ))
          .subscribe( _ => {
                        this.tourForm.reset();
                        this.router.navigateByUrl('/tours');
                      },
                      (validatinResult) => ValidationErrorHandler.handleValidationResult(this.tourForm, validatinResult));

      } else {
        const updatedTour = automapper.map('TourFormModel', 'TourWithShowsForUpdate', this.tourForm.value);
        const patchDocument = compare(this.originalTourForUpdate, updatedTour);

        this.tourService.partiallyUpdateTourWithShows(this.tourId, patchDocument)
          // .pipe( catchError( (error, caught) => { alert('Error Updating Tour'); return empty(); }))
          .subscribe( _ => {
                      this.tourForm.reset();
                      this.router.navigateByUrl('/tours');
                    },
                    (validationResult) => ValidationErrorHandler.handleValidationResult(this.tourForm, validationResult));
      }
    }
  }

  addShow(): void {
    const shows = this.tourForm.get('shows') as FormArray;
    shows.push(ShowSingleComponent.createShow());
  }

  removeShow(index: number): void {
    const shows = this.tourForm.get('shows') as FormArray;
    shows.removeAt(index);
  }

  private updateForm() {
    const datePipe = new DatePipe('en-EN');
    const format = 'yyyy-MM-dd';

    this.tourForm.patchValue({
      title: this.tour.title,
      manager: this.tour.managerId, // this.managers.find( m => m.managerId === this.tour.managerId),
      estimatedProfits: (this.isAdmin ? this.tour.estimatedProfits : null),
      description: this.tour.description,
      startDate: datePipe.transform(this.tour.startDate, format),
      endDate: datePipe.transform(this.tour.endDate, format),
      // shows: (this.isAdmin ? this.tourForm.get('shows').value.concat(ShowSingleComponent.updateShows(this.tour.shows)) : []),
    });

      const shows = this.tourForm.get('shows') as FormArray;
      const showGroups = ShowSingleComponent.updateShows(this.tour.shows);
      showGroups.forEach( show => shows.push(show) );
  }

  get title() { return this.tourForm.get('title'); }
  get manager() { return this.tourForm.get('manager'); }
  get description() { return this.tourForm.get('description'); }
  get estimatedProfits() { return this.tourForm.get('estimatedProfits'); }
  get startDate() { return this.tourForm.get('startDate'); }
  get endDate() { return this.tourForm.get('endDate'); }
  get shows() { return this.tourForm.get('shows'); }
}
