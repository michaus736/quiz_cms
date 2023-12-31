import { Component } from '@angular/core';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { Quiz } from 'src/app/models/quiz';
import { CategoryHttpService } from 'src/app/services/http/category-http-service';
import { TagHttpService } from 'src/app/services/http/tag-http-service';
import { ActivatedRoute, Router } from '@angular/router';

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

  constructor(private quizService: QuizHttpService, private categoryService:CategoryHttpService, private tagService: TagHttpService, private router: Router) { }

  addQuiz() {
    const { id, ...quizData } = this.newQuiz; 
    this.quizService.createQuiz(quizData).subscribe(response => {
      this.router.navigate(['/moderator/add-questions/', quizData.name])
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
