import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { AuthenticatedResponse } from 'src/app/models/api-request/login/authenticatedResponse';
import { LoginRequest } from 'src/app/models/api-request/login/loginRequest';
import { JWT, REFRESHTOKEN } from '../constans/auth';
import { DataService } from './data.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends DataService {
  isUserLoggedIn: boolean = false;

  login(loginRequest: LoginRequest) {
    this._httpClient
      .post<AuthenticatedResponse>(`${this.getUrl()}/login`, loginRequest, {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      })
      .subscribe({
        next: (response: AuthenticatedResponse) => {
          const token = response.token;
          localStorage.setItem(JWT, token);

          const refreshToken = response.refreshToken;
          localStorage.setItem(REFRESHTOKEN, refreshToken);

          this.isUserLoggedIn = false;
          this.router.navigate(['/']);
        },
        error: (err: HttpErrorResponse) => {
          this.isUserLoggedIn = true;
          this.snackBar.open('Enter data are invalid!!!', undefined, {
            duration: 2000,
          });
        },
      });
  }

  logout(): void {
    this.isUserLoggedIn = false;
    localStorage.removeItem(JWT);
    localStorage.removeItem(REFRESHTOKEN);
  }

  constructor(
    httpClient: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    super(httpClient);
    this.entityType = 'auth';
  }
}
