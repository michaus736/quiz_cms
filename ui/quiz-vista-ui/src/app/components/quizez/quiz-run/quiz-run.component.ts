import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';
import { QuizRun } from 'src/app/models/quiz-run/quiz-run';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { QuestionRun } from '../../../models/quiz-run/question-run';
import { AnswerRun } from 'src/app/models/quiz-run/answer-run';

@Component({
  selector: 'app-quiz-run',
  templateUrl: './quiz-run.component.html',
  styleUrls: ['./quiz-run.component.css']
})
export class QuizRunComponent {
  quizName: string = '';
  quizData!: QuizRun
  
  selectedAnswers: { [key: string]: any } = {}; // Przechowuje wybrane odpowiedzi przez użytkownika
  isFormInvalid: boolean = true;


  constructor(private route: ActivatedRoute, private quizHttpService: QuizHttpService, private formBuilder: FormBuilder
    ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.quizName = params.get('quizName') ?? '';
        return this.quizHttpService.getQuizRunQusetions(this.quizName);
      })
    )
    .subscribe(
      response => {
        this.quizData = response;
        console.log(this.quizData);
        this.initializeSelectedQuestions()

      },
      error => {
        console.error('Error fetching quiz questions:', error);
      }
    );
  }
  initializeSelectedQuestions() {
    this.quizData.model.questions.forEach((question:QuestionRun) => {
      if(question.type === '1'){
        this.selectedAnswers[question.id] = null;
      }
      else if(question.type === '2'){
        question.answers.forEach((answer: AnswerRun) => {
          this.selectedAnswers[question.id + '-' + answer.id] = false
        })
      }
    })
    console.log("initialized answers: ", this.selectedAnswers)
  }
  
  checkFormValidity(): void {
    console.log("changed answers: ", this.selectedAnswers)

    for(const key in this.selectedAnswers){
      if(key.includes('-')){
        const[questionId, answerId] = key.split('-')

      }
      else{
        const value = this.selectedAnswers[key]
        if(value == null){
          this.isFormInvalid = true
          return
        }
      }
    }

    console.log(this.isFormInvalid)
    this.isFormInvalid = true;
  }


  
  onSubmit(){

    this.isFormInvalid = true;


    const userAnswers = [];
    for (const key in this.selectedAnswers) {
      if (this.selectedAnswers.hasOwnProperty(key)) {
        if (this.selectedAnswers[key]) {
          if(key.includes('-')){
            const [questionId, answerId] = key.split('-')
            userAnswers.push(answerId)
          }
          else userAnswers.push(key);
        }
      }
    }

    // userAnswers zawiera teraz informacje o wybranych odpowiedziach przez użytkownika
    console.log('Wybrane odpowiedzi przez użytkownika:', userAnswers);
  }



}
