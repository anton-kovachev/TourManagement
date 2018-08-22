import { Show } from '../shared/show.model';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray } from '@angular/forms';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CustomValidators } from '../../../shared/custom-validators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-show-single',
  templateUrl: './show-single.component.html',
  styleUrls: ['./show-single.component.css']
})
export class ShowSingleComponent implements OnInit {

  @Input() show: FormGroup;
  @Input() showIndex: number;
  @Output() removeShowClicked: EventEmitter<number> = new EventEmitter<number>();

  constructor(private formBuilder: FormBuilder) { }

  static createShow() {
    return new FormGroup({
      date: new FormControl('', [Validators.required]),
      venue: new FormControl('', [Validators.required, Validators.maxLength(150)]),
      city: new FormControl('', [Validators.required, Validators.maxLength(150)]),
      country: new FormControl('', [Validators.required, Validators.maxLength(150)]),
    }, CustomValidators.ShowDateInTourTimeframe());
  }

  static updateShows(shows: Show[]): FormGroup[] {
    const datePipe = new DatePipe('en-EN');
    const format = 'yyyy-MM-dd';
    return shows.map( show => {
      const showForm = this.createShow();

      showForm.patchValue({
        date: datePipe.transform(show.date, format),
        venue: show.venue,
        city: show.city,
        country: show.country,
      });

      return showForm;
    });
  }

  removeShow() {
   this.removeShowClicked.emit(this.showIndex);
  }

  testShow() {
    const testVal = this.show.get('venue').touched;
    const testVal2 = this.show.controls['venue'].touched;
  }

  ngOnInit() {
  }

  get date() { return this.show.get('date'); }
  get venue() { return this.show.get('venue'); }
  get city() { return this.show.get('city'); }
  get country() { return this.show.get('country'); }
}
