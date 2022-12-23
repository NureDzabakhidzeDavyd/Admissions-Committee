import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  userName: string = '';
  password: string = '';

  formData: FormGroup = this.formBuilder.group([
    { username: '' },
    { password: '' },
  ]);

  constructor(
    private authService: AuthService,
    private router: Router,
    private readonly formBuilder: FormBuilder
  ) {}

  onClickSubmit(data: any) {
    this.userName = data.userName;
    this.password = data.password;

    console.log('Login page: ' + this.userName);
    console.log('Login page: ' + this.password);

    this.authService.login(this.userName, this.password).subscribe((data) => {
      console.log('Is Login Success: ' + data);

      if (data) this.router.navigate(['/']);
    });
  }
}
