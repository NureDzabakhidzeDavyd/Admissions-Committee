import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Applicant } from 'src/app/models/api-models/applicant';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class ApplicantService extends DataService {
  private baseApiUrl = 'https://localhost:7151';

  constructor(_httpClient: HttpClient) {
    super(_httpClient);
    this.entityType = 'applicants';
  }

  getApplicants(): Observable<Applicant[]> {
    return this._httpClient.get<Applicant[]>(this.baseApiUrl + '/applicants');
  }

  // getApplicantsByFilters(
  //   dynamicFilters: DynamicFilters
  // ): Observable<Applicant[]> {
  //   var request = '/applicants';
  //   dynamicFilters.filters.forEach((dynamicFilter, index) => {
  //     const fieldType: number = dynamicFilter.value === 'string' ? 1 : 2;
  //     const value = 'Микита';
  //     request += `${this.baseApiUrl}/applicants/filters[${index}].fieldName=${dynamicFilter.fieldName}&filters[${index}].fieldType=${fieldType}&filters[${index}].value='${value}'`;
  //   });

  //   return this._httpClient.get<Applicant[]>(request);
  // }
}
