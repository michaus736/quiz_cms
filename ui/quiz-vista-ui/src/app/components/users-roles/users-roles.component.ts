import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service';
import { UserHttpService } from 'src/app/services/http/user-http-service';
import { UsersHttpService } from 'src/app/services/http/users-http.service';

@Component({
  selector: 'app-users-roles',
  templateUrl: './users-roles.component.html',
  styleUrls: ['./users-roles.component.css']
})
export class UsersRolesComponent {
  users: any[] = [];
  constructor(private usersHttpService:UsersHttpService, private authService:AuthService, private userHttpService:UserHttpService){};

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
  hasRole(roles: any[], roleName: string): string {
    const role = roles.find(r => r.name === roleName);
    return role ? 'Tak' : 'Nie';
}

toggleRole(userName: string, roleName: string): void {
  this.userHttpService.toggleRole(userName, roleName).subscribe(
    response => {
      console.log(response);
      this.refreshUsers();

    },
    error => {
      console.error('Error toggling role', error);
    }
  );
}

private refreshUsers(): void {
  this.usersHttpService.showUsers().subscribe(
    (data) => {
      this.users = data.model;
    },
    (error) => {
      console.error('Error refreshing users', error);
    }
  );
}
}
