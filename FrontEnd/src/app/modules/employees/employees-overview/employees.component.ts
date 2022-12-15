import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeesService } from 'src/app/core/services/employees.service';
import { Employee } from 'src/app/models/api-models/employee';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
})
export class EmployeesComponent implements OnInit {
  constructor(private employeeService: EmployeesService) {}

  applicants: Employee[] = [];
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

  ngOnInit() {
    // Fetch students
    this.employeeService.getEmployees().subscribe(
      (success) => {
        console.log(success);
        this.applicants = success;
        this.dataSource = new MatTableDataSource<Employee>(this.applicants);
        //   this.getApplicantFilter();

        if (this.matPaginator) {
          this.dataSource.paginator = this.matPaginator;
        }

        if (this.matSort) {
          this.dataSource.sort = this.matSort;
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
