import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
  public isUserLoggedIn: boolean;

  public get getUserLoggedIn(): boolean {
    let val: string = localStorage.getItem('isUserLoggedIn') as string;

    if (val != null && val == 'true') {
      return true;
    }

    return false;
  }

  constructor() {
    let storeData = localStorage.getItem('isUserLoggedIn');
    console.log('StoreData: ' + storeData);
    if (storeData != null && storeData == 'true') {
      this.isUserLoggedIn = true;
    } else {
      this.isUserLoggedIn = false;
    }
  }
}
