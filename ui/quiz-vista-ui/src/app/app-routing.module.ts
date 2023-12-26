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
import { CategoriesComponent } from './components/moderator/categories/categories/categories.component';
import { ModeratorGuard } from './services/moderator-guard-service';
import { ModeratorComponent } from './components/moderator/moderator/moderator.component';
import { TagsComponent } from './components/moderator/tags/tags/tags.component';
import { EditCategoryComponent } from './components/moderator/categories/edit-category/edit-category.component';
import { AddCategoryComponent } from './components/moderator/categories/add-category/add-category.component';
import { AddTagComponent } from './components/moderator/tags/add-tag/add-tag.component';
import { EditTagComponent } from './components/moderator/tags/edit-tag/edit-tag.component';

const routes: Routes = [
  { path: 'home', component: GuestComponent },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard] },
  { path: 'quizez', component: QuizezComponent, canActivate:[UserGuard]},
  { path: 'admin/users', component: UsersComponent , canActivate: [AdminGuard]},
  { path: 'admin/users-roles', component: UsersRolesComponent, canActivate: [AdminGuard] },
  { path: 'admin/edit-user/:id', component:EditUserComponent, canActivate: [AdminGuard] },
  { path: 'moderator', component:ModeratorComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/categories', component:CategoriesComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/add-category', component:AddCategoryComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/edit-category/:id', component:EditCategoryComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/tags', component:TagsComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/add-tag', component:AddTagComponent, canActivate: [ModeratorGuard] },
  { path: 'moderator/edit-tag/:id', component:EditTagComponent, canActivate: [ModeratorGuard] },
  { path: 'error/:code', component: ErrorComponent },

  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Domyślna ścieżka
  { path: '**', redirectTo: '/error/404' } // Obsługa nieznanych ścieżek

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
