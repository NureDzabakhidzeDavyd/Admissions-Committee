import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SpecialityService } from 'src/app/core/services/specialities.service';
import { Speciality } from 'src/app/models/ui-models/speciality.model';

import html2canvas from 'html2canvas';
import { jsPDF } from 'jspdf';

@Component({
  selector: 'app-employee-details',
  templateUrl: './speciality-details.component.html',
  styleUrls: [
    '../../../../styles/form.styles.scss',
    '../../../../styles/button.styles.scss',
    '../../../../styles/table-pdf.styles.scss',
  ],
})
export class SpecialityDetailsComponent implements OnInit {
  @ViewChild('print')
  content!: ElementRef;

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

  /**
   * openPdf
   */
  public printPdf() {
    var element: HTMLElement = document.getElementById('print') as HTMLElement;
    html2canvas(element).then((canvas) => {
      console.log(canvas);
      var imgData = canvas.toDataURL('img/png');

      var imgHeight = (canvas.height * 208) / canvas.width;
      var doc = new jsPDF();
      doc.addImage(imgData, 0, 0, 208, imgHeight);
      doc.save('speciality.pdf');
    });
  }

  constructor(
    private readonly specialityService: SpecialityService,
    private readonly route: ActivatedRoute
  ) {}
}
