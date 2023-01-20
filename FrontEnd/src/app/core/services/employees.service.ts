import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from 'src/app/models/api-models/employee';

import { Faculty } from 'src/app/models/api-models/faculty';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService extends DataService {
  private baseApiUrl = 'https://localhost:7151';

  constructor(_httpClient: HttpClient) {
    super(_httpClient);
    this.entityType = 'employees';
  }

  getEmployees(): Observable<Employee[]> {
    return this._httpClient.get<Employee[]>(this.baseApiUrl + '/employees');
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this._httpClient.get<Employee>(this.baseApiUrl + '/employees/' + id);
  }

  updateEmployee(employeeId: number, employee: any) {
    return this._httpClient.put(`${this.getUrl()}/${employeeId}`, employee);
  }
}
