import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SpecialityService } from 'src/app/core/services/specialities.service';
import { Speciality } from 'src/app/models/ui-models/speciality.model';

@Component({
  selector: 'app-employee-details',
  templateUrl: './speciality-details.component.html',
  styleUrls: ['../../../../styles/form.styles.scss'],
})
export class SpecialityDetailsComponent implements OnInit {
  specialityId: number | null | undefined;
  speciality: Speciality = {
    specialityId: 0,
    specialityName: '',
    specialityCode: 0,

    facultyId: 0,
    faculty: {
      facultyId: 0,
      facultyName: '',
    },

    educationalProgram: '',
    educationDegree: '',
    branchName: '',
    offerType: '',
    educationForm: '',
    educationCost: 0,

    seatTotal: 0,
    submittedApplicationsTotal: 0,
    budgetTotal: 0,
    contractTotal: 0,
    quota1Total: 0,
    quota2Total: 0,

    coefficients: [],
  };

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (routeValue) => {
        this.specialityId = Number(routeValue.get('id'));
        console.log(this.specialityId);
        if (this.specialityId) {
          this.specialityService.getById(this.specialityId).subscribe({
            next: (value) => {
              this.speciality = value;
              console.log(this.speciality);
            },
          });
        }
      },
    });
  }

  constructor(
    private readonly specialityService: SpecialityService,
    private readonly route: ActivatedRoute
  ) {}
}
