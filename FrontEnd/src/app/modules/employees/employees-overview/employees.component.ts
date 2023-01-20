import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeesService } from 'src/app/core/services/employees.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { Employee } from 'src/app/models/api-models/employee';
import { DynamicFilters } from 'src/app/models/api-request/dynamic-filter/dynamicFilters';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
})
export class EmployeesComponent implements OnInit {
  constructor(
    private employeeService: EmployeesService,
    private _errorhandler: ErrorHandlerService
  ) {}

  employees: Employee[] = [];
  displayedColumns: string[] = [
    'employeeId',
    'firstName',
    'secondName',
    'patronymic',
    'address',
    'birth',
    'email',
    'phone',
    'facultyName',
    'details',
  ];
  dataSource: MatTableDataSource<Employee> = new MatTableDataSource<Employee>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;

  public getAllByFilters(dynamicFilters: DynamicFilters) {
    console.log(dynamicFilters);
    if (dynamicFilters.filters.length > 0) {
      this.employeeService.getAllByDynamicFilters(dynamicFilters).subscribe({
        next: (value) => {
          this.employees = value;
          this.dataSource = new MatTableDataSource<Employee>(this.employees);
          this.dataSource.paginator = this.matPaginator;
          this.dataSource.sort = this.matSort;
          console.log(this.employees);
        },
      });
    } else {
      this.getEmployees();
    }
  }

  public getEmployees() {
    this.employeeService.getEmployees().subscribe(
      (success) => {
        console.log(success);
        this.employees = success;
        this.dataSource = new MatTableDataSource<Employee>(this.employees);

        if (this.matPaginator) {
          this.dataSource.paginator = this.matPaginator;
        }

        if (this.matSort) {
          this.dataSource.sort = this.matSort;
        }
      },
      (error: HttpErrorResponse) => {
        this._errorhandler.handleError(error);
      }
    );
  }

  ngOnInit() {
    this.getEmployees();
  }
}
