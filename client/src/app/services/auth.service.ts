import { Injectable } from '@angular/core';
import { FormBuilder} from '@angular/forms';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly BaseURI = 'https://localhost:44302/api';

  constructor(private _formBuilder: FormBuilder, private _http: HttpClient) { }

  register(formData) {
    return this._http.post(this.BaseURI + '/Auth/register', formData);
  }

  login(formData) {
    return this._http.post(this.BaseURI + '/Auth/login-user', formData);
  }

  loginAdmin(formData) {
    return this._http.post(this.BaseURI + '/Auth/login-admin', formData);
  }
}
