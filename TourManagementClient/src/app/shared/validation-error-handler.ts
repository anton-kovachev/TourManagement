import { FormGroup } from '@angular/forms';

export class ValidationErrorHandler {
    static handleValidationResult(form: FormGroup, validationResult: any): void {
        for (const property of Object.keys(validationResult)) {
          if (form.controls[property]) {
              const formFieldValidationErrors = {};

              validationResult[property].forEach(error => {
                  formFieldValidationErrors[error.validatorKey] = error.message;
            });

            form.controls[property].setErrors(formFieldValidationErrors);
          } else {
            const formValidationErrors = {};

            validationResult[property].forEach(error => {
              formValidationErrors[error.validatorKey] = error.message;
            });

            form.setErrors(formValidationErrors);
          }
        }
      }
}
