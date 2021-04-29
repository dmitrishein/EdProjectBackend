import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {UserForRegistration} from './../../_interfaces/user/userForRegistration';
import {AuthenticationService} from '../../shared/services/authentication.service';
import { PasswordConfirmationValidatorService } from 'src/app/shared/services/password-confirmation-validator.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})


export class RegisterUserComponent implements OnInit {
  public registerForm: FormGroup;
  constructor(private _authService: AuthenticationService) { }
  ngOnInit(): void {
    this.registerForm = new FormGroup({
      username: new FormControl(''),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('')
    });
  }
  public validateControl = (controlName: string) => {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }
  public hasError = (controlName: string, errorName: string) => {
    return this.registerForm.controls[controlName].hasError(errorName)
  }
  public registerUser = (registerFormValue) => {
    const formValues = { ...registerFormValue };
    const user: UserForRegistration = {
      username: formValues.username,
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      confirmPassword: formValues.confirm
    };
    this._authService.registerUser("Account/Registration", user)
    .subscribe(_ => {
      console.log("Successful registration");
    },
    error => {
      console.log(error.error.errors);
    })
  }
}

   
