import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})

export class UserHttpService {

  url: string =  `${ApiConfig.url}/User`;

  constructor(private http: HttpClient) { }

  register(user: User): Observable<any>{
    return this.http.post(`${this.url}/register`, user)
  }

  login(userName: string, password: string): Observable<any>{
    return this.http.post(`${this.url}/login`, {
      "userName": userName,
      "password": password,
      "firstName": "string",
      "lastName": "string",
      "email": "user@example.com"
    })
  }

  toggleRole(userName: string, roleName: string): Observable<any> {
    return this.http.post(`${this.url}/toggle-role`,{
        userName: userName,
        roleName: roleName
    })
  }

  update(user: User): Observable<any> {
    // Zakładamy, że endpoint "/edit" nie wymaga ID w URL, jeśli wymaga, dostosuj URL
    return this.http.put(`${this.url}/edit`, user);
  }
  

}
