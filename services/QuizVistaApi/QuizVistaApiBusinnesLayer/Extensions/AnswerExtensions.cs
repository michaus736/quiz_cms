using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class AnswerExtensions
    {

        public static AnswerResponse Convert(this Answer answer)
        {
            return new AnswerResponse
            (
                answer.Id,
                answer.AnswerText,
                answer.IsCorrect,
                answer.QuestionId,
                answer.AttemptId,
                answer.Attempt.Convert(),
                answer.Question.Convert()
            );
        }

        public static IEnumerable<AnswerResponse> ConvertCollection(this IEnumerable<Answer> answers)
        {
            return answers.Select(Convert) ?? new List<AnswerResponse>();
        }

    }
}
