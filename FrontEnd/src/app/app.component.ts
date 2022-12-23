import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isUserLoggedIn = false;

  ngOnInit() {
    let storeData = localStorage.getItem('isUserLoggedIn');
    console.log('StoreData: ' + storeData);
    if (storeData != null && storeData == 'true') {
      this.isUserLoggedIn = true;
    } else {
      this.isUserLoggedIn = false;
    }
  }
}
