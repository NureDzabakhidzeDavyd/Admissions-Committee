import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AdmissionsCommitteeGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | UrlTree {
    let url: string = state.url;

    return this.checkLogin(url);
  }

  checkLogin(url: string): true | UrlTree {
    console.log('Url: ' + url);
    let val: string = localStorage.getItem('isUserLoggedIn') as string;

    if (val != null && val == 'true') {
      if (url == '/login') {
        return this.router.parseUrl('/');
      } else {
        return true;
      }
    } else {
      return this.router.parseUrl('/login');
    }
  }
}
