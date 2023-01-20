import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticatedResponse } from 'src/app/models/api-request/login/authenticatedResponse';
import { LoginRequest } from 'src/app/models/api-request/login/loginRequest';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginRequest: LoginRequest = {
    userName: '',
    password: '',
  };

  invalidLogin: boolean = true;

  formData: FormGroup = this.formBuilder.group({
    userName: [''],
    password: [''],
  });

  constructor(
    private router: Router,
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService
  ) {}

  onClickSubmit(data: any) {
    let loginUsername = data.userName;
    let loginPassword = data.password;
    this.loginRequest = { userName: loginUsername, password: loginPassword };
    this.authService.login(this.loginRequest);
  }
}
