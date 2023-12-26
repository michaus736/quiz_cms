import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminPanelComponent } from './components/admin/admin-panel/admin-panel.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TopMenuComponent } from './components/top-menu/top-menu.component';
import { BaseComponent } from './components/base/base.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GuestComponent } from './components/guest/guest.component';
import { QuizezComponent } from './components/quizez/quizez.component';
import { AuthInterceptor } from './auth.interceptor';
import { SideNavComponent } from './components/side-nav/side-nav.component';
import { AdminSideNavComponent } from './components/admin/admin-side-nav/admin-side-nav.component';
import { UsersComponent } from './components/admin/users/users.component';
import { UsersRolesComponent } from './components/admin/users-roles/users-roles.component';
import { EditUserComponent } from './components/admin/edit-user/edit-user.component';
import { ErrorComponent } from './components/error/error.component';
import { ModeratorSideNavComponent } from './components/moderator/moderator-side-nav/moderator-side-nav.component';
import { CategoriesComponent } from './components/moderator/categories/categories/categories.component';
import { ModeratorComponent } from './components/moderator/moderator/moderator.component';
import { TagsComponent } from './components/moderator/tags/tags/tags.component';
import { EditCategoryComponent } from './components/moderator/categories/edit-category/edit-category.component';
import { AddCategoryComponent } from './components/moderator/categories/add-category/add-category.component';
import { AddTagComponent } from './components/moderator/tags/add-tag/add-tag.component';
import { EditTagComponent } from './components/moderator/tags/edit-tag/edit-tag.component';
import { QuizDetailsComponent } from './components/quizez/quiz-details/quiz-details.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminPanelComponent,
    TopMenuComponent,
    BaseComponent,
    LoginComponent,
    RegisterComponent,
    GuestComponent,
    QuizezComponent,
    SideNavComponent,
    AdminSideNavComponent,
    UsersComponent,
    UsersRolesComponent,
    EditUserComponent,
    ErrorComponent,
    ModeratorSideNavComponent,
    CategoriesComponent,
    ModeratorComponent,
    TagsComponent,
    EditCategoryComponent,
    AddCategoryComponent,
    AddTagComponent,
    EditTagComponent,
    QuizDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi:true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
