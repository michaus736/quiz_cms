import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiConfig } from '../../config/api-config';
import { Quiz } from 'src/app/models/quiz';

@Injectable({
  providedIn: 'root'
})

export class QuizHttpService {

  url: string =  `${ApiConfig.url}/Quiz`;

  constructor(private http: HttpClient) { }


  
  getQuiz(): Observable<any> {
    return this.http.get(`${this.url}/User`);
  }

  getQuizezForMod(): Observable<any> {
    return this.http.get(`${this.url}/Moderator`);
  }


  getQuizDetails(quizName: string): Observable<any> {
    return this.http.get(`${this.url}/details?quizName=${quizName}`)
  }
  getQuizRunQusetions(quizName: string):Observable<any> {
    return this.http.get(`${this.url}/quiz-run?quizName=${quizName}`)
  }

  createQuiz(quiz: Quiz): Observable<any>{
    return this.http.post(`${this.url}/create`,quiz);
  }

  getQuizModQuestions(quizName: string):Observable<any>{
    return this.http.get(`${this.url}/get-questions-mod?quizName=${quizName}`)
  }

  deleteQuiz(quizId:string):Observable<any>{
    return this.http.delete(`${this.url}/delete/${quizId}`)
  }
}
