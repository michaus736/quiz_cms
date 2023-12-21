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
    this.userHttpService.login(this.registerData.userName, this.registerData.password).subscribe(
      response => {
        console.log('Register successful', response);
      },
      error => {
        console.error('Register failed', error.error);
      }
    );
  }
}