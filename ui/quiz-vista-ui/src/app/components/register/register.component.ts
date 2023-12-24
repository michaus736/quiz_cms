import { Component } from '@angular/core';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData={
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
    email: ''
  };

  constructor(private userHttpService: UserHttpService) { }

  onSubmit(): void {

    console.log(this.registerData)

    this.userHttpService.register(
      this.registerData.userName, 
      this.registerData.password,
      this.registerData.firstName,
      this.registerData.lastName,
      this.registerData.email).subscribe(
      response => {
        console.log('Register successful', response);
      },
      error => {
        console.error('Register failed', error.error);
      }
    );
  }
}