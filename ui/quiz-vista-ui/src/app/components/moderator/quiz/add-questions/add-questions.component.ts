import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Question } from 'src/app/models/question';
import { Answer } from 'src/app/models/answer';
import { QuestionHttpService } from 'src/app/services/http/question-http-service';
import { QuizHttpService } from 'src/app/services/http/quiz-http-service';
import { AnswertHttpService } from 'src/app/services/http/answer-http.service';


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
  questionsToDelete: string[] = [];
  answersToDelete: string[]=[];
  newAnswers: Answer[]=[];
  


  constructor(private route: ActivatedRoute,private quizHttpService: QuizHttpService, private questionHttpService: QuestionHttpService, private answerHttpService: AnswertHttpService,private router: Router) { }

  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>{
      this.quizName = params.get('quizName') ?? '';
    })

    this.getQuizDetails(this.quizName)
    this.getQuestionsForQuiz(this.quizName)
  }

  onSubmit() {
    if (!this.isFormValid()) {
      alert('Każde pytanie musi mieć prawidłową odpowiedź.');
      return;
    }

    this.answersToDelete.forEach(answerId => {
      this.answerHttpService.deleteAnswer(answerId).subscribe(
        response => console.log('Odpowiedź usunięta:', response),
        error => console.error('Błąd podczas usuwania odpowiedzi:', error)
      );
    });


    this.questionsToDelete.forEach(questionId => {
      this.questionHttpService.deleteQuestion(questionId).subscribe(
        response => console.log('Pytanie usunięte:', response),
        error => console.error('Błąd podczas usuwania pytania:', error)
      );
    });


    this.newAnswers.forEach(answer => {
      if (answer.answerText.trim() !== '') {
        this.answerHttpService.createAnswer(answer).subscribe(
          response => console.log('Odpowiedź dodana:', response),
          error => console.error('Błąd podczas dodawania odpowiedzi:', error)
        );
      }
    });

    console.log(this.questions);
    if (this.quizDetails) {
      this.questions.forEach(question => {
        if (question.id === 0 && question.text.trim() !== '') {
          question.quizId = this.quizDetails.id; 
  
          this.questionHttpService.createQuestion(question).subscribe(
            response => {
              console.log('Pytanie dodane:', response);
            },
            error => {
              console.error('Wystąpił błąd przy dodawaniu pytania:', error);
            }
          );
        }
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
      answers: [
        {
          id: 0,
          questionId: 0,
          answerText: '',
          isCorrect: false
        },
        {
          id: 0,
          questionId: 0,
          answerText: '',
          isCorrect: false
        }
      ]
    };

    this.questions.push(newQuestion);
  }

  editQuestion(question: Question) {
    if (question.id === 0) {
      console.log('Pytanie nie zostało jeszcze zapisane.');
      return;
    }
  
    this.questionHttpService.editQuestion(question).subscribe(
      response => console.log('Pytanie zaktualizowane:', response),
      error => console.error('Błąd podczas aktualizacji pytania:', error)
    );
  }

  deleteQuestion(index: number) {
      const question = this.questions[index];
  if (question.id !== 0) {
    this.questionsToDelete.push(question.id.toString());
    this.questions.splice(index, 1); // Ukryj pytanie z interfejsu użytkownika
  } else {
    this.questions.splice(index, 1); // Usuń nowe pytanie, które nie ma jeszcze ID
  }
  }
  

  addAnswer(question: Question) {
    const newAnswer: Answer = {
      id: 0, 
      questionId: question.id, 
      answerText: '',
      isCorrect: false,
    };
    if (question.id !== 0) {
      this.newAnswers.push(newAnswer);
    }
    question.answers.push(newAnswer);
  }

  editAnswer(question: Question, answer: Answer) {
    if (question.id !== 0 && answer.id !== 0) {
      this.answerHttpService.editAnswer(answer).subscribe(
        response => console.log('Odpowiedź zaktualizowana:', response),
        error => console.error('Błąd podczas aktualizacji odpowiedzi:', error)
      );
    }
  }

  
  
  getQuizDetails(quizName: string){
    this.quizHttpService.getQuizDetails(quizName).subscribe(res=>{
      console.log(res)
      this.quizDetails = res.model
    })
}

deleteQuiz(){
  this.quizHttpService.deleteQuiz(this.quizDetails.id).subscribe(res=>{
    this.router.navigate(['/moderator/quizzez']);
  },
  error=>{
    console.error('Wystąpił bład podczas usuwania quizu', error);
  })
}

deleteAnswer(question: Question, answerIndex: number) {
  const answer = question.answers[answerIndex];

  if (question.id === 0) {
    // Dla nowo dodanego pytania, po prostu usuń odpowiedź lokalnie
    question.answers.splice(answerIndex, 1);
  } else if (answer.id === 0) {
    // Dla nowo dodanej odpowiedzi w istniejącym pytaniu, usuń ją z listy newAnswers
    const newAnswerIndex = this.newAnswers.findIndex(a => a === answer);
    if (newAnswerIndex !== -1) {
      this.newAnswers.splice(newAnswerIndex, 1);
    }
  } else {
    // Dla istniejącej odpowiedzi, dodaj jej ID do listy do usunięcia
    this.answersToDelete.push(answer.id.toString());
    question.answers.splice(answerIndex, 1);
  }
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
  else{
    question.answers.push({ id: 0, questionId: question.id, answerText: '', isCorrect: false });
    question.answers.push({ id: 0, questionId: question.id, answerText: '', isCorrect: false });
  }
}

isFormValid() {
  return this.questions.every(question =>
    question.text.trim() !== '' && 
    question.answers &&
    question.answers.length > 0 &&
    question.answers.some(answer => answer.isCorrect) && 
    question.answers.every(answer => answer.answerText.trim() !== '') 
  );
}


}