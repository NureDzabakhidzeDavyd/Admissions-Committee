import { Component, Input } from '@angular/core';
import { Coefficient } from 'src/app/models/ui-models/coefficient.model';

@Component({
  selector: 'speciality-coefficients',
  templateUrl: './speciality-coefficients.component.html',
  styleUrls: ['./speciality-coefficients.component.scss'],
})
export class SpecialityCoefficientsComponent {
  @Input() coefficients: Coefficient[] = [];

  constructor() {}
}
