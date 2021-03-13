import { Input, Component, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent implements OnInit {
  EventValue: any = "Save";
  users: any;
  UserlistForm: FormGroup;

  constructor(private _service: AdminService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit() {
    this.getUserlistdata();

    this.UserlistForm = new FormGroup({
      id: new FormControl("", [Validators.required]),
      firstName: new FormControl("", [Validators.required]),
      lastName: new FormControl("", [Validators.required]),
      address: new FormControl("", [Validators.required]),
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required])
    });
  }

  getUserlistdata() {
    this._service.getUserlistAdminData().subscribe((res: any) => {
      this.users = res;
    })
  }

  Save() {
    this._service.postUserData(this.UserlistForm.value).subscribe((data: any) => {
      this.resetFrom();
      this._toastr.success("Saved Successfully", "Saved");
    })
  }

  Update() {
    this._service.putUserData(this.UserlistForm.value.id, this.UserlistForm.value).subscribe((data: any) => {
      this.resetFrom();
    })
  }

  EditWishlistData(Data) {
    this.UserlistForm.controls["id"].setValue(Data.id);
    this.UserlistForm.controls["firstName"].setValue(Data.firstName);
    this.UserlistForm.controls["lastName"].setValue(Data.lastName);
    this.UserlistForm.controls["address"].setValue(Data.address);
    this.UserlistForm.controls["email"].setValue(Data.email);
    this.EventValue = "Update";
  }

  DeleteWishlistData(id) {
    this._service.deleteUserData(id).subscribe((data: any) => {
      this.getUserlistdata();
    })
  }

  resetFrom() {
    this.getUserlistdata();
    this.UserlistForm.reset();
    this.EventValue = "Save";
  }

  logout() {
    localStorage.removeItem('usertoken');
    localStorage.removeItem('userid');
    localStorage.removeItem('role');
    this._router.navigateByUrl('/login');
  }

  redirectToWishlist() {
    this._router.navigateByUrl('/wishlist-admin');
  }

  redirectToRecoverUser() {
    this._router.navigateByUrl('/recover-user-admin');
  }

}
