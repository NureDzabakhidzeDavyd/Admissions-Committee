import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm } from '@angular/forms';
import { SpecialityService } from 'src/app/core/services/specialities.service';
import { coefficientRangeValidatorDirective } from 'src/app/core/validators/coefficient.validators';
import { Coefficient } from 'src/app/models/ui-models/coefficient.model';
import { CompetitiveScoreStatistic } from 'src/app/models/ui-models/competitiveScoreStatistic.model';

@Component({
  selector: 'speciality-competitive-score',
  templateUrl: './speciality-competitive-score.component.html',
  styleUrls: [
    './speciality-competitive-score.component.scss',
    '../../../../styles/button.styles.scss',
  ],
})
export class CompetitiveScoreComponent {
  @Input() specialityId: number = 0;
  @Input() coefficients: Coefficient[] = [];

  public compareApplicantCompetitiveScore: number = 0;
  public calculateApplicantCompetitiveScore: number = 0;

  competitiveScoreStatistic: CompetitiveScoreStatistic = {
    applicantCompetitiveScorePosition: 0,
    totalApplicantsCount: 0,
    averageCompetitiveScore: 0,
  };
  showCompetitiveScoreStatistic: boolean = false;
  competitiveScoreIsCalculated: boolean = false;

  // CALC COMP SCORE
  calcCompetitveScoreForm = this.formBuilder.group({
    firstNumber: [
      0,
      coefficientRangeValidatorDirective.forbiddecCompetitiveScore,
    ],
    secondNumber: [
      0,
      coefficientRangeValidatorDirective.forbiddecCompetitiveScore,
    ],
    thirdNumber: [
      0,
      coefficientRangeValidatorDirective.forbiddecCompetitiveScore,
    ],
  });

  compareCompetitiveScore() {
    if (
      this.specialityId != 0 &&
      this.compareApplicantCompetitiveScore > 100 &&
      this.compareApplicantCompetitiveScore < 201
    ) {
      this.specialityService
        .compareCompetitiveScore(
          this.specialityId,
          this.compareApplicantCompetitiveScore
        )
        .subscribe({
          next: (successResponse) => {
            this.competitiveScoreStatistic = successResponse;
            this.showCompetitiveScoreStatistic = true;
            console.log(this.competitiveScoreStatistic);
          },
        });
    }
  }

  closeCompareResult(): void {
    this.showCompetitiveScoreStatistic = !this.showCompetitiveScoreStatistic;
  }

  calculateCompetitiveScore(): number {
    let firstNumber: number = (
      this.calcCompetitveScoreForm.get('firstNumber') as FormControl
    ).value;
    let secondNumber: number = (
      this.calcCompetitveScoreForm.get('secondNumber') as FormControl
    ).value;
    let thirdNumber: number = (
      this.calcCompetitveScoreForm.get('thirdNumber') as FormControl
    ).value;
    if (firstNumber && secondNumber && thirdNumber) {
      this.calculateApplicantCompetitiveScore =
        this.coefficients[0].coefficientValue * firstNumber +
        this.coefficients[1].coefficientValue * secondNumber +
        this.coefficients[2].coefficientValue * thirdNumber;
    } else {
      throw new Error("Can't find numbers values");
    }
    console.log(`${this.coefficients[0].coefficientValue} ${firstNumber}`);
    console.log(`${this.coefficients[1].coefficientValue} ${secondNumber}`);
    console.log(`${this.coefficients[2].coefficientValue} ${thirdNumber}`);
    console.log(this.calculateApplicantCompetitiveScore);
    this.competitiveScoreIsCalculated = true;
    return this.calculateApplicantCompetitiveScore;
  }

  public closeCalculateResult() {
    this.competitiveScoreIsCalculated = !this.competitiveScoreIsCalculated;
  }

  public get CompetitveScoreForm(): FormGroup {
    return this.calcCompetitveScoreForm;
  }

  public get ThirdNumber(): FormControl {
    return this.calcCompetitveScoreForm.get('thirdNumber') as FormControl;
  }
  public get SecondNumber(): FormControl {
    return this.calcCompetitveScoreForm.get('secondNumber') as FormControl;
  }
  public get FirstNumber(): FormControl {
    return this.calcCompetitveScoreForm.get('firstNumber') as FormControl;
  }
  public get Score(): number {
    return this.compareApplicantCompetitiveScore;
  }

  constructor(
    private readonly specialityService: SpecialityService,
    private formBuilder: FormBuilder
  ) {}
}
