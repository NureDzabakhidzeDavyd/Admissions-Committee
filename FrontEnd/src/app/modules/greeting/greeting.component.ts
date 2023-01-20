import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-greeting',
  templateUrl: './greeting.component.html',
  styleUrls: ['./greeting.component.scss', '../../../styles.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class GreetingComponent {}
