import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service';
import { UsersHttpService } from 'src/app/services/http/users-http.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  users: any[] = [];
  constructor(private usersHttpService:UsersHttpService, private authService:AuthService){};

  ngOnInit(): void {
    this.usersHttpService.showUsers().subscribe(
      (data) => {
        console.log(data);
        this.users = data.model;
      },
      (error) => {
        console.error('Error fetching users', error);
      }
    );
  }
  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
}
