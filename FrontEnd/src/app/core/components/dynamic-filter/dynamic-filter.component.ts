import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  RequiredValidator,
  Validators,
} from '@angular/forms';
import { DynamicFilter } from 'src/app/models/api-request/dynamic-filter/dynamicFilter';
import { DynamicFilters } from 'src/app/models/api-request/dynamic-filter/dynamicFilters';

@Component({
  selector: 'app-dynamic-filter',
  templateUrl: './dynamic-filter.component.html',
  styleUrls: [
    './dynamic-filter.component.scss',
    '../../../../styles/button.styles.scss',
  ],
})
export class DynamicFilterComponent {
  @Input() fields: string[] = [];
  @Output('onDynamicSearch') newItemEvent = new EventEmitter<DynamicFilters>();
  form: FormGroup;

  public get dynamicFilters() {
    return this.form.get('dynamicFilters') as FormArray;
  }

  public addDynamicFilter() {
    this.dynamicFilters.push(
      new FormGroup({
        fieldName: new FormControl('', Validators.required),
        value: new FormControl('', Validators.required),
      })
    );
  }

  public deleteDynamicFilter(formGroupNumber: number) {
    this.dynamicFilters.removeAt(formGroupNumber);
    if (this.dynamicFilters.length < 1) {
      let dynamicFilters: DynamicFilters = { filters: [] };
      this.newItemEvent.emit(dynamicFilters);
    }
  }

  public searchByDynamicFields() {
    let dynamicFilters: DynamicFilters = { filters: [] };
    this.dynamicFilters?.controls.forEach((group) => {
      let inputfieldName = this.capitalizeFirstLatter(
        group.get('fieldName')?.value
      );
      let inputvalue = group.get('value')?.value;
      let inputfieldType = this.defineFieldType(inputvalue);

      let dynamicFilter: DynamicFilter = {
        fieldName: inputfieldName,
        fieldType: inputfieldType,
        value: inputvalue,
      };

      dynamicFilters.filters.push(dynamicFilter);
    });
    this.newItemEvent.emit(dynamicFilters);
  }

  private defineFieldType(field: string): number {
    if (!isNaN(Number(field))) {
      return 2;
    }
    return 1;
  }

  private capitalizeFirstLatter(field: string) {
    return field.charAt(0).toUpperCase() + field.slice(1);
  }

  constructor(formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      dynamicFilters: formBuilder.array([]),
    });
  }
}
