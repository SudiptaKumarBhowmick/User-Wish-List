import { Input, Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private _service: AuthService, private _toastr: ToastrService) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      FirstName: new FormControl("", [Validators.required]),
      LastName: new FormControl("", [Validators.required]),
      Address: new FormControl("", [Validators.required]),
      Email: new FormControl("", [Validators.required]),
      Password: new FormControl("", [Validators.required])
    });
  }

  submit(registerFormDetails: NgForm) {
    this._service.register(registerFormDetails.value).subscribe(
      (res: any) => {
        this._toastr.success("Registration successfull", "Registration successfull");
      }
    );
  }

}
