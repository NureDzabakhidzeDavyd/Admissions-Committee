import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from 'src/app/models/api-models/employee';
import { Faculty } from 'src/app/models/api-models/faculty';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  private baseApiUrl = 'https://localhost:7151';

  constructor(private _httpClient: HttpClient, private dataService: DataService) {}

  getEmployees(): Observable<Employee[]> {
    return this._httpClient.get<Employee[]>(this.baseApiUrl + '/employees');
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this._httpClient.get<Employee>(this.baseApiUrl + '/employees/' + id);
  }

  updateEmployee(employee: Employee) {
    // this.dataService.getAll().pipe(value => {})
    // return this._httpClient.post();
    // TODO: Create updateEmployee method
  }
}
