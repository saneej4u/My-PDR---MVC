import { Component, OnInit } from '@angular/core';
import { FormGroup,
    FormBuilder,
    Validators} from '@angular/forms';


import { Router }      from '@angular/router';

import { AuthService } from './auth.service';

import { RegisterBindingModel } from  './registerbindingmodel';

@Component({
    selector: 'app-account',
    templateUrl: './sign-up.component.html',
    styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

    registerForm: FormGroup;
    registerBindingModel: RegisterBindingModel;
    resultSignUp: any;
    errors: any;

    constructor(private authService: AuthService, public router: Router, private formBuilder: FormBuilder) { }



    ngOnInit() {

        this.registerForm = this.formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            email: ['', Validators.required],
            password: ['', Validators.required],
            confirmPassword: ['', Validators.required]
        });
    }


    onSubmitClicked(model: RegisterBindingModel) {
        this.authService.signUp(model).
            subscribe((res: any) => {
                this.router.navigate(["login"]);
            }, (errors) => {
               this.errors = errors._body;
            });
    }

}