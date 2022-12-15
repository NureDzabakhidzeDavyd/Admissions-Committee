import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { EmployeesService } from 'src/app/core/services/employees.service';
import { Employee } from 'src/app/models/ui-models/employee.mode';

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

  constructor(
    private readonly employeeService: EmployeesService,
    private readonly route: ActivatedRoute
  ) {}

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
        }
      },
    });
  }
}
