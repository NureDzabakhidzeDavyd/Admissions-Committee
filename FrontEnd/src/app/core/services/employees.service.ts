import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from 'src/app/models/api-models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  private baseApiUrl = 'https://localhost:7151';

  constructor(private _httpClient: HttpClient) {}

  getEmployees(): Observable<Employee[]> {
    return this._httpClient.get<Employee[]>(this.baseApiUrl + '/employees');
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this._httpClient.get<Employee>(this.baseApiUrl + '/employees/' + id);
  }
}
