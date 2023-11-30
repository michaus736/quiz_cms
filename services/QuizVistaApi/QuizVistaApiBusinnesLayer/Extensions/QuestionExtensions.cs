using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions
{
    public static class QuestionExtensions
    {

        public static QuestionResponse Convert(this Question question)
        {
            return new QuestionResponse(
                question.Id,
                question.Type,
                question.Text,
                question.AdditionalValue,
                question.SubstractionalValue,
                question.QuizId,
                question.CmsTitleStyle,
                question.CmsQuestionsStyle,
                question.Answers.ConvertCollection().ToList(),
                question.Quiz.Convert()
            );
        }


        public static IEnumerable<QuestionResponse> ConvertCollection(this IEnumerable<Question> questions) {
            return questions.Select(Convert) ?? new List<QuestionResponse>();
        }

        public static Question ToEntity(this QuestionRequest request)
        {
            return new Question
            {
                Type = request.Type,
                Text = request.Text,
                AdditionalValue = request.AdditionalValue,
                SubstractionalValue = request.SubstractionalValue,
                QuizId = request.QuizId,
                CmsTitleStyle = request.CmsTitleStyle,
                CmsQuestionsStyle = request.CmsQuestionsStyle,
                Answers = request.Answers.ConvertCollection().ToList()
            };
        }

    }
}
