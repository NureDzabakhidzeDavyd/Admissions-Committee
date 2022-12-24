import { Component, Input, OnInit } from '@angular/core';
import { SpecialityService } from 'src/app/core/services/specialities.service';
import { Statistic } from 'src/app/models/ui-models/statistic.model';

@Component({
  selector: 'speciality-statistic',
  templateUrl: './speciality-statistic.component.html',
  styleUrls: ['./speciality-statistic.component.scss'],
})
export class SpecialityStatisticComponent {
  @Input()
  statistics: Statistic[] = [];

  columns = [
    {
      columnDef: 'statisticYear',
      header: 'statisticYear',
      cell: (element: Statistic) => `${element.statisticYear}`,
    },
    {
      columnDef: 'budgetMin',
      header: 'budgetMin',
      cell: (element: Statistic) => `${element.budgetMin}`,
    },
    {
      columnDef: 'budgetAver',
      header: 'budgetAver',
      cell: (element: Statistic) => `${element.budgetAver}`,
    },
    {
      columnDef: 'contractMin',
      header: 'contractMin',
      cell: (element: Statistic) => `${element.contractMin}`,
    },
    {
      columnDef: 'contractAver',
      header: 'contractAver',
      cell: (element: Statistic) => `${element.contractAver}`,
    },
  ];
  dataSource = this.statistics;
  displayedColumns = this.columns.map((c) => c.columnDef);

  constructor() {}
}
