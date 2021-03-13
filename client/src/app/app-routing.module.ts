import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginAdminComponent } from './login-admin/login-admin.component';
import { LoginComponent } from './login/login.component';
import { RecoverUserComponent } from './recover-user/recover-user.component';
import { RegisterComponent } from './register/register.component';
import { UserlistComponent } from './userlist/userlist.component';
import { WishlistAdminComponent } from './wishlist-admin/wishlist-admin.component';
import { WishlistComponent } from './wishlist/wishlist.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'login-admin', component: LoginAdminComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'wishlist', component: WishlistComponent },
  { path: 'wishlist-admin', component: WishlistAdminComponent },
  { path: 'userlist-admin', component: UserlistComponent },
  { path: 'recover-user-admin', component: RecoverUserComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
