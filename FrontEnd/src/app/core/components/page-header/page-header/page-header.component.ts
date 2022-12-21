import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styles: [
    `
      .page-header {
        h2 {
          font-family: 'Nunito', 'sans-serif';
          font-size: 30px;
          font-weight: bold;
          margin: 0;
        }
      }
    `,
  ],
})
export class PageHeaderComponent {
  @Input() title!: string;

  constructor() {}
}
