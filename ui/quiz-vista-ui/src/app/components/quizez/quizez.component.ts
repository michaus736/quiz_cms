import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/services/auth-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';

@Component({
  selector: 'app-quizez',
  templateUrl: './quizez.component.html',
  styleUrls: ['./quizez.component.css']
})
export class QuizezComponent implements OnInit{
  quizzes: any[] = [];
  categoryName: string | null = '';
  tagName: string | null = '';
  message: string='';

  constructor(private quizService: QuizHttpService, private authService: AuthService, private route: ActivatedRoute){}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
    this.categoryName = params.get('categoryName');
    
    this.tagName = params.get('tagName');

    if(this.tagName != null) {
      this.quizService.getQuizByTag(this.tagName).subscribe(
        data=>{
          this.quizzes=data.model;
          console.log(data);
        },
        error=>{
          console.error("Błąd!!",error);
        }
      )
    }
    else if(this.categoryName != null) {
      this.quizService.getQuizByCategory(this.categoryName).subscribe(
        data=>{
          this.quizzes=data.model;
          console.log(data);
        },
        error=>{
          console.error("Błąd!!",error);
        }
      )
    } 
    else {
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
  )}
  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
}
