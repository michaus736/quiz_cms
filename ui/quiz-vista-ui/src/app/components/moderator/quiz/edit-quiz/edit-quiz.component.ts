import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Quiz } from 'src/app/models/quiz';
import { ErrorHandlerService } from 'src/app/services/error-handler.service';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { TagHttpService } from 'src/app/services/http/tag-http-service';

@Component({
  selector: 'app-edit-quiz',
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css']
})
export class EditQuizComponent {
  quizName: string = ''
  quizDetails!: Quiz;
  categories: any[]=[];
  tags: any[]=[];
  backendErrorMessages: any[]=[];
  users: any[]=[];
  newUserName: string =''


  constructor(private quizService: QuizHttpService, private categoryService:CategoryHttpService, private tagService: TagHttpService, private router: Router,private route: ActivatedRoute, private errorHandlerService:ErrorHandlerService){}

  
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.quizName = params['quizName'];
      this.getQuizDetails();
    });

    this.loadCategories();
    this.loadTags();
  }


  editQuiz() {
    if (!this.quizDetails) {
      console.error('Quiz details are not loaded');
      return;
    }
    const quizData = { ...this.quizDetails }; 
    quizData.id=this.quizDetails.id;
    this.quizService.editQuiz(quizData).subscribe(response => {
      console.log("Dziala", response);
    }, error => {
      console.log(error);
    });
  }


  getQuizDetails():void{
    this.quizService.getQuizDetailsForMod(this.quizName).subscribe(
      (Data: any)=>{
        console.log(Data);
        this.quizDetails=Data.model;
        this.users=Data.model.users
      },
      (error)=>{
        console.error('Error fetching quiz details:', error);
      }
    )
  }


  loadCategories() {
    this.categoryService.showCategories().subscribe(
      (data) => {
        console.log(data);
        this.categories = data.model;
      },
      (error) => {
        console.error('Error loading categories', error);
      }
    );
  }


  loadTags() {
    this.tagService.showTags().subscribe(
      (data) => {
        this.tags = data.model;
      },
      (error) => {
        console.error('Error loading tags', error);
      }
    );
}

unAssignUser(quizName: string, userName: string): void {
  this.quizService.unAssignUser(quizName, userName).subscribe(
    response => {
      console.log("Użytkownik usunięty", response);
      this.getQuizDetails();
    },
    error => {
      console.error('Błąd przy usuwaniu użytkownika:', error);
    }
  );
}

assignUser(quizName: string, userName: string): void {
  if (!userName) {
    console.error('Nazwa użytkownika jest wymagana');
    return;
  }
  this.quizService.AssignUser(quizName, userName).subscribe(
    response => {
      console.log("Użytkownik przypisany", response);
      this.getQuizDetails();
      this.newUserName = '';
    },
    error => {
      console.error('Błąd przy przypisywaniu użytkownika:', error);
    }
  );
}
}