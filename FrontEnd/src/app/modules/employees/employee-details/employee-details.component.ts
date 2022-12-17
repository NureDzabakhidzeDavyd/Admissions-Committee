import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { EmployeesService } from 'src/app/core/services/employees.service';
import { FacultiesService } from 'src/app/core/services/faculties.service';
import { Employee } from 'src/app/models/ui-models/employee.model';
import { Faculty } from 'src/app/models/ui-models/faculty.model';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.scss'],
})
export class EmployeeDetailsComponent implements OnInit {
  employeeId: number | null | undefined;
  employee: Employee = {
    employeeId: 0,
    faculty: {
      facultyId: 0,
      facultyName: '',
    },
    person: {
      firstName: '',
      secondName: '',
      patronymic: '',
      address: '',
      birth: new Date(),
      email: '',
      phone: '',
    },
    working: [],
    careerInfo: '',
  };
  faculties: Faculty[] = [];

  onUpdate(): void {}

  FormatDate(iDate: Date) {
    var inputDate = new Date(iDate);
    var formattedDate =
      inputDate.getFullYear() +
      '-' +
      (inputDate.getMonth() + 1) +
      '-' +
      inputDate.getDate();
    return formattedDate;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (routeValue) => {
        this.employeeId = Number(routeValue.get('id'));
        console.log(this.employeeId);
        if (this.employeeId) {
          this.employeeService.getEmployeeById(this.employeeId).subscribe({
            next: (value) => {
              this.employee = value;
              console.log(this.employee);
            },
          });

          this._facultyService.getAll().subscribe({
            next: (value) => {
              this.faculties = value;
              console.log(this.faculties);
            },
          });
        }
      },
    });
  }

  constructor(
    private readonly employeeService: EmployeesService,
    private readonly route: ActivatedRoute,
    private readonly _facultyService: FacultiesService
  ) {}
}
