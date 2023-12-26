import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';

@Injectable({
  providedIn: 'root'
})

export class QuizHttpService {

  url: string =  `${ApiConfig.url}/Quiz`;

  constructor(private http: HttpClient) { }


  
  getQuiz(): Observable<any> {
    return this.http.get(`${this.url}/User`);
  }

getQuizDetails(quizName: string): Observable<any> {
  return this.http.get(`${this.url}/details?quizName=${quizName}`)
}

}
