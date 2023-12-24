import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserHttpService } from 'src/app/services/http/user-http-service';
import { UsersHttpService } from 'src/app/services/http/users-http.service';


@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {
  user!: User;
  userId: string = '1';
  updateSuccess = false;

  constructor(private usersHttpService: UsersHttpService, private userHttpService: UserHttpService) {}

  ngOnInit(): void {
    this.getUserData();
  }

  getUserData(): void {
    this.usersHttpService.showUser(this.userId).subscribe(
      (userData: any) => {
        console.log(userData);
        this.user = userData.model;
      },
      (error) => {
        console.error('Error fetching user:', error);
      }
    );
  }

  updateUser(): void {
    this.userHttpService.update(this.user).subscribe(
      response => {
        console.log('User updated successfully', response);
        this.updateSuccess = true;
        setTimeout(() => this.updateSuccess = false, 5000);
      },
      error => {
        console.error('Error updating user', error);
      }
    );
  }
}