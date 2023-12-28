import { Component } from '@angular/core';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { Quiz } from 'src/app/models/quiz'; // Step 2: Import the Quiz interface
import { CategoryHttpService } from 'src/app/services/http/category-http-service';
import { TagHttpService } from 'src/app/services/http/tag-http-service';

@Component({
  selector: 'app-add-quiz',
  templateUrl: './add-quiz.component.html',
  styleUrls: ['./add-quiz.component.css']
})
export class AddQuizComponent {
  newQuiz: Quiz = {
    id: '',
    name: '',
    description: '',
    categoryId: 0,
    cmsTitleStyle: '',
    isActive: true,
    attemptCount:0,
    publicAccess: true,
    tagIds: [] 
  };

  categories: any[] =[];
  tags: any[] =[];

  ngOnInit() {
    this.loadCategories();
    this.loadTags();
  }

  constructor(private quizService: QuizHttpService, private categoryService:CategoryHttpService, private tagService: TagHttpService) { }

  addQuiz() {
    const { id, ...quizData } = this.newQuiz; 
    this.quizService.createQuiz(quizData).subscribe(response => {
    }, error => {
      
    });
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
}
