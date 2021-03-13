import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Userlist } from '../models/userlist.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  readonly BaseURI = 'https://localhost:44302/api';
  user_list: Userlist[];

  constructor(private _http: HttpClient) { }

  //wishlist admin
  getWishlistData() {
    return this._http.get(this.BaseURI + '/WishlistAdmin/user-wishlist');
  }

  getUserlistData() {
    this._http.get(this.BaseURI + '/WishlistAdmin/user-list')
      .toPromise()
      .then(res => {
        this.user_list = res as Userlist[]
      });
  }

  postWishlistData(formData) {
    var userId = localStorage.getItem('userid');
    var wishListData = {
      ItemName: formData.itemName,
      ItemDescription: formData.itemDescription,
      WebLink: formData.webLink,
      UserId: userId
    };
    return this._http.post(this.BaseURI + '/WishlistAdmin', wishListData);
  }

  putWishlistData(id, formData) {
    var wishListData = {
      ItemName: formData.itemName,
      ItemDescription: formData.itemDescription,
      WebLink: formData.webLink,
      UserId: id
    };
    return this._http.put(this.BaseURI + '/WishlistAdmin/' + id, wishListData);
  }

  deleteWishlistData(id) {
    return this._http.delete(this.BaseURI + '/WishlistAdmin/' + id);
  }

  //user list admin
  getUserlistAdminData() {
    return this._http.get(this.BaseURI + '/UserlistAdmin/user-list');
  }

  postUserData(formData) {
    var userData = {
      FirstName: formData.firstName,
      LastName: formData.lastName,
      Address: formData.address,
      Email: formData.email,
      Password: formData.password,
    };
    return this._http.post(this.BaseURI + '/UserlistAdmin/add-user', userData);
  }

  putUserData(id, formData) {
    var userData = {
      FirstName: formData.firstName,
      LastName: formData.lastName,
      Address: formData.address,
      Email: formData.email,
      Password: formData.password,
    };
    return this._http.put(this.BaseURI + '/UserlistAdmin/update-user/' + id, userData);
  }

  deleteUserData(id) {
    return this._http.delete(this.BaseURI + '/UserlistAdmin/' + id);
  }

  //recover user list admin
  getDeletedUserlistAdminData() {
    return this._http.get(this.BaseURI + '/UserlistAdmin/deleted-user-list');
  }

  recoverUser(id, userData) {
    var userDetails = {
      FirstName: userData.firstName,
      LastName: userData.lastName,
      Address: userData.address,
      Email: userData.email,
      Password: userData.password,
    };
    return this._http.put(this.BaseURI + '/UserlistAdmin/recover-user/' + id, userDetails);
  }
}
