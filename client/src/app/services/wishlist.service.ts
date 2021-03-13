import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  readonly BaseURI = 'https://localhost:44302/api';

  constructor(private _http: HttpClient) { }

  getData(id) {
    var userId = new HttpParams().set('id', id);
    return this._http.get(this.BaseURI + '/Wishlist', { params: userId }); 
  }

  postData(formData) {
    var userId = localStorage.getItem('userid');
    var wishListData = {
      ItemName: formData.itemName,
      ItemDescription: formData.itemDescription,
      WebLink: formData.webLink,
      UserId: userId
    };
    return this._http.post(this.BaseURI + '/Wishlist', wishListData);
  }

  putData(id, formData) {
    var wishListData = {
      ItemName: formData.itemName,
      ItemDescription: formData.itemDescription,
      WebLink: formData.webLink,
      UserId: id
    };
    return this._http.put(this.BaseURI + '/Wishlist/' + id, wishListData);
  }

  deleteData(id) {
    return this._http.delete(this.BaseURI + '/Wishlist/' + id);
  }
}
