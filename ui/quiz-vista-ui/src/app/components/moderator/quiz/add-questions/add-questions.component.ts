import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Question } from 'src/app/models/question';
import { Answer } from 'src/app/models/answer';
import { QuestionHttpService } from 'src/app/services/http/question-http-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';


@Component({
  selector: 'app-add-questions',
  templateUrl: './add-questions.component.html',
  styleUrls: ['./add-questions.component.css']
})
export class AddQuestionsComponent {
  questions: Question[] = [];
  quizName: string = ''
  quizDetails: any;
  quizWithQuestions: any;
  isNew?: true;

  constructor(private route: ActivatedRoute,private quizHttpService: QuizHttpService, private questionHttpService: QuestionHttpService) { }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      this.quizName = params.get('quizName') ?? '';
    })

    this.getQuizDetails(this.quizName)
    this.getQuestionsForQuiz(this.quizName)
  }

  onSubmit() {
    if (!this.isFormValid()) {
      alert('Each question must have at least one answer.');
      return;
    }

    console.log(this.questions);
    if (this.quizDetails) {
      this.questions.forEach(question => {
        question.quizId = this.quizDetails.id; 

        this.questionHttpService.createQuestion(question).subscribe(
          response => {
            console.log('Pytanie dodane:', response);
          },
          error => {
            console.error('Wystąpił błąd przy dodawaniu pytania:', error);
          }
        );
      });
    }
  }


  addQuestion() {
    const newQuestion: Question = {
      id: 0,
      text: '',
      type: '1',
      additionalValue: 1,
      substractionalValue: 0,
      quizId: 0, 
      cmsTitleStyle: '',
      cmsQuestionsStyle: '',
      answers: []
    };

    this.questions.push(newQuestion);
  }

  addAnswer(question: Question) {
    const newAnswer: Answer = {
      id: 0, 
      questionId: question.id, 
      answerText: '',
      isCorrect: false
    };
    question.answers.push(newAnswer);
  }

  
  getQuizDetails(quizName: string){
    this.quizHttpService.getQuizDetails(quizName).subscribe(res=>{
      this.quizDetails = res.model
    })
}

getQuestionsForQuiz(quizName: string) {
  this.quizHttpService.getQuizModQuestions(quizName).subscribe(res => {
    this.quizWithQuestions = res.model;
    console.log(this.quizWithQuestions);

    if (this.quizWithQuestions && this.quizWithQuestions.questions) {
      this.questions = this.quizWithQuestions.questions.map((question: any) => {
        return {
          id: question.id,
          text: question.text,
          type: question.type,
          additionalValue: question.additionalValue,
          substractionalValue: question.substractionalValue,
          cmsTitleStyle: question.cmsTitleValue, 
          cmsQuestionsStyle: question.cmsQuestionsValue,
          answers: question.answers.map((answer: any) => {
            return {
              id: answer.id,
              questionId: question.id,
              answerText: answer.text,
              isCorrect: answer.isCorrect
            };
          })
        };
      });
    }
  });
}


onQuestionTypeChange(question: Question, index: number) {
  question.answers = [];

  if (question.type === '2') {
    question.answers.push({ id: 0, questionId: question.id, answerText: 'Prawda', isCorrect: false });
    question.answers.push({ id: 0, questionId: question.id, answerText: 'Fałsz', isCorrect: false });
  }
}

isFormValid() {
  return this.questions.every(question => question.answers && question.answers.length > 0);
}

}