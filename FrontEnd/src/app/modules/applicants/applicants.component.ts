import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Applicant } from '../../models/ui-models/applicant.model';
import { ApplicantService } from '../../core/services/applicant.service';
import { DynamicFilters } from '../../models/api-models/filters/dynamicFilters';
import { DynamicFilter } from '../../models/api-models/filters/dynamicFilter';

@Component({
  selector: 'app-applicants',
  templateUrl: './applicants.component.html',
  styleUrls: ['./applicants.component.scss'],
})
export class ApplicantsComponent implements OnInit {
  constructor(private applicantService: ApplicantService) {}

  applicants: Applicant[] = [];
  displayedColumns: string[] = [
    'applicantId',
    'firstName',
    'secondName',
    'patronymic',
    'address',
    'birth',
    'email',
    'phone',
    'certificate',
  ];
  dataSource: MatTableDataSource<Applicant> =
    new MatTableDataSource<Applicant>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;

  getApplicantFilter() {
    this.applicantService
      .getApplicantsByFilters({
        filters: [{ fieldName: 'firstName', Value: 'Микита', FieldType: 1 }],
      })
      .subscribe((success) => {
        console.log(success);
        this.applicants = success;
        this.dataSource = new MatTableDataSource<Applicant>(this.applicants);
      });
  }

  ngOnInit() {
    // Fetch students
    this.applicantService.getApplicants().subscribe(
      (success) => {
        console.log(success);
        this.applicants = success;
        this.dataSource = new MatTableDataSource<Applicant>(this.applicants);
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
