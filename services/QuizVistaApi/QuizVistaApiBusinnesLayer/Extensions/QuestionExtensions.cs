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

        public static QuestionResponse ToResponse(this Question question)
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
                question.Answers.ToCollectionResponse().ToList(),
                question.Quiz.ToResponse()
            );
        }


        public static IEnumerable<QuestionResponse> ToCollectionResponse(this IEnumerable<Question> questions) {
            return questions.Select(ToResponse) ?? new List<QuestionResponse>();
        }

    }
}
