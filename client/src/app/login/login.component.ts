import { Input, Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
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
    this._service.login(loginFormDetails.value).subscribe(
      (res: any) => {
        localStorage.setItem('usertoken', res.data.jwtToken);
        localStorage.setItem('userid', res.data.id);
        localStorage.setItem('role', res.data.roleDescription);
        this._router.navigateByUrl('/wishlist');
      },
      err => {
        this._toastr.error("Invalid username or password!", "Authentication Error");
      }
    );
  }
}
