import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from '../../config/api-config';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersHttpService {
  url: string =  `${ApiConfig.url}/User/showusers`;

  constructor(private http:HttpClient) { }

  showUsers():Observable<any>{
    return this.http.get(`${this.url}`);
  }
}
