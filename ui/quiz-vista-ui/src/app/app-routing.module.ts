import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuestComponent } from './components/guest/guest.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { QuizezComponent } from './components/quizez/quizez.component';
import { UsersComponent } from './components/admin/users/users.component';
import { UsersRolesComponent } from './components/admin/users-roles/users-roles.component';
import { EditUserComponent } from './components/admin/edit-user/edit-user.component';
import { AdminGuard } from './services/admin-guard-service';
import { ErrorComponent } from './components/error/error.component';
import { UserGuard } from './services/user-guard-service';

const routes: Routes = [
  { path: 'home', component: GuestComponent },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard] },
  { path: 'quizez', component: QuizezComponent, canActivate:[UserGuard]},
  { path: 'users', component: UsersComponent , canActivate: [AdminGuard]},
  { path: 'users-roles', component: UsersRolesComponent, canActivate: [AdminGuard] },
  { path: 'edit-user/:id', component:EditUserComponent, canActivate: [AdminGuard] },
  { path: 'error/:code', component: ErrorComponent },

  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Domyślna ścieżka
  { path: '**', redirectTo: '/error/404' } // Obsługa nieznanych ścieżek

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
