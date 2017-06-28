import { Component, OnInit } from '@angular/core';
import { FormGroup,
  FormBuilder,
  Validators} from '@angular/forms';

import { Router }      from '@angular/router';

import { AuthService } from './auth.service';
import { LoginModel } from './loginModel';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  loginForm: FormGroup;
  error = '';

  constructor(private authService: AuthService, public router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onLoginSubmitClicked(model: LoginModel) {
    this.authService.login(model.username, model.password).subscribe((result) => {
      if (this.authService.isLoggedIn == true) {
        let redirect = this.authService.redirectUrl;

        if (redirect) {
          this.router.navigate([redirect])
        }
        else {
          this.router.navigate(["home"])
        }
      }
      else {
        alert("Login Failed");


      }

    }, () => {

      this.error = 'Username or password is incorrect';

    }, () => {

      console.log("Completed");
    })
  }

}
