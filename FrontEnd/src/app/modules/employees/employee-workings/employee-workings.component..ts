import { Component, Input, OnInit } from '@angular/core';
import { WorkingsService } from 'src/app/core/services/workings.service';
import { Working } from 'src/app/models/ui-models/working.model';

@Component({
  selector: 'app-employee-workings',
  templateUrl: './employee-workings.component.html',
})
export class EmployeeWorkingsComponent implements OnInit {
  @Input() employeeId: number = 0;
  public workings: Working[] = [];

  ngOnInit(): void {
    if (this.employeeId != 0) {
      this.workingService.GetEmployeeWorkingsAsync(this.employeeId).subscribe({
        next: (value) => {
          this.workings = value;
          console.log(this.workings);
        },
      });
    }
  }

  constructor(private readonly workingService: WorkingsService) {}
}
