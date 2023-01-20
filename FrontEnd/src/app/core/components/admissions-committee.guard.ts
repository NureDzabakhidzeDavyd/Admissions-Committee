import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthenticatedResponse } from 'src/app/models/api-request/login/authenticatedResponse';
import { JWT, REFRESHTOKEN } from '../constans/auth';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AdmissionsCommitteeGuard implements CanActivate {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private httpClient: HttpClient
  ) {}

  async canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean | UrlTree> {
    const token = localStorage.getItem(JWT);
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      console.log(JSON.stringify(this.jwtHelper.decodeToken(token)));
      return true;
    }

    const isRefreshSuccess = await this.tryRefreshingTokens(token as string);
    if (!isRefreshSuccess) {
      this.router.navigate(['login']);
    }
    return isRefreshSuccess;
  }

  private async tryRefreshingTokens(token: string): Promise<boolean> {
    const refreshToken: string = localStorage.getItem(REFRESHTOKEN) as string;
    if (!token || !refreshToken) {
      return false;
    }

    const credentials = JSON.stringify({
      accessToken: token,
      refreshToken: refreshToken,
    });
    let isRefreshSuccess: boolean;
    const refreshRes = await new Promise<AuthenticatedResponse>(
      (resolve, reject) => {
        this.httpClient
          .post<AuthenticatedResponse>(
            'https://localhost:7151/auth/refresh',
            credentials,
            {
              headers: new HttpHeaders({
                'Content-Type': 'application/json',
              }),
            }
          )
          .subscribe({
            next: (res: AuthenticatedResponse) => resolve(res),
            error: (_) => {
              reject;
              isRefreshSuccess = false;
            },
          });
      }
    );
    localStorage.setItem(JWT, refreshRes.token);
    localStorage.setItem(REFRESHTOKEN, refreshRes.refreshToken);
    isRefreshSuccess = true;
    return isRefreshSuccess;
  }

  checkLogin(url: string): true | UrlTree {
    console.log('Url: ' + url);
    let val: string = localStorage.getItem('isUserLoggedIn') as string;

    if (val != null && val == 'true') {
      if (url == '/login') this.router.parseUrl('/');
      else return true;
    }

    return this.router.parseUrl('/login');
  }
}
