using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Models.Requests
{
    public class AnswerRequest
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerRequest() { }

        public AnswerRequest(int id,int questionId, string answerText, bool isCorrect)
        {
            Id = id;
            QuestionId = questionId;
            AnswerText = answerText;
            IsCorrect = isCorrect;
        }
    }
}
