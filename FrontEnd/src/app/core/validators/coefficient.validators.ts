// import {
//   AbstractControl,
//   FormControl,
//   ValidationErrors,
//   ValidatorFn,
// } from '@angular/forms';

import { Directive, OnInit } from '@angular/core';
import { FormControl, NG_VALIDATORS, Validator } from '@angular/forms';

// export function ValidateCoefficientRange(): ValidatorFn {
//   return (control: AbstractControl): Promise<ValidationErrors | null> => {
//     return new Promise((resolve) => {
//       let number: number = (control as FormControl).value;
// const result = number < 100 || number > 200;

// resolve(result ? { forbiddenNumber: { value: number } } : null);
//     });
//   };
// }

@Directive({
  selector: '[coefficientNumberIsValid]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: coefficientRangeValidatorDirective,
      multi: true,
    },
  ],
})
export class coefficientRangeValidatorDirective {
  public static forbiddecCompetitiveScore(c: FormControl) {
    let number: number = (c as FormControl).value;

    const result = number < 100 || number > 200;

    return result
      ? { forbiddenNumber: { value: number, min: 100, max: 200 } }
      : null;
  }
}
