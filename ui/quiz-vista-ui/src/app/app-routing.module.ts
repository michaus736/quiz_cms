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
import { QuizDetailsComponent } from './components/quizez/quiz-details/quiz-details.component';

const routes: Routes = [
  { path: 'home', component: GuestComponent },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'admin', component: AdminPanelComponent},
  { path: 'quizez', component: QuizezComponent},
  { path: 'quiz-details/:quizName', component: QuizDetailsComponent},
  { path: 'users', component: UsersComponent},
  { path: 'users-roles', component: UsersRolesComponent},
  { path: 'edit-user/:id', component: EditUserComponent},


  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Domyślna ścieżka
  { path: '**', redirectTo: '/home' } // Obsługa nieznanych ścieżek

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
