import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from '../services/admin.service';

@Component({
  selector: 'app-recover-user',
  templateUrl: './recover-user.component.html',
  styleUrls: ['./recover-user.component.css']
})
export class RecoverUserComponent implements OnInit {
  users: any;

  constructor(private _service: AdminService, private _toastr: ToastrService,
    private _router: Router) { }

  ngOnInit() {
    this.getDeletedUserlistdata();
  }

  getDeletedUserlistdata() {
    this._service.getDeletedUserlistAdminData().subscribe((res: any) => {
      this.users = res;
    })
  }
  RecoverUserData(userData) {
    this._service.recoverUser(userData.id, userData).subscribe((res: any) => {
      this.getDeletedUserlistdata();
      this._toastr.success("Recovered user data successfully", "Successful");
    })
  }

  redirectToUserlist() {
    this._router.navigateByUrl('/userlist-admin');
  }

  logout() {
    localStorage.removeItem('usertoken');
    localStorage.removeItem('userid');
    localStorage.removeItem('role');
    this._router.navigateByUrl('/login');
  }

}
