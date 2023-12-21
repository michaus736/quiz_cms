import { Component, OnInit } from '@angular/core';
import { QuizService } from 'src/app/services/http/quiz-http-service';

@Component({
  selector: 'app-quizez',
  templateUrl: './quizez.component.html',
  styleUrls: ['./quizez.component.css']
})
export class QuizezComponent implements OnInit{
  quizzes: any[] = [];
  constructor(private quizService:QuizService){}

  ngOnInit(): void {
    this.quizService.getQuiz().subscribe(
      data=>{
        this.quizzes=data.model;
        console.log(data);
      },
      error=>{
        console.error("Błąd!!",error);
      }
    )
  }
}
