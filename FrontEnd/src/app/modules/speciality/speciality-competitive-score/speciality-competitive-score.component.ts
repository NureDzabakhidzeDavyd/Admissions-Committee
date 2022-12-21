import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { SpecialityService } from 'src/app/core/services/specialities.service';
import { Coefficient } from 'src/app/models/ui-models/coefficient.model';
import { CompetitiveScoreStatistic } from 'src/app/models/ui-models/competitiveScoreStatistic.model';

@Component({
  selector: 'speciality-competitive-score',
  templateUrl: './speciality-competitive-score.component.html',
  styleUrls: ['./speciality-competitive-score.component.scss'],
})
export class CompetitiveScoreComponent {
  @Input() specialityId: number = 0;
  @Input() coefficients: Coefficient[] = [];

  competitiveScore: number = 0;

  competitiveScoreStatistic: CompetitiveScoreStatistic = {
    applicantCompetitiveScorePosition: 0,
    totalApplicantsCount: 0,
    averageCompetitiveScore: 0,
  };

  // CALC COMP SCORE
  calcCompetitveScoreForm = this.formBuilder.group({
    firstNumber: 0,
    secondNumber: 0,
    thirdNumber: 0,
  });

  // calculateCompetitiveScore(firstNumber:)

  compareCompetitiveScore() {
    if (
      this.specialityId != 0 &&
      this.competitiveScore > 100 &&
      this.competitiveScore < 201
    ) {
      this.specialityService
        .compareCompetitiveScore(this.specialityId, this.competitiveScore)
        .subscribe({
          next: (successResponse) => {
            this.competitiveScoreStatistic = successResponse;
            console.log(this.competitiveScoreStatistic);
          },
        });
    }
  }

  calculateCompetitiveScore(): number {
    let result: number = 0;
    let firstNumber = this.calcCompetitveScoreForm.get('firstNumber')?.value;
    let secondNumber = this.calcCompetitveScoreForm.get('secondNumber')?.value;
    let thirdNumber = this.calcCompetitveScoreForm.get('thirdNumber')?.value;
    if (firstNumber && secondNumber && thirdNumber) {
      result =
        this.coefficients[0].coefficientValue * firstNumber +
        this.coefficients[1].coefficientValue * secondNumber +
        this.coefficients[2].coefficientValue * thirdNumber;
    } else {
      throw new Error("Can't find numbers values");
    }
    console.log(`${this.coefficients[0].coefficientValue} ${firstNumber}`);
    console.log(`${this.coefficients[1].coefficientValue} ${secondNumber}`);
    console.log(`${this.coefficients[2].coefficientValue} ${thirdNumber}`);
    console.log(result);
    return result;
  }

  public get CompetitveScoreForm(): FormGroup {
    return this.calcCompetitveScoreForm;
  }

  constructor(
    private readonly specialityService: SpecialityService,
    private formBuilder: FormBuilder
  ) {}
}
