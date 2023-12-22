import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GuestComponent } from './components/guest/guest.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { QuizezComponent } from './components/quizez/quizez.component';
import { UsersComponent } from './components/users/users.component';
import { UsersRolesComponent } from './components/users-roles/users-roles.component';

const routes: Routes = [
  { path: 'home', component: GuestComponent },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'admin', component: AdminPanelComponent},
  { path: 'quizez', component: QuizezComponent},
  { path: 'users', component: UsersComponent},
  { path: 'users-roles', component: UsersRolesComponent},

  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Domyślna ścieżka
  { path: '**', redirectTo: '/home' } // Obsługa nieznanych ścieżek

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
