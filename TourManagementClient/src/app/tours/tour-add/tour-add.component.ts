import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable, empty } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { } from 'automapper-ts';

import { Band } from '../../shared/band.model';
import { Manager } from '../../shared/manager.model';
import { MasterDataService } from '../../shared/master-data.service';
import { TourService } from '../shared/tour.service';
import { CustomValidators } from '../../shared/custom-validators';
import { Show } from '../shows/shared/show.model';
import { ShowSingleComponent } from '../shows/show-single/show-single.component';
import { OpenIdConnectService } from '../../shared/open-id-connect.service';
import { User } from 'oidc-client';
import { CATCH_ERROR_VAR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-tour-add',
  templateUrl: './tour-add.component.html',
  styleUrls: ['./tour-add.component.css']
})
export class TourAddComponent implements OnInit {

  bands: Band[] = [];
  managers: Manager[] = [];
  tourForm: FormGroup;
  isAdmin: boolean = this.openIdConnectService.user.profile.role === 'Admin';

  constructor(private tourService: TourService,
              private masterDataService: MasterDataService,
              private openIdConnectService: OpenIdConnectService,
              private formBuilder: FormBuilder,
              private router: Router ) { }

  ngOnInit() {

    this.tourForm = this.formBuilder.group({
      band: [, Validators.required],
      title: ['', [Validators.required, Validators.maxLength(200)]],
      manager: [],
      description: [, [Validators.required, Validators.maxLength(2000)]],
      estimatedProfits: [, [Validators.required, Validators.min(0), Validators.max(100000000000)]],
      startDate: [, Validators.required],
      endDate: [, Validators.required],
      shows: new FormArray([], CustomValidators.ShowDateInTourRangeValidator()),
    }, { validator: CustomValidators.StartDateBeforeEndDateValidator });

    this.masterDataService.getBands().subscribe(bands => this.bands = bands);

    if (this.isAdmin) {
      this.masterDataService.getManagers().subscribe(managers => this.managers = managers);
    }
  }

  addTour() {
    if (this.tourForm.valid) {
      console.log(this.tourForm);
      if (this.isAdmin) {
        if (this.tourForm.value.shows.length) {
          const tourForCreation = automapper.map('TourFormModel', 'TourWithManagerAndShowsForCreation', this.tourForm.value);

          this.tourService.addTourWithManagerAndShows(tourForCreation)
            .pipe(catchError( (error, caught) => { alert('Error Creating Tour'); return empty(); }))
            .subscribe(_ => this.router.navigateByUrl('/tours'));
        } else {
          const tourForCreation = automapper.map('TourFormModel', 'TourWithShowsForCreation', this.tourForm.value);

          this.tourService.addTourWithManager(tourForCreation)
            .pipe(catchError( (error, caught) => { alert('Error Creating Tour'); return empty(); }))
            .subscribe(_ => this.router.navigateByUrl('/tours'));
        }
      } else {
        const tourForCreation = automapper.map('TourFormModel', 'TourWithShowsForCreation', this.tourForm.value);

        this.tourService.addTourWithShows(tourForCreation)
          .pipe(catchError( (error, caught) => { alert('Error Creating Tour'); return empty(); }))
          .subscribe(_ => this.router.navigateByUrl('/tours'));

      }
    }
  }

  addShow() {
    const shows = this.tourForm.get('shows') as FormArray;
    shows.push(ShowSingleComponent.createShow());
  }

  get band() { return this.tourForm.get('band'); }
  get manager() { return this.tourForm.get('manager'); }
  get title() { return this.tourForm.get('title'); }
  get description() { return this.tourForm.get('description'); }
  get estimatedProfits() { return this.tourForm.get('estimatedProfits'); }
  get startDate() { return this.tourForm.get('startDate'); }
  get endDate() { return this.tourForm.get('endDate'); }
  get shows() { return this.tourForm.get('shows'); }
}
