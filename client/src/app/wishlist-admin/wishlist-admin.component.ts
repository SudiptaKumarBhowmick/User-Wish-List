import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-wishlist-admin',
  templateUrl: './wishlist-admin.component.html',
  styleUrls: ['./wishlist-admin.component.css']
})
export class WishlistAdminComponent implements OnInit {
  wishLists: any;
  WishlistAdminForm: FormGroup;
  EventValue: any = "Save";

  constructor(private _service: AdminService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit() {
    this.getWishlistdata();
    this._service.getUserlistData();

    this.WishlistAdminForm = new FormGroup({
      userWishlistId: new FormControl(null),
      itemName: new FormControl("", [Validators.required]),
      itemDescription: new FormControl("", [Validators.required]),
      webLink: new FormControl("", [Validators.required]),
      User: new FormControl("", [Validators.required])
    })
  }

  getWishlistdata() {
    this._service.getWishlistData().subscribe((res: any) => {
      this.wishLists = res;
    })
  }

  Save() {
    console.log(this.WishlistAdminForm.value);
    this._service.postWishlistData(this.WishlistAdminForm.value).subscribe((data: any) => {
      this.resetFrom();
      this._toastr.success("Saved Successfully", "Saved");
    })
  }

  Update() {
    this._service.putWishlistData(this.WishlistAdminForm.value.User, this.WishlistAdminForm.value).subscribe((data: any) => {
      this.resetFrom();
    })
  }

  EditWishlistData(Data) {
    this.WishlistAdminForm.controls["userWishlistId"].setValue(Data.userWishlistId);
    this.WishlistAdminForm.controls["itemName"].setValue(Data.itemName);
    this.WishlistAdminForm.controls["itemDescription"].setValue(Data.itemDescription);
    this.WishlistAdminForm.controls["webLink"].setValue(Data.webLink);
    this.WishlistAdminForm.controls["User"].setValue(Data.userId);
    this.EventValue = "Update";
  }

  DeleteWishlistData(id) {
    this._service.deleteWishlistData(id).subscribe((data: any) => {
      this.getWishlistdata();
    })
  }

  resetFrom() {
    this.getWishlistdata();
    this.WishlistAdminForm.reset();
    this.EventValue = "Save";
  }

  logout() {
    localStorage.removeItem('usertoken');
    localStorage.removeItem('userid');
    localStorage.removeItem('role');
    this._router.navigateByUrl('/login');
  }

  redirectToUserlist() {
    this._router.navigateByUrl('/userlist-admin');
  }

}
