import { AbstractControl, ValidatorFn, FormArray, FormControl } from '@angular/forms';

export class CustomValidators {
    static StartDateBeforeEndDateValidator(control: AbstractControl) {
        const startDate = control.get('startDate').value;
        const endDate = control.get('endDate').value;

        if (startDate < endDate) {
            return null;
        }

        return { startDateBeforeEndDate: {valid: false} };
    }

    static ShowDateInTourRangeValidator(): ValidatorFn {
        return (control: AbstractControl): {[key: string]: any} | null => {

            if (control && control.parent) {
                const tourStartDate = control.parent.get('startDate').value;
                const tourEndDate = control.parent.get('endDate').value;

                const shows = (control as FormArray).value;

                let invalid = false;
                const allDatesValid = shows.forEach(show => {
                    const showDate = show.date;

                    if (showDate < tourStartDate || showDate > tourEndDate) {
                        invalid = true;
                    }
                });

                if (!invalid) {
                    return null;
                }
            }

            return  { showDateInTourRange: {valid: false} };
        };
    }

    static ShowDateInTourTimeframe(): ValidatorFn {
        return (control: AbstractControl): {[key: string]: any} | null => {
            if (!control.parent) {
                return null;
            }

            const tourStartDate = control.parent.parent.get('startDate').value;
            const tourEndDate = control.parent.parent.get('endDate').value;

            const showDate = control.get('date').value;

            if (showDate >= tourStartDate && showDate <= tourEndDate) {
                return null;
            }

            return {showDateInTourTimeframe: {valid: false}};
        };
    }
}
