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

  constructor(private quizService: QuizHttpService, private authService: AuthService, private route: ActivatedRoute){}

  ngOnInit(): void {

    this.categoryName = this.route.snapshot.paramMap.get('categoryName');
    
    // Pobierz wartość parametru 'tagName' z URL
    this.tagName = this.route.snapshot.paramMap.get('tagName');

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

  IsUserLogged() {
    return this.authService.isUserLoggedIn(); 
  }
}
