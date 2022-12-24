import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CompetitiveScoreStatistic } from 'src/app/models/api-models/competitiveScoreStatistic';
import { Speciality } from 'src/app/models/api-models/speciality';
import { Statistic } from 'src/app/models/api-models/statistic';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class SpecialityService extends DataService {
  speciality: Speciality = {
    specialityId: 0,
    specialityName: '',
    specialityCode: 0,

    facultyId: 0,
    faculty: {
      facultyId: 0,
      facultyName: '',
    },

    educationalProgram: '',
    educationDegree: '',
    branchName: '',
    offerType: '',
    educationForm: '',
    educationCost: 0,
    seatTotal: 0,
    submittedApplicationsTotal: 0,
    budgetTotal: 0,
    contractTotal: 0,
    quota1Total: 0,
    quota2Total: 0,

    coefficients: [],
  };

  specialities: Speciality[] = [];

  /**
   * compareCompetitiveScore
id: number, competitiveScore: number :   */
  public compareCompetitiveScore(
    id: number,
    competitiveScore: number
  ): Observable<CompetitiveScoreStatistic> {
    return this._httpClient.get<CompetitiveScoreStatistic>(
      `${this.getUrl()}/${id}/compare-competitive/${competitiveScore}`
    );
  }

  public getSpecialityStatistics(id: number): Observable<Statistic[]> {
    return this._httpClient.get<Statistic[]>(
      `${this.getUrl()}/${id}/statistics`
    );
  }

  constructor(httpClient: HttpClient) {
    super(httpClient);
    this.entityType = 'speciality';
  }
}
