import { Component } from '@angular/core';
import { UserHttpService } from 'src/app/services/http/user-http-service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginData={
    userName: '',
    password: ''
  };

  constructor(private userHttpService: UserHttpService,
    private router: Router,
    private authService: AuthService) { }

  onSubmit(): void {
    this.userHttpService.login(this.loginData.userName, this.loginData.password).subscribe(
      response => {
        console.log('Login successful', response);
        this.authService.login(response.model.token)
        this.router.navigate(['/quizez']);
      },
      error => {
        console.error('Login failed', error.error);
      }
    );
  }




}
