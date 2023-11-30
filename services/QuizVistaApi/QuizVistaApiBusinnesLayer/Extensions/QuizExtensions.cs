using Azure.Core;
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
    public static class QuizExtensions
    {

        public static QuizResponse Convert(this Quiz quiz)
        {
            return new QuizResponse(
                quiz.Id,
                quiz.Name,
                quiz.Description,
                quiz.CreationDate,
                quiz.EditionDate,
                quiz.CategoryId,
                quiz.CmsTitleStyle,
                quiz.UserId,
                quiz.Category.Convert(),
                quiz.Questions.ConvertCollection().ToList(),
                quiz.User.Convert(),
                quiz.Tags.ConvertCollection().ToList(),
                quiz.Users.ConvertCollection().ToList()
            );
        }

        public static IEnumerable<QuizResponse> ConvertCollection(this IEnumerable<Quiz> quizes) {
            return quizes.Select(Convert) ?? new List<QuizResponse>();
        }

        public static Quiz ToEntity(this QuizRequest quizRequest)
        {
            return new Quiz
            {
                Name = quizRequest.Name,
                Description = quizRequest.Description,
                CategoryId = quizRequest.CategoryId,
                CmsTitleStyle = quizRequest.CmsTitleStyle,
                UserId = quizRequest.UserId,
                Tags = quizRequest.TagIds.Select(id => new Tag { Id = id }).ToList()
            };
        }


    }
}
