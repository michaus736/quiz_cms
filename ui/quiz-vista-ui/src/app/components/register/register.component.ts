// src/app/components/register/register.component.ts

import { Component } from '@angular/core';
import { UserHttpService } from 'src/app/services/http/user-http-service';
import { User } from 'src/app/models/user'; // Import interfejsu User
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData: User = { // Zdefiniuj registerData jako obiekt typu User
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    email: ''
  };

  constructor(private userHttpService: UserHttpService, private router: Router) { }

  onSubmit(): void {
    // Upewnij się, że wszystkie wymagane pola są ustawione przed wywołaniem tej metody
    this.userHttpService.register(this.registerData).subscribe(

    console.log(this.registerData)

    this.userHttpService.register(
      this.registerData.userName, 
      this.registerData.password,
      this.registerData.firstName,
      this.registerData.lastName,
      this.registerData.email).subscribe(
      response => {
        console.log('Register successful', response);
        this.router.navigate(['/login']);
      },
      error => {
        console.error('Register failed', error.error);
      }
    );
  }
}
