import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service';
import { UserHttpService } from 'src/app/services/http/user-http-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  users: any[] = [];
  filteredUsers: any[] = [];
  searchTerm: string = '';
  constructor(private userHttpService:UserHttpService, private authService:AuthService){};

  ngOnInit(): void {
    this.loadUsers();
  }

private loadUsers():void{
  this.userHttpService.showUsers().subscribe(
    (data) => {
      console.log(data);
      this.users = data.model;
      this.filterUsers();
    },
    (error) => {
      console.error('Error fetching users', error);
    }
  );
}

filterUsers(): void {
  if (!this.searchTerm) {
    this.filteredUsers = this.users;
  } else {
    this.filteredUsers = this.users.filter(user =>
      user.userName.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}

  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
}
