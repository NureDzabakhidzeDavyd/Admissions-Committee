import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { SpecialityService } from 'src/app/core/services/specialities.service';
import { DynamicFilters } from 'src/app/models/api-request/dynamic-filter/dynamicFilters';
import { Speciality } from 'src/app/models/ui-models/speciality.model';

@Component({
  selector: 'app-employees',
  templateUrl: './specialities.component.html',
  styleUrls: ['../../../../styles/table.styles.scss'],
})
export class SpecialityComponent implements OnInit {
  constructor(private specialityService: SpecialityService) {}

  specialities: Speciality[] = [];
  displayedColumns: string[] = [
    'specialityId',
    'specialityName',
    'specialityCode',
    'faculty',
    'details',
  ];
  dataSource: MatTableDataSource<Speciality> =
    new MatTableDataSource<Speciality>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;

  public getAllByFilters(dynamicFilters: DynamicFilters) {
    console.log(dynamicFilters);
    if (dynamicFilters.filters.length > 0) {
      this.specialityService.getAllByDynamicFilters(dynamicFilters).subscribe({
        next: (value) => {
          this.specialities = value;
          this.dataSource = new MatTableDataSource<Speciality>(
            this.specialities
          );
          this.dataSource.paginator = this.matPaginator;
          this.dataSource.sort = this.matSort;
          console.log(this.specialities);
        },
      });
    } else {
      this.getSpecialities();
    }
  }

  public getSpecialities() {
    this.specialityService.getAll().subscribe(
      (success) => {
        console.log(success);
        this.specialities = success;
        this.dataSource = new MatTableDataSource<Speciality>(this.specialities);

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
    this.getSpecialities();
  }
}
