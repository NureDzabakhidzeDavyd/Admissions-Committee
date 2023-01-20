import { ElementRef, Injectable } from '@angular/core';
import { jsPDF } from 'jspdf';

@Injectable({
  providedIn: 'root',
})
export class PdfPrintService {
  constructor() {}

  public SavePDF(element: ElementRef): void {
    let pWidth = window.innerWidth; // 595.28 is the width of a4
    let srcWidth = element.nativeElement.scrollWidth + 50;
    let margin = 18; // narrow margin - 1.27 cm (36);
    let scale = (pWidth - margin * 2) / srcWidth;

    let pdf = new jsPDF('p', 'pt', 'a4');

    pdf.setFontSize(18);
    pdf.text('My PDF Table', 11, 8);
    pdf.setFontSize(11);
    pdf.setTextColor(100);

    pdf.html(element.nativeElement, {
      x: margin,
      y: margin,
      html2canvas: {
        scale: 595.28 / srcWidth,
      },
      callback: (pdf) => {
        window.open(pdf.output('bloburl'));
      },
    });
  }
}
