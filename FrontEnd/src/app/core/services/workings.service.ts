import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Working } from 'src/app/models/api-models/working';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class WorkingsService extends DataService {
  constructor(private httpClient: HttpClient) {
    super(httpClient);
    this.entityType = 'workings';
  }

  public GetEmployeeWorkingsAsync(employeeId: number): Observable<Working[]> {
    return this.httpClient.get<Working[]>(`/${this.entityType}/${employeeId}`);
  }
}
