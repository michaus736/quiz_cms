using Azure.Core;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class QuizExtensions
    {

        public static QuizResponse ToResponse(this Quiz quiz)
        {
            return new QuizResponse(
                quiz.Id,
                quiz.Name,
                quiz.Description,
                quiz.CreationDate,
                quiz.EditionDate,
                quiz.CategoryId,
                quiz.CmsTitleStyle,
                quiz.AuthorId,
                quiz.IsActive,
                quiz.AttemptCount,
                quiz.PublicAccess,
                quiz?.Author?.ToResponse(),
                quiz?.Category?.ToResponse(),
                quiz?.Questions?.ToCollectionResponse()?.ToList(),
                quiz?.Tags?.ToCollectionResponse()?.ToList(),
                quiz?.Users?.ToCollectionResponse()?.ToList()
            );
        }

        public static IEnumerable<QuizResponse> ToCollectionResponse(this IEnumerable<Quiz> quizes)
        {
            return quizes.Select(ToResponse) ?? Enumerable.Empty<QuizResponse>();
        }

        public static Quiz ToEntity(this QuizRequest quizRequest)
        {
            return new Quiz
            {
                Name = quizRequest.Name,
                Description = quizRequest.Description,
                CategoryId = quizRequest.CategoryId,
                CmsTitleStyle = quizRequest.CmsTitleStyle,
                //AuthorId = quizRequest.AuthorId,
                Tags = quizRequest.TagIds.Select(id => new Tag { Id = id }).ToList()
            };
        }


    }
}
