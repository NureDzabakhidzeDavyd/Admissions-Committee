import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JWT } from './core/constans/auth';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent implements OnInit {
  public isUserLoggedIn: boolean = false;

  public get getUserLoggedIn() {
    let jwt = localStorage.getItem(JWT);
    if (jwt && !this.jwtHelper.isTokenExpired(jwt)) {
      this.isUserLoggedIn = true;
    } else {
      this.isUserLoggedIn = false;
    }
    return this.isUserLoggedIn;
  }

  constructor(private jwtHelper: JwtHelperService) {}

  ngOnInit(): void {}
}
