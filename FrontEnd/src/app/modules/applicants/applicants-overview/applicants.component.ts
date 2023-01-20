import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Applicant } from '../../../models/ui-models/applicant.model';
import { ApplicantService } from '../../../core/services/applicant.service';
import { DynamicFilters } from 'src/app/models/api-request/dynamic-filter/dynamicFilters';

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

  public getAllByFilters(dynamicFilters: DynamicFilters) {
    console.log(dynamicFilters);
    if (dynamicFilters.filters.length > 0) {
      this.applicantService.getAllByDynamicFilters(dynamicFilters).subscribe({
        next: (value) => {
          this.applicants = value;
          this.dataSource = new MatTableDataSource<Applicant>(this.applicants);
          this.dataSource.paginator = this.matPaginator;
          this.dataSource.sort = this.matSort;
        },
      });
    } else {
      this.getApplicants();
    }
  }

  public getApplicants() {
    this.applicantService.getApplicants().subscribe(
      (success) => {
        console.log(success);
        this.applicants = success;
        this.dataSource = new MatTableDataSource<Applicant>(this.applicants);

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

  ngOnInit() {
    this.getApplicants();
  }
}
