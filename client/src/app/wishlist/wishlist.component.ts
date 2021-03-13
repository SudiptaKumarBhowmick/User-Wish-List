import { Component, OnInit } from '@angular/core';
import { WishlistService } from '../services/wishlist.service';
import { FormGroup, FormControl, Validators } from '@angular/forms'; 
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  wishLists: any;
  WishlistForm: FormGroup;
  EventValue: any = "Save";

  constructor(private _service: WishlistService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit() {
    this.getWishlistdata();

    this.WishlistForm = new FormGroup({
      id: new FormControl(null),
      itemName: new FormControl("", [Validators.required]),
      itemDescription: new FormControl("", [Validators.required]),
      webLink: new FormControl("", [Validators.required])
    })
  }

  getWishlistdata() {
    var userId = localStorage.getItem('userid');
    this._service.getData(userId).subscribe((res: any) => {
      this.wishLists = res;
    })
  }

  Save() {
    this._service.postData(this.WishlistForm.value).subscribe((data: any) => {
      this.resetFrom();
      this._toastr.success("Saved Successfully", "Saved");
    })
  }

  Update() {
    this._service.putData(this.WishlistForm.value.id, this.WishlistForm.value).subscribe((data: any) => {
      this.resetFrom();
    })
  } 

  EditWishlistData(Data) {
    this.WishlistForm.controls["id"].setValue(Data.id);
    this.WishlistForm.controls["itemName"].setValue(Data.itemName);
    this.WishlistForm.controls["itemDescription"].setValue(Data.itemDescription);
    this.WishlistForm.controls["webLink"].setValue(Data.webLink);
    this.EventValue = "Update";
  }

  DeleteWishlistData(id) {
    this._service.deleteData(id).subscribe((data: any) => {
      this.getWishlistdata();
    })
  } 

  resetFrom() {
    this.getWishlistdata();
    this.WishlistForm.reset();
    this.EventValue = "Save";
  }

  logout() {
    localStorage.removeItem('usertoken');
    localStorage.removeItem('userid');
    localStorage.removeItem('role');
    this._router.navigateByUrl('/login');
  }
}
