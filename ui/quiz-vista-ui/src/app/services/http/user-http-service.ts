import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';

@Injectable({
  providedIn: 'root'
})

export class UserHttpService {

  url: string =  `${ApiConfig.url}/User`;

  constructor(private http: HttpClient) { }

  register(userName: string, password: string, firstName: string, lastName: string, email: string): Observable<any>{
    return this.http.post(`${this.url}/register`, {
      "userName": userName,
      "password": password,
      "firstName": firstName,
      "lastName": lastName,
      "email": email
    })
  }

  login(userName: string, password: string): Observable<any>{
    return this.http.post(`${this.url}/login`, {
      "userName": userName,
      "password": password,
      "firstName": "string",
      "lastName": "string",
      "email": "string"
    })
  }



}
