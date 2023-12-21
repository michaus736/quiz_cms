import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminPanelComponent } from './components/admin-panel/admin-panel.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TopMenuComponent } from './components/top-menu/top-menu.component';
import { BaseComponent } from './components/base/base.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { GuestComponent } from './components/guest/guest.component';
import { QuizezComponent } from './components/quizez/quizez.component';
import { AuthInterceptor } from './auth.interceptor';
import { SideNavComponent } from './components/side-nav/side-nav.component';

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
    SideNavComponent
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
