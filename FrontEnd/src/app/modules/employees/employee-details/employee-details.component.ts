import { DatePipe } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';
import { EmployeesService } from 'src/app/core/services/employees.service';
import { ErrorHandlerService } from 'src/app/core/services/error-handler.service';
import { FacultiesService } from 'src/app/core/services/faculties.service';
import { UpdateEmployeeRequest } from 'src/app/models/api-request/updateEmployeeRequest';
import { Employee } from 'src/app/models/ui-models/employee.model';
import { Faculty } from 'src/app/models/ui-models/faculty.model';

@Component({
  selector: 'app-employee-details',
  templateUrl: './employee-details.component.html',
  styleUrls: [
    './employee-details.component.scss',
    '../../../../styles/button.styles.scss',
    '../../../../styles/table-pdf.styles.scss',
  ],
})
export class EmployeeDetailsComponent implements OnInit {
  isAdmin: boolean = false;

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
  public get EmployeeBirth() {
    return this.datepipe.transform(this.employee.person.birth, 'yyyy-MM-dd');
  }

  ngOnInit(): void {
    let storeData = localStorage.getItem('isUserLoggedIn');
    if (storeData != null && storeData == 'true') {
      this.isAdmin = true;
    } else {
      this.isAdmin = false;
    }

    this.activatedRoute.paramMap.subscribe({
      next: (routeValue) => {
        this.employeeId = Number(routeValue.get('id'));
        console.log(this.employeeId);
        if (this.employeeId) {
          this.employeeService.getEmployeeById(this.employeeId).subscribe({
            next: (value) => {
              this.employee = value;
              console.log(this.employee);
            },
            error: (value: HttpErrorResponse) => {
              this.errorHandler.handleError(value);
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

  onDelete(): void {
    this.employeeService.deleteById(this.employee.employeeId).subscribe({
      next: (value) => {
        this.snackBar.open('Employee was deleted successfully', undefined, {
          duration: 2000,
        });

        setTimeout(() => {
          this.router.navigateByUrl('employees');
        }, 2000);
      },
    });
  }

  onUpdate(): void {
    let updateEmployee: UpdateEmployeeRequest = {
      person: {
        firstName: this.employee.person.firstName,
        secondName: this.employee.person.secondName,
        patronymic: this.employee.person.patronymic,
        address: this.employee.person.address,
        birth: this.employee.person.birth,
        email: this.employee.person.email,
        phone: this.employee.person.phone,
      },
      FacultyId: this.employee.faculty.facultyId,
      CareerInfo: this.employee.careerInfo,
    };

    this.employeeService
      .updateEmployee(this.employee.employeeId, updateEmployee)
      .subscribe({
        next: (value) => {
          this.employee = value as Employee;
          console.log(this.employee);
        },
      });

    this.snackBar.open('Employee update successfully', undefined, {
      duration: 2000,
    });
  }

  public printPdf() {
    var element: HTMLElement = document.getElementById('print') as HTMLElement;
    html2canvas(element).then((canvas) => {
      console.log(canvas);
      var imgData = canvas.toDataURL('img/png');

      var imgHeight = (canvas.height * 208) / canvas.width;
      var doc = new jsPDF();
      doc.addImage(imgData, 0, 0, 208, imgHeight);
      doc.save('employee.pdf');
    });
  }

  constructor(
    private readonly employeeService: EmployeesService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly _facultyService: FacultiesService,
    public datepipe: DatePipe,
    private readonly router: Router,
    private readonly snackBar: MatSnackBar,
    private errorHandler: ErrorHandlerService
  ) {}
}
