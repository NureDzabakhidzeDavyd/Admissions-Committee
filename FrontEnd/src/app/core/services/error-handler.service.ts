import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ErrorHandlerService {
  private errorMessage: string = '';

  public handleError(error: HttpErrorResponse) {
    switch (error.status) {
      case 404:
        this.handle404Error();
        break;
      case 500:
        this.handle500Error();
        break;
      default:
        break;
    }
  }

  private handle404Error() {
    this._router.navigate(['/404']);
  }

  private handle500Error() {
    this._router.navigate(['/500']);
  }

  private createErrorMessage(error: HttpErrorResponse) {
    this.errorMessage = error.error ? error.error : error.statusText;
  }

  constructor(private _router: Router) {}
}
