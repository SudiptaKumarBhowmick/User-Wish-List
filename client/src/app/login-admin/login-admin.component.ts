import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-admin',
  templateUrl: './login-admin.component.html',
  styleUrls: ['./login-admin.component.css']
})
export class LoginAdminComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private _service: AuthService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit() {
    this.loginForm = new FormGroup({
      Email: new FormControl("", [Validators.required]),
      Password: new FormControl("", [Validators.required])
    });
  }

  submit(loginFormDetails: NgForm) {
    this._service.loginAdmin(loginFormDetails.value).subscribe(
      (res: any) => {
        localStorage.setItem('usertoken', res.data.jwtToken);
        localStorage.setItem('userid', res.data.id);
        localStorage.setItem('role', res.data.roleDescription);
        this._router.navigateByUrl('/wishlist-admin');
      },
      err => {
        this._toastr.error("Invalid username or password!", "Authentication Error");
      }
    );
  }
}
